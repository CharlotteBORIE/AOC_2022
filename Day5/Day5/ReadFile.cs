namespace Day5;

public class ReadFile
{
    public string[] lines;
    
    public ReadFile(string path)
    {
        lines = File.ReadAllLines(path);
    }
}