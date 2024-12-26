## Benchmark results

This is how the allocations stacked on the final and optimized versions of the problems.

**Ran on:**
13th Gen Intel(R) Core(TM) i5-1340P
Base speed: 1.90 GHz
Cores: 12
Memory: 16 GB

| Type          | Method         |        Mean |      Error |    StdDev |   Gen0 | Allocated |
| ------------- | -------------- | ----------: | ---------: | --------: | -----: | --------: |
| Day1Benchmark | 'Day1 problem' |   164.59 ns |  10.317 ns |  0.566 ns |      - |         - |
| Day2Benchmark | 'Day2 problem' |   399.60 ns |  12.473 ns |  0.684 ns |      - |         - |
| Day3Benchmark | 'Day3 problem' |    77.39 ns |   1.816 ns |  0.100 ns |      - |         - |
| Day4Benchmark | 'Day4 problem' | 2,188.69 ns | 201.389 ns | 11.039 ns |      - |         - |
| Day5Benchmark | 'Day5 problem' | 1,132.20 ns | 102.219 ns |  5.603 ns | 0.0858 |     544 B |
| Day6Benchmark | 'Day6 problem' | 1,224.95 ns | 113.220 ns |  6.206 ns |      - |         - |
