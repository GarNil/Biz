using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace Biz.Common
{
    public static class CsvHelperProxy
    {
        private const string DefaultSeparator = ";";
        private const byte SemiColon = 59;

        private static CsvConfiguration Configuration(ClassMap map, string delimiter = DefaultSeparator)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.Delimiter = delimiter;
            if (map != null)
                config.RegisterClassMap(map);
            return config;
        }

        /// <summary>
        /// A try to challenge the ReadRows method 
        /// FAILED -> More rows of code, More complexity and less perf... (maybe better in memory??)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sizeBuffer"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static IEnumerable<string[]> ReadRowsLowLevel(string path, int sizeBuffer = 4096, string separator = DefaultSeparator)
        {
            using (var stream = File.OpenRead(path))
            {
                const byte CR = 13;
                const byte LF = 10;
                //const byte SEMICOLON = 59;
                var buffer = new byte[sizeBuffer];
                int sizeToRead = stream.Read(buffer, 0, buffer.Length);
                var chars = new char[sizeBuffer];
                int prevIndex = 0;
                int index = 0;
                while (sizeToRead > 0)
                {
                    for (int i = 0; i < sizeToRead; i++)
                    {
                        switch (buffer[i])
                        {
                            case CR:
                            case LF:
                                if (prevIndex != index)
                                {
                                    yield return new string(chars, prevIndex, index - prevIndex).Split(separator);
                                    prevIndex = index;
                                }
                                break;
                            default:
                                chars[index++] = Convert.ToChar(buffer[i]);
                                if (index == chars.Length)
                                {
                                    index = chars.Length - prevIndex;
                                    Array.Copy(chars, prevIndex, chars, 0, index);
                                    prevIndex = 0;
                                }
                                break;
                        }
                    }
                    sizeToRead = stream.Read(buffer, 0, buffer.Length);
                }
                if (prevIndex != index)
                {
                    yield return new string(chars, prevIndex, index - prevIndex).Split(separator);
                }
            }
        }

        public static IEnumerable<string[]> ReadRows(string path, string separator = DefaultSeparator)
        {
            using (var stream = File.OpenRead(path))
            {
                foreach (var result in ReadRowsFromStream(stream, separator))
                    yield return result;
            }
        }

        public async static IAsyncEnumerable<string[]> ReadRowsAsync(string path, string separator = DefaultSeparator)
        {
            using (var stream = File.OpenRead(path))
            {
                await foreach (var result in ReadRowsFromStreamAsync(stream, separator))
                    yield return result;
            }
        }

        public static IEnumerable<string[]> ReadRowsContent(string input, string separator = DefaultSeparator)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(input)))
            {
                foreach (var result in ReadRowsFromStream(stream, separator))
                    yield return result;
            }
        }

        public static IEnumerable<string[]> ReadRowsFromStream(Stream stream, string separator = DefaultSeparator)
        {
            using (var sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                    yield return sr.ReadLine().Split(separator);
            }
        }

        public async static IAsyncEnumerable<string[]> ReadRowsFromStreamAsync(Stream stream, string separator = DefaultSeparator)
        {
            using (var sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                    yield return (await sr.ReadLineAsync()).Split(separator);
            }
        }


        //public static IObservable<(string[] n, int i)> ReadRowsObservable(string path, string separator = DefaultSeparator)
        //    => ReadRows(path, separator).Select((n, i) => (n, i)).ToObservable();
        public static IObservable<string[]> ReadRowsObservable(string path, string separator = DefaultSeparator)
            => ReadRows(path, separator).ToObservable();

        
        public static IEnumerable<T> Read<T>(string path, ClassMap map = null, bool withHeader = true)
            where T : class
        {
            using (var stream = File.OpenRead(path))
            {
                foreach (var v in Read<T>(stream, map, withHeader))
                    yield return v;
            }
        }

        public static IEnumerable<T> Read<T>(Stream file, ClassMap map = null, bool withHeader = true)
            where T : class
        {
            using (var ctx = new StreamReader(file))
            using (var csv = new CsvReader(ctx, Configuration(map)))
            {
                csv.Configuration.MissingFieldFound = null;
                csv.Read();
                if (withHeader)
                    csv.ReadHeader();
                while (csv.Read())
                {
                    yield return csv.GetRecord<T>();
                }
            }
        }

        public static IEnumerable<T> Read<T, TClassMap>(string path, bool withHeader = true)
            where T : class
            where TClassMap : ClassMap, new()
            => Read<T>(path, new TClassMap(), withHeader);


        /// <summary>
        /// Reads the specified file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TClassMap">The type of the class map.</typeparam>
        /// <param name="file">The file.</param>
        /// <param name="withHeader">if set to <c>true</c> [with header].</param>
        /// <returns></returns>
        public static IEnumerable<T> Read<T, TClassMap>(Stream file, bool withHeader = true)
            where T : class
            where TClassMap : ClassMap, new()
            => Read<T>(file, new TClassMap(), withHeader);
    }
}
