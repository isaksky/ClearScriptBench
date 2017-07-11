# ClearScriptBench
Benchmark some ways to pass objects from .NET to Javascript in ClearScript

# Results

``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i5-4690K CPU 3.50GHz (Haswell), ProcessorCount=4
Frequency=3417977 Hz, Resolution=292.5707 ns, Timer=TSC
  [Host]     : Clr 4.0.30319.42000, 64bit RyuJIT-v4.7.2098.0 [AttachedDebugger]
  DefaultJob : Clr 4.0.30319.42000, 64bit RyuJIT-v4.7.2098.0


```
 | Method |       Mean |     Error |   StdDev |     Median |
 |------- |-----------:|----------:|---------:|-----------:|
 |  Embed | 1,063.0 us | 187.42 us | 546.7 us | 1,143.5 us |
 |   Json |   347.1 us |  72.50 us | 210.3 us |   287.9 us |
