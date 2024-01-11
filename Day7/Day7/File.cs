namespace Day7;

public class File
{
    public int size;
    public string name;

    public File(int value,string nameValue)
    {
        size = value;
        name = nameValue;
    }

    public override string ToString()
    {
        return name +"/"+ size.ToString();
    }
}