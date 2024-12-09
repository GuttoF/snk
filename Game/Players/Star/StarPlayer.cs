using Game.Core;

namespace Game.Players.Star;

public static class StarPlayer
{
    public static string Decide(GameStateData data, Action<Direction> snakeGoTo)
    {
        var path = StarPathfinder.FindPath(data.Grid, data.HeadPosition, data.TailPosition, data.FoodPosition);
        Console.WriteLine(path);

        snakeGoTo(path.First().ToDirection());

        return path;
    }
}
