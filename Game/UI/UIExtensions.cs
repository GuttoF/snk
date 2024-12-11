using Game.Core;
using System.Windows.Media;

namespace Game.UI;

public static class UIExtensions
{
    public static ImageSource ToImage(this CellType value)
    {
        return value switch
        {
            CellType.Empty => Images.Empty,
            CellType.SnakeHead => Images.Head,
            CellType.SnakeBody => Images.Body,
            CellType.SnakeTail => Images.Tail,
            CellType.Food => Images.Food,
            _ => Images.Empty,
        };
    }

	private static readonly Dictionary<Direction, int> DirectionToRotation = new()
	{
		{ Direction.Up, 0 },
		{ Direction.Right, 90 },
		{ Direction.Down, 180 },
		{ Direction.Left, 270 },
	};
    public static int ToRotation(this Direction value)
    {
        return DirectionToRotation[value];
    }
}
