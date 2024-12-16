using Game.Core;

namespace Game.Players.Star;

public static class StarPlayer
{
    public static string Decide(GameStateData data, Action<Direction> snakeGoTo)
    {
        var rows = data.Grid.GetLength(0);
        var columns = data.Grid.GetLength(1);

        var percentage = data.SnakeSize * 1d / (rows * columns);

        var counts = new List<int>();
        var directions = data.HeadPosition.GetStarDirections(rows, columns);
        var options = directions.Select(data.HeadPosition.MoveTo).Where(p => data.Grid[p.Row, p.Column] == 0).ToList();

        if (percentage > 0.20 && options.Count == 2)
        {
            foreach (var option in options)
            {
                data.Grid[option.Row, option.Column] = (int) CellType.SnakeBody;

                var count = StarEmptyCounter.Run(data.Grid, option);
                counts.Add(count);

                data.Grid[option.Row, option.Column] = (int) CellType.Empty;
            }

            var limit = rows;

            if (counts[0] <= limit && counts[1] > limit)
            {
                snakeGoTo(directions[1]);
                return directions[1].ToCharString();
            }
            if (counts[1] <= limit && counts[0] > limit)
            {
                snakeGoTo(directions[0]);
                return directions[0].ToCharString();
            }
        }

        var path = StarPathfinder.FindPath(data.Grid, data.HeadPosition, data.TailPosition, data.FoodPosition);

        snakeGoTo(path.First().ToDirection());

        return path;
    }
}
