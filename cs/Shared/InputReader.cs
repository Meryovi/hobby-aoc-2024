namespace aoc24;

public static class InputReader
{
    public const string NewLine = "\r\n";

    private static string? rootDirectory = null;

    public static string ReadProblemInput(string id) => File.ReadAllText($"{GetRootDirectory()}/input/{id}.txt");

    private static string GetRootDirectory()
    {
        if (rootDirectory is not null)
            return rootDirectory;

        var directory = new DirectoryInfo(Environment.CurrentDirectory);

        while (directory!.Name != "cs")
            directory = directory.Parent;

        rootDirectory = directory.Parent!.ToString();
        return rootDirectory;
    }
}
