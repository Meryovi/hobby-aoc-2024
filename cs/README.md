## Benchmark results

This is how the allocations stacked on the final and optimized versions of the problems.

**Benchmarked on:**
13th Gen Intel(R) Core(TM) i5-1340P
Base speed: 1.90 GHz
Cores: 12
Memory: 16 GB

| Type           | Method          |            Mean |         Error |       StdDev |   Gen0 | Allocated |
| -------------- | --------------- | --------------: | ------------: | -----------: | -----: | --------: |
| Day1Benchmark  | 'Day1 problem'  |       163.22 ns |      3.139 ns |     0.172 ns |      - |         - |
| Day2Benchmark  | 'Day2 problem'  |       313.17 ns |     36.408 ns |     1.996 ns |      - |         - |
| Day3Benchmark  | 'Day3 problem'  |        75.26 ns |      3.205 ns |     0.176 ns |      - |         - |
| Day4Benchmark  | 'Day4 problem'  |     2,152.16 ns |     63.143 ns |     3.461 ns |      - |         - |
| Day5Benchmark  | 'Day5 problem'  |     1,019.13 ns |     54.079 ns |     2.964 ns | 0.0858 |     544 B |
| Day6Benchmark  | 'Day6 problem'  |     1,210.32 ns |     10.136 ns |     0.556 ns |      - |         - |
| Day7Benchmark  | 'Day7 problem'  |       481.51 ns |     21.665 ns |     1.188 ns |      - |         - |
| Day8Benchmark  | 'Day8 problem'  |       759.30 ns |     70.432 ns |     3.861 ns | 0.0277 |     176 B |
| Day9Benchmark  | 'Day9 problem'  |        54.84 ns |     10.966 ns |     0.601 ns |      - |         - |
| Day10Benchmark | 'Day10 problem' |     6,864.10 ns |     372.63 ns |    20.425 ns |      - |         - |
| Day11Benchmark | 'Day11 problem' | 1,034,175.91 ns | 52,701.946 ns | 2,888.771 ns |      - |    1025 B |
| Day12Benchmark | 'Day12 problem' |       857.18 ns |     51.934 ns |     2.847 ns |      - |         - |
