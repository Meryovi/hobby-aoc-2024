## C# solutions

These are the solutions in one of my favorite languages: C#

To run all unit tests, run `dotnet test` in the `cs` folder. If you want to see more details, run `dotnet test -l "console;verbosity=detailed"` instead.

To run the benchmarks, run `dotnet run` or `dotnet run #day` (where #day is the problem day, e.g. `dotnet run 18`) or `dotnet run all` to run all benchmarks.

But remember! The `input` folder must exist and contain valid input files for each problem day.
Wish I could include the input files in this repo, but unfortunately it's not possible! :(

### Benchmark results

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
| Day13Benchmark | 'Day13 problem' |    22,346.45 ns |  2,580.048 ns |   141.421 ns |      - |         - |
| Day14Benchmark | 'Day14 problem' |       336.07 ns |     70.665 ns |     3.873 ns |      - |         - |
| Day15Benchmark | 'Day15 problem' |       717.13 ns |     87.646 ns |     4.804 ns |      - |         - |
| Day16Benchmark | 'Day16 problem' |    11,249.31 ns |  2,445.643 ns |   134.054 ns |      - |         - |
| Day17Benchmark | 'Day17 problem' |       309.28 ns |     56.397 ns |     3.091 ns | 0.0429 |     272 B |
| Day18Benchmark | 'Day18 problem' |     8,319.80 ns |  2,119.593 ns |   116.182 ns |      - |         - |
