# ClearScriptBench
Benchmark some ways to pass objects from .NET to Javascript in [ClearScript](https://microsoft.github.io/ClearScript/Examples/Examples.html).

# Results

``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i5-4690K CPU 3.50GHz (Haswell), ProcessorCount=4
Frequency=3417977 Hz, Resolution=292.5707 ns, Timer=TSC
  [Host]     : Clr 4.0.30319.42000, 64bit RyuJIT-v4.7.2098.0
  DefaultJob : Clr 4.0.30319.42000, 64bit RyuJIT-v4.7.2098.0


```
 | Method |       Mean |     Error |   StdDev |
 |------- |-----------:|----------:|---------:|
 |  Embed | 1,058.9 us | 182.67 us | 535.8 us |
 |   Json |   377.7 us |  76.93 us | 222.0 us |
