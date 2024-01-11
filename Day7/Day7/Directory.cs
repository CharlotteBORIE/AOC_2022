namespace Day7;

public class Directory
{
    public Directory parent;
    public List<Directory> directories;
    public List<File> files;
    public int FullSize => SetSize();
    public string name;
    

    public Directory(Directory parentname,List<Directory> direc, List<File> fil,string nameValue)
    {
        parent = parentname;
        directories = direc;
        files = fil;
        name = nameValue;
    }
    
    public Directory(string nameValue)
    {
        parent = this;
        name = nameValue;
        directories = new List<Directory>();
        files = new List<File>();
    }
    
    public Directory(Directory parentname,string nameValue)
    {
        parent = parentname;
        name = nameValue;
        directories = new List<Directory>();
        files = new List<File>();
    }

    public void AddFile(int value,string nameValue)
    {
        files.Add(new File(value,nameValue));
    }
    
    public void AddDirectory(string nameValue)
    {
        directories.Add(new Directory(this,nameValue));
    }

    public Directory FindChild(string nameWanted)
    {
        foreach (var direc in directories)
        {
            if (direc.name == nameWanted)
            {
                return direc;
            }
        }

        throw new Exception("directory doesn't exist");
    }

    public override string ToString()
    {
        string ret = name + "/" + FullSize;
        string path=" )";
        var parentTemp=parent;
        while (parentTemp.name != "Home")
        {
            path = "/" +parentTemp.name +  path;
            parentTemp = parentTemp.parent;
        }

        ret += "   ( Home"+path;

        // if (files.Count != 0)
        // {
        //     ret+="\n        Files\n           "+String.Join("\n           ",files);
        // }
        if (directories.Count != 0)
        {
            ret += " : \n - " + String.Join("\n - ", directories);
        }

        return ret;
    }

    public int SetSize()
    {
        var size = 0;
        foreach (var file in files)
        {
            size += file.size;
        }

        foreach (var direc in directories)
        {
            size += direc.SetSize();
        }
        return size;
    }

    public int FindHeaviest(int number)
    {
        SetSize();
        List<int> heavy = new List<int>(directories.Count);
        foreach (var direc in directories)
        {
            heavy.Add(direc.FullSize);
        }

        var sort=heavy.OrderByDescending(v => v);
        return sort.Take(number).Sum();
    }
    
    public int FindUnder(int number)
    {
        //not 1324583
        SetSize();
        int var = 0;
        foreach (var direc in directories)
        {
            if (direc.FullSize <= number)
            {
                var += direc.FullSize;
            }
            var+=direc.FindUnder(number);
        }

        return var;
    }
    
    public int FindSmallestAbove(int number)
    {
        int min = 30000000;
        foreach (var direc in directories)
        {
            if (direc.FullSize >= number && direc.FullSize<min)
            {
                min = direc.FullSize;
            }

            var temp = direc.FindSmallestAbove(number);
            min = temp == 0 ? min : Math.Min(min,temp);
        }

        return min;
    }
}