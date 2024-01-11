namespace Day9;

public struct Move
{
    public int norm;
    public Direction direction;

    public Move(int norm,Direction direction)
    {
        this.norm = norm;
        this.direction = direction;
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None,
    }
}