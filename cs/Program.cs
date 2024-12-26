using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

var benchmarkConfig = ManualConfig
    .Create(DefaultConfig.Instance)
    .WithOptions(ConfigOptions.JoinSummary)
    .WithOptions(ConfigOptions.DisableLogFile);

var assembly = typeof(Program).Assembly;

Console.WriteLine("Number of the problem day to benchmark? (defaults to all)");

if (int.TryParse(Console.ReadLine(), out int testToRun))
{
    // A specific one
    var matchingBench = assembly.GetTypes().Where(t => t.Name.StartsWith($"Day{testToRun}Bench")).FirstOrDefault();
    if (matchingBench is not null)
    {
        Console.WriteLine($"Running benchmark for Day {testToRun}");
        BenchmarkRunner.Run(matchingBench, benchmarkConfig);
        return;
    }
}

// All benchmarks
Console.WriteLine("Running all benchmarks");
BenchmarkRunner.Run(assembly, benchmarkConfig);
