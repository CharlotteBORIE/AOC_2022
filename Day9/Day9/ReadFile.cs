namespace Day9;

public struct ReadFile
{
    public string[] lines;

    public ReadFile(string path)
    {
        lines = File.ReadAllLines(path);
    } 
}