``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.16299.1029 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i7-6820HQ CPU 2.70GHz (Skylake), 1 CPU, 4 logical and 4 physical cores
Frequency=2648437 Hz, Resolution=377.5812 ns, Timer=TSC
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT
  DefaultJob : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT


```
|            Method |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------ |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|     SequenceParse | 206.2 ns |  3.97 ns |  3.71 ns |  1.21 |    0.03 | 0.0477 |     - |     - |     200 B |
| StreamReaderParse | 604.9 ns | 11.48 ns | 11.79 ns |  3.56 |    0.12 | 0.8678 |     - |     - |    3632 B |
|         SpanParse | 170.0 ns |  3.42 ns |  3.66 ns |  1.00 |    0.00 | 0.0477 |     - |     - |     200 B |
| SequenceFastParse | 198.8 ns |  3.98 ns |  4.58 ns |  1.17 |    0.04 | 0.0477 |     - |     - |     200 B |
|   ByteByByteParse | 255.7 ns |  5.12 ns |  6.47 ns |  1.51 |    0.04 | 0.0401 |     - |     - |     168 B |
