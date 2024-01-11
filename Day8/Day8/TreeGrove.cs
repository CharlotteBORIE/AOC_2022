namespace Day8;

public struct TreeGrove
{
    public int rows;
    public int columns;

    public int[,] treeMap;

    public TreeGrove(string[] lines)
    {
        rows = lines.Length;
        columns = lines[0].Length;
        treeMap = new int[rows, columns];

        for (int index = 0; index < rows; index++)
        {
            var row = lines[index];
            for (int j = 0; j < columns; j++)
            {
                treeMap[index, j] = row[j]- '0';
            }
        }

    }

    public bool IsVisible(int row, int column)
    {
        if (row == 0 || row == rows || column == 0 || column == columns)
        {
            return true;
        }
        
        int height = treeMap[row, column];
        
        //top
        int top = 0;
        while( top < row && treeMap[top, column] < height) top++;
        if (top == row)
            return true;
        
        
        //bottom
        int bot = rows-1;
        while( bot > row && treeMap[bot, column] < height) bot--;
        if (bot == row)
            return true;
        
        
        //right
        int right = 0;
        while( right < column && treeMap[row, right] < height) right++;
        if (right == column)
            return true;

        //left
        int left = columns-1;
        while( left > column && treeMap[row, left] < height) left--;
        if (left == column)
            return true;
        

        return false;
    }
    
    public int[] Score(int row, int column)
    {
        //495720
        if (row == 0 || row == rows || column == 0 || column == columns)
        {
            return new int[]{0,0,0,0};
        }
        
        int height = treeMap[row, column];
        
        //top
        int top = 1;
        int lookUp = row-1;
        while (lookUp >0 && treeMap[lookUp, column] < height)
        {
            top++;
            lookUp--;
        }
        
        //bottom
        int bottom = 1;
        int lookDown = row+1;
        while (lookDown < rows-1 && treeMap[lookDown, column] < height)
        {
            bottom++;
            lookDown++;
        }
        
        //top
        int right = 1;
        int lookRight = column-1;
        while (lookRight > 0 && treeMap[row, lookRight] < height)
        {
            right++;
            lookRight--;
        }
        
        //bottom
        int left = 1;
        int lookLeft = column+1;
        while (lookLeft < columns-1 && treeMap[row, lookLeft] < height)
        {
            left++;
            lookLeft++;
        }

        return new int[] {top, bottom, right, left};
    }

    public int CountVisible()
    {
        //9801 too high
        int count = 0;
        for (var index0 = 0; index0 < rows; index0++)
        for (var index1 = 0; index1 < columns; index1++)
        {
            if (IsVisible(index0, index1))
            {
                count++;
            }
        }

        return count;
    }
    
    public int GetBestScore()
    {
        //9801 too high
        int bestScore = -1;
        for (var index0 = 0; index0 < rows; index0++)
        for (var index1 = 0; index1 < columns; index1++)
        {
            var temp = Score(index0, index1);
            var num = 1;
            foreach (var scroe in temp)
            {
                num *= scroe;
            }
            if(num>bestScore)
            {
                Console.WriteLine(index0+"  "+index1+" : "+String.Join(" ",temp));
                bestScore = num;
            }
        }

        return bestScore;
    }
    
}