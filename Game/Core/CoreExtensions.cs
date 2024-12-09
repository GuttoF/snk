namespace Game.Core;

public static class CoreExtensions
{
    public static bool IsSafe(this CellType cellType)
    {
        return cellType != CellType.SnakeBody && cellType != CellType.Outside;
    }
}
