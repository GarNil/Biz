using System;

namespace Biz.Benchmark
{
    using System;
    using System.Buffers;
    using System.IO;
    using System.Text;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Running;

    namespace MyBenchmarks
    {
        // -> https://gist.github.com/davidfowl/323a5a037700ad49692b97c0afff69f6
        // It looks great to use... (one run)
        [MemoryDiagnoser]
        public class HttpLineParseBenchmark
        {
            private readonly byte[] data;
            private readonly ReadOnlySequence<byte> sequence;
            private readonly MemoryStream stream;

            public HttpLineParseBenchmark()
            {
                data = Encoding.UTF8.GetBytes("GET /this/is/a/test?foo=2&bar=45&baz=294r4r3r4rrr43r4r HTTP/1.1\r\n");
                sequence = new ReadOnlySequence<byte>(data);
                stream = new MemoryStream(data);
            }

            [Benchmark]
            public (string, string, string) SequenceParse()
            {
                var utf8 = Encoding.UTF8;

                var reader = new SequenceReader<byte>(sequence);
                reader.TryReadTo(out ReadOnlySpan<byte> method, (byte)' ');
                reader.TryReadTo(out ReadOnlySpan<byte> uri, (byte)' ');
                reader.TryReadTo(out ReadOnlySpan<byte> version, (byte)'\r');

                return (utf8.GetString(method), utf8.GetString(uri), utf8.GetString(version));
            }

            [Benchmark]
            public (string, string, string) StreamReaderParse()
            {
                var reader = new StreamReader(stream);

                var span = reader.ReadLine().AsSpan();
                var index = span.IndexOf(' ');
                var method = span.Slice(0, index);
                span = span.Slice(index + 1);
                index = span.IndexOf(' ');
                var uri = span.Slice(0, index);
                span = span.Slice(index + 1);
                var version = span;

                stream.Position = 0;

                return (new string(method), new string(uri), new string(version));
            }

            [Benchmark(Baseline = true)]
            public (string, string, string) SpanParse()
            {
                var utf8 = Encoding.UTF8;

                var span = data.AsSpan();
                var index = span.IndexOf((byte)' ');
                var method = utf8.GetString(span.Slice(0, index));
                span = span.Slice(index + 1);
                index = span.IndexOf((byte)' ');
                var uri = utf8.GetString(span.Slice(0, index));
                span = span.Slice(index + 1);
                index = span.IndexOf((byte)'\r');
                var version = utf8.GetString(span.Slice(0, index));

                return (method, uri, version);
            }

            [Benchmark]
            public (string, string, string) SequenceFastParse()
            {
                var utf8 = Encoding.UTF8;

                var reader = new SequenceReader<byte>(sequence);
                string method = null, uri = null, version = null;

                var span = reader.CurrentSpan;

                var index = span.IndexOf((byte)' ');

                if (index == -1)
                {
                    // Slow path
                    reader.TryReadTo(out ReadOnlySpan<byte> methodSpan, (byte)' ');
                    method = utf8.GetString(methodSpan);
                    span = reader.UnreadSpan;
                }
                else
                {
                    // Fast path
                    method = utf8.GetString(span.Slice(0, index));
                    span = span.Slice(index + 1);
                    reader.Advance(index + 1);
                }

                index = span.IndexOf((byte)' ');
                if (index == -1)
                {
                    reader.TryReadTo(out ReadOnlySpan<byte> uriSpan, (byte)' ');
                    uri = utf8.GetString(uriSpan);
                    span = reader.UnreadSpan;
                }
                else
                {
                    uri = utf8.GetString(span.Slice(0, index));
                    span = span.Slice(index + 1);
                    reader.Advance(index + 1);
                }

                index = span.IndexOf((byte)'\r');
                if (index == -1)
                {
                    reader.TryReadTo(out ReadOnlySpan<byte> versionSpan, (byte)' ');
                    version = utf8.GetString(versionSpan);
                    span = reader.UnreadSpan;
                }
                else
                {
                    version = utf8.GetString(span.Slice(0, index));
                    reader.Advance(index + 1);
                }


                return (method, uri, version);
            }

            [Benchmark]
            public (string, string, string) ByteByByteParse()
            {
                var utf8 = Encoding.UTF8;

                var span = data.AsSpan();
                var state = 0;
                var previous = 0;
                string method = null, uri = null, version = null;

                for (int i = 0; i < span.Length && state < 3; i++)
                {
                    var ch = span[i];
                    switch (state)
                    {
                        case 0:
                            if (ch == (byte)' ')
                            {
                                method = utf8.GetString(span.Slice(0, previous));
                                previous = i + 1;
                                state++;
                            }
                            break;
                        case 1:
                            if (ch == (byte)' ')
                            {
                                uri = utf8.GetString(span.Slice(previous, i - previous));
                                previous = i + 1;
                                state++;
                            }
                            break;
                        case 2:
                            if (ch == (byte)'\r')
                            {
                                version = utf8.GetString(span.Slice(previous, i - previous));
                                previous = i + 1;
                                state++;
                            }
                            break;
                        default:
                            break;
                    }
                }

                return (method, uri, version);
            }
        }

        public class Program
        {
            public static void Main(string[] args)
            {
                var summary = BenchmarkRunner.Run<HttpLineParseBenchmark>();
            }
        }
    }
}
