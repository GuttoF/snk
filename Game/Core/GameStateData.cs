namespace Game.Core;

public class GameStateData
{
    public int Rows { get; set; }
    public int Columns { get; set; }

    public Position HeadPosition { get; set; }
    public Direction HeadDirection { get; set; }

    public CellType HeadRight { get; set; }
    public CellType HeadDown { get; set; }
    public CellType HeadLeft { get; set; }
    public CellType HeadUp { get; set; }

    public Position FoodPosition { get; set; }

    public CellType CellAt(Direction direction)
    {
        if (direction == Direction.Right) return HeadRight;
        if (direction == Direction.Down) return HeadDown;
        if (direction == Direction.Left) return HeadLeft;
        return HeadUp;
    }
}
