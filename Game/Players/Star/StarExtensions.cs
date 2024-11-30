using Game.Core;

namespace Game.Players.Star;

public static class StarExtensions
{
    public static List<Direction> GetStarDirection(this Position position, int rows, int columns)
    {
        var row = position.Row;
        var column = position.Column;

        if (row % 2 == 0)
        {
            if (column % 2 == 0)
            {
                if (column == 0) return [Direction.Down];
                return [Direction.Left, Direction.Down];
            }

            if (row == 0) return [Direction.Left];
            return [Direction.Left, Direction.Up];
        }

        if (column % 2 == 0)
        {
            if (row == rows-1) return [Direction.Right];
            return [Direction.Right, Direction.Down];
        }

        if (column == columns-1) return [Direction.Up];
        return [Direction.Up, Direction.Right];
    }
}