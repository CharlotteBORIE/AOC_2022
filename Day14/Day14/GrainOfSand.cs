using System.Numerics;

namespace Day14;

public struct GrainOfSand
{
    public Vector2 position;
    public bool isResting;
    public bool isAtBottom;

    public GrainOfSand(Vector2 start)
    {
        position = start;
        isResting = false;
        isAtBottom = false;
    }

    public bool MoveGrainOnce(bool[,] grid)
    {
        int posX = (int)position.X;
        var posY = (int)position.Y;
        
        //test bottom
        if (posY == grid.GetUpperBound(1))
        {
            isResting = true;
            isAtBottom = true;
            return isResting;
        }
        
        //can go down
        if (!grid[posX, posY + 1])
        {
            position = new Vector2(posX, posY + 1);
        }
        else if (posX-1>=0 && !grid[posX-1, posY + 1])
        {
            position = new Vector2(posX-1, posY + 1);
        }
        else if (posX+1< grid.GetUpperBound(0) &&!grid[posX+1, posY + 1])
        {
            position = new Vector2(posX+1, posY + 1);
        }
        else
        {
            isResting = true;
        }
        return isResting;
    }
    
}