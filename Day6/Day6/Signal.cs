namespace Day6;

public class Signal
{
    public int index_marker;
    public List<char> marker;
    public List<char> message;
    public int size = 4;
    public int messageSize = 14;
    public Signal(ReadFile read)
    {
        marker= new List<char>(size);

        var signal = read.lines.First();
        for (; index_marker < size; index_marker++)
        {
            marker.Add(signal[index_marker]);
        }

        int jump = FoundJump(size,marker);
        while(jump > 0)
        {
            marker.RemoveRange(0,jump);
            for (int i = 0; i < jump; i++)
            {
                marker.Add(signal[index_marker]);
                index_marker++;
            }
            
            jump = FoundJump(size,marker);
        }

        Console.WriteLine(String.Join(" ",marker));
        Console.WriteLine(index_marker);
        
        message= new List<char>(messageSize);
        message.AddRange(marker);
        for (int i=0; i < messageSize-size; i++)
        {
            message.Add(signal[index_marker]);
            index_marker++;
        }
        
        int jump2 = FoundJump(messageSize,message);
        while(jump2 > 0)
        {
            message.RemoveRange(0,jump2);
            for (int i = 0; i < jump2; i++)
            {
                message.Add(signal[index_marker]);
                index_marker++;
            }
            
            jump2 = FoundJump(messageSize,message);
        }
        
        Console.WriteLine(String.Join(" ",message));
        Console.WriteLine(index_marker);
        
    }

    private bool MarkerFoundInit()
    {
        for (int i=0; i < size; i++)
        {
            for (int j = i + 1; j < size; j++)
            {
                if (marker[i] == marker[j])
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    private bool MarkerFound(char cara)
    {
        for (int i=1; i < size; i++)
        {
            if (marker[i] == cara)
            {
                return false;
            }
        }
        return true;
    }
    
    private int FoundJump(int sizeWanted,List<char> list)
    {
        for (int i=sizeWanted-1; i >0; i--)
        {
            for (int j = i - 1; j >=0; j--)
            {
                if (list[i] == list[j])
                {
                    return j+1;
                }
            }
        }

        return 0;
    }
    
}