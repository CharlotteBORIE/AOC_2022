namespace Day20;

public class EncryptedMessage
{
    public (long,long)[] message;
    public long size;
    public int multiplier = 811589153;

    public EncryptedMessage(string[] lines)
    {
        size = lines.Length;
        message = new (long,long)[size];
        for (int i = 0; i < lines.Length; i++)
        {
            message[i] = (i,multiplier*Int64.Parse(lines[i]));
        }
        for (int i = 0; i < 10; i++)
        {
            DecryptMessage();
        }
        Console.WriteLine(GetCoordinates());
    }

    private long GetCoordinates()
    {
        var index0 = message.Select((value, index) => (value, index))
            .Where(pair => pair.value.Item2 == 0)
            .Select(pair => pair.index)
            .First();
        var index1 = message[(index0 + 1000) % size].Item2;
        var index2 = message[(index0 + 2000) % size].Item2;
        var index3 = message[(index0 + 3000) % size].Item2;
        //Console.WriteLine(message[(index0+1000) % size] +" "+ message[(index0+2000) % size] +" "+ message[(index0+3000) % size]);
        return index1 + index2 + index3;
    }

    private void DecryptMessage()
    {
        
            for (int indexToMove = 0; indexToMove < size; indexToMove++)
            {
                var list = message.Select((value, index) => (value, index))
                    .Where(pair => pair.value.Item1 == indexToMove)
                    .Select(pair => (pair.value.Item2, pair.index));
                if (list.Count() != 1)
                {
                    Console.Write(" ahhhhhhhhhh");
                }

                var currentIndex = list.First();
                MoveCoordinates(currentIndex.Item2, currentIndex.Item1, indexToMove);
            }
            Console.Write("");
    }

    private void MoveCoordinates(int index, long indexToAdd,int oldIndex)
    {
        var modulo = mod(indexToAdd, size - 1);
        if (modulo == 0)
        {
            return;
        }
        var newIndex = index + modulo;
        var positiveIndexToAdd= mod(newIndex,size-1) ;
        // if (newIndex > size)
        // {
        //     positiveIndexToAdd--;
        // }
        // else if (newIndex < -size)
        // {
        //     positiveIndexToAdd++;
        // }
         
        // if (newIndex >= 2 * size)
        // {
        //     positiveIndexToAdd+=45;
        // }
        
        if (positiveIndexToAdd== 0 && indexToAdd<0)
        {
            positiveIndexToAdd = size-1 ;
        }
        else if (positiveIndexToAdd == size-1 && indexToAdd > 0)
        {
            positiveIndexToAdd = 0;
        }
    


        if (index<positiveIndexToAdd)
        {
            for (int i = index; i < positiveIndexToAdd; i++)
            {
                message[i] = message[i+1];
            }
        }
        else
        {
            for (int i = index; i > positiveIndexToAdd; i--)
            {
                message[i] = message[i-1];
            }
        }
        message[positiveIndexToAdd] = (oldIndex,indexToAdd);
    }
    
    
    int mod(int x, int m) {
        int r = x%m;
        return r<0 ? r+m : r;
    }
    
    long mod(long x, long m) {
        long r = x%m;
        return r<0 ? r+m : r;
    }
}