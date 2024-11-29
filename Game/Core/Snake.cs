namespace Game.Core;

public class Snake
{
    public Direction HeadDirection { get; set; } = Direction.Right;
    public LinkedList<Position> CellsPositions { get; set; } = [];
	public LinkedList<Direction> Commands { get; set; } = [];

    public void Append(int row, int column)
    {
        CellsPositions.AddLast(new Position(row, column));
    }

    public void MoveTo(Position position)
    {
        CellsPositions.AddFirst(position);
    }

    public Direction GetHeadDirection()
    {
        return HeadDirection;
    }

    public Position GetHeadPosition()
    {
        return CellsPositions.First!.Value;
    }

    public Position GetTailPosition()
    {
        return CellsPositions.Last!.Value;
    }

    public void DropTail()
    {
        CellsPositions.RemoveLast();
    }

    private Direction GetLastDirection()
    {
        if (Commands.Count == 0) return HeadDirection;

        return Commands.Last!.Value;
    }

	private bool CanChangeDirection(Direction newDirection)
	{
		if (Commands.Count == 2) return false;

        var lastDirection = GetLastDirection();
		return newDirection != lastDirection && newDirection != lastDirection.Opposite();
	}

    public int Size()
    {
        return CellsPositions.Count;
    }

	public void GoTo(Direction direction)
    {
        if (CanChangeDirection(direction))
        {
            Commands.AddLast(direction);  
        }
    }

    public void ChangeDirection()
    {
        if (Commands.Count > 0)
        {
            HeadDirection = Commands.First!.Value;
            Commands.RemoveFirst();
        }
    }

    public Position NextHeadPosition()
    {
        return GetHeadPosition().MoveTo(HeadDirection);
    }
}
