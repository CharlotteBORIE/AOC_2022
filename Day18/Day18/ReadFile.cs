namespace Day18;

public struct ReadFile
{
    public string[] lines;

    public ReadFile(string path)
    {
        lines = File.ReadAllLines(path);
    }
}