using Game.Core;

namespace Game.Players.Star;

public static class StarPlayer
{
    public static void Decide(GameStateData data, Action<Direction> snakeGoTo)
    {
        var headPosition = data.HeadPosition;

        var directions = headPosition.GetStarDirection(data.Rows, data.Columns);
        if (directions.Count == 1)
        {
            snakeGoTo(directions[0]); return;
        }

        if (data.CellAt(directions[0]) == CellType.SnakeBody)
        {
            snakeGoTo(directions[1]); return;
        }
        if (data.CellAt(directions[1]) == CellType.SnakeBody)
        {
            snakeGoTo(directions[0]); return;
        }

        var path = StarPathfinder.FindPath(data.Grid, data.HeadPosition, data.TailPosition, data.FoodPosition);

        Console.WriteLine(path);

        if (path.First() == 'R') snakeGoTo(Direction.Right);
        if (path.First() == 'D') snakeGoTo(Direction.Down);
        if (path.First() == 'L') snakeGoTo(Direction.Left);
        if (path.First() == 'U') snakeGoTo(Direction.Up);
    }
}
