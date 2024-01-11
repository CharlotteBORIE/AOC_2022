using System.Numerics;

namespace Day9;

public class Grid
{
    public int height;
    public int width;
    public int startX;
    public int startY;
    public List<Move> moves = new List<Move>();
    public bool[,] grid;
    public bool[,] grid2;
    public Vector2[] knots = new Vector2[9];



    public Grid(string[] read)
    {
        var maxUp = 0;
        var maxDown = 0;
        var maxLeft=0;
        var maxRight = 0;
        
        foreach (var line in read)
        {
            var infos = line.Split(" ");
            int norm = Int32.Parse(infos[1]);
            switch (infos[0])
            {
                case "L":
                    width+=norm;
                    if (width > maxLeft)
                    {
                        maxLeft = width;
                    }
                    moves.Add(new Move(norm,Move.Direction.Left));
                    break;
                case "R":
                    width-=norm;
                    if (width < maxRight)
                    {
                        maxRight = width;
                    }
                    moves.Add(new Move(norm,Move.Direction.Right));
                    break;
                case "U":
                    height+=norm;
                    if (height > maxUp)
                    {
                        maxUp = height;
                    }
                    moves.Add(new Move(norm,Move.Direction.Up));
                    break;
                case "D":
                    height-=norm;
                    if (height < maxDown)
                    {
                        maxDown = height;
                    }
                    moves.Add(new Move(norm,Move.Direction.Down));
                    break;
                default:
                    throw new Exception("Not good read");
            }
        }

        height = Math.Abs(maxUp - maxDown);
        width = Math.Abs(maxLeft - maxRight);
        startX = -maxRight;
        startY = -maxDown;
        grid = new bool[Math.Abs(height)+1, Math.Abs(width)+1];
        grid2 = new bool[Math.Abs(height)+1, Math.Abs(width)+1];
        
    }

    public void GetMovesTail()
    {
        var positionTail = new Vector2(startX,startY);
        var positionHead = new Vector2(startX,startY);
        grid2[(int)positionHead.Y,(int) positionHead.X] = true;

        foreach (var move in moves)
        {
            MoveRope(ref positionHead,ref positionTail,move);
        }
        positionTail = GetNewPositionTail(positionHead, positionTail);
        grid[(int)positionTail.Y,(int) positionTail.X] = true;
    }
    
    public void GetMovesRope()
    {
        for (int i = 0; i < 9; i++)
        {
            knots[i] = new Vector2(startX,startY);
        }
        var positionHead = new Vector2(startX,startY);
        grid2[(int)positionHead.Y,(int) positionHead.X] = true;

        foreach (var move in moves)
        {
            MoveRope(ref positionHead,move);
        }
        GetNewPositionRope(positionHead);
        grid[(int)knots[8].Y,(int) knots[8].X] = true;
    }

    private void MoveRope(ref Vector2 positionHead, ref Vector2 positionTail, Move move)
    {
        for (int i = 0; i < move.norm; i++)
        {
            positionHead = GetNewPositionHead(positionHead,move.direction);
            grid2[(int)positionHead.Y,(int) positionHead.X] = true;
            
            positionTail = GetNewPositionTail(positionHead, positionTail);
            grid[(int)positionTail.Y,(int) positionTail.X] = true;
            
        }
    }
    
    private void MoveRope(ref Vector2 positionHead, Move move)
    {
        for (int i = 0; i < move.norm; i++)
        {
            positionHead = GetNewPositionHead(positionHead,move.direction);
            grid2[(int)positionHead.Y,(int) positionHead.X] = true;
            
            GetNewPositionRope(positionHead);
            grid[(int)knots[8].Y,(int) knots[8].X] = true;
            
        }
    }
    
    private void GetNewPositionRope(Vector2 positionHead)
    {
        knots[0] = GetNewPositionTail(positionHead, knots[0]);
        for (var index = 1; index < knots.Length; index++)
        {

            knots[index] = GetNewPositionTail(knots[index - 1], knots[index]);
        }
    }

    private Vector2 GetNewPositionTail(Vector2 positionHead, Vector2 positionTail)
    {
        var distHorizontal = positionHead.X - positionTail.X;
        var distVertical = positionHead.Y - positionTail.Y;

        var resX = positionTail.X;
        var resY = positionTail.Y;
        
        if (distHorizontal != 0 && distVertical != 0 && 
            (Math.Abs(distHorizontal)> 1 || Math.Abs(distVertical)>1))
        {
            resX += distHorizontal > 0 ? 1 : -1;
            resY += distVertical > 0 ? 1 : -1;
            return new Vector2(resX, resY);
        }

        if (Math.Abs(distHorizontal)>1)
        {
            resX+=distHorizontal>0?1:-1;
        }
        if (Math.Abs(distVertical)>1)
        {
            resY+=distVertical>0?1:-1;
        }

        return new Vector2(resX, resY);
    }

    private Vector2 GetNewPositionHead(Vector2 pos,Move vec)
    {
        float positionHeadX = pos.X;
        float positionHeadY = pos.Y;
        switch (vec.direction)
        {
            case Move.Direction.Down:
                positionHeadY -= vec.norm;
                break;
            case Move.Direction.Up:
                positionHeadY += vec.norm;
                break;
            case Move.Direction.Left:
                positionHeadX += vec.norm;
                break;
            case Move.Direction.Right:
                positionHeadX -= vec.norm;
                break;
        }

        return new Vector2(positionHeadX,positionHeadY);
    }
    
    private Vector2 GetNewPositionHead(Vector2 pos, Move.Direction direction)
    {
            float positionHeadX = pos.X;
            float positionHeadY = pos.Y;
            switch (direction)
            {
                case Move.Direction.Down:
                    positionHeadY --;
                    break;
                case Move.Direction.Up:
                    positionHeadY ++;
                    break;
                case Move.Direction.Left:
                    positionHeadX ++;
                    break;
                case Move.Direction.Right:
                    positionHeadX --;
                    break;
            }

            return new Vector2(positionHeadX,positionHeadY);
        
    }

    public int CountTailPositions()
    {
        // 1649 too low
        int count = 0;
        foreach (var space in grid)
        {
            if (space)
            {
                count++;
            }
        }

        return count;
    }
    
    public int CountHeadPositions()
    {
        // 1649 too low
        int count = 0;
        foreach (var space in grid2)
        {
            if (space)
            {
                count++;
            }
        }

        return count;
    }
}