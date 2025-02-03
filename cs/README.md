## C# solutions

These are the solutions in one of my favorite languages: C#

To run all unit tests, run `dotnet test` in the `cs` folder. If you want to see more details, run `dotnet test -l "console;verbosity=detailed"` instead.

To run the benchmarks, run `dotnet run` or `dotnet run #day` (where #day is the problem day, e.g. `dotnet run 18`) or `dotnet run all` to run all benchmarks.

But remember! The `input` folder must exist and contain valid input files for each problem day.
Wish I could include the input files in this repo, but, unfortunately, it's not possible. :(

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
| Day13Benchmark | 'Day13 problem' |    22,111.48 ns |    575.911 ns |    31.568 ns |      - |         - |
| Day14Benchmark | 'Day14 problem' |       333.91 ns |      8.703 ns |     0.477 ns |      - |         - |
| Day15Benchmark | 'Day15 problem' |       594.57 ns |     21.691 ns |     1.189 ns |      - |         - |
| Day16Benchmark | 'Day16 problem' |     9,136.05 ns |    359.076 ns |    19.682 ns |      - |         - |
| Day17Benchmark | 'Day17 problem' |       248.87 ns |     65.647 ns |     3.598 ns | 0.0429 |     272 B |
| Day18Benchmark | 'Day18 problem' |     7,839.59 ns |  3,206.277 ns |   175.747 ns |      - |         - |
| Day19Benchmark | 'Day19 problem' |       205.15 ns |     20.272 ns |     1.111 ns | 0.0637 |     400 B |
| Day20Benchmark | 'Day20 problem' |   368,658.17 ns |  4,617.472 ns |   253.099 ns |      - |     480 B |
| Day21Benchmark | 'Day21 problem' |     7,705.35 ns |   1,757.27 ns |    96.322 ns |      - |         - |
| Day22Benchmark | 'Day22 problem' |    42,393.31 ns |   2,943.68 ns |   161.353 ns |      - |         - |
| Day23Benchmark | 'Day23 problem' |    22,859.87 ns |     788.50 ns |    43.220 ns | 0.3662 |    2432 B |
| Day24Benchmark | 'Day24 problem' |       477.41 ns |     14.484 ns |     0.794 ns | 0.0153 |      96 B |
| Day25Benchmark | 'Day25 problem' |       685.46 ns |     59.278 ns |     3.249 ns |      - |         - |

### Closing remarks

As you can see, some solutions _do involve_ allocations. It can be tempting to aim for 0B on every problem, but the codes becomes hard to follow very quickly.
For that reason, I tried to strike a balance between readability (which is somewhat reduced by using lower level primitives) and reducing allocations.
Some problems, for example, require string manipulation, so those end up producing allocations anyway.
Overall, I'm happy with the results. :)
