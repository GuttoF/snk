using Game.Core;

namespace Game.Players.Star;

public static class StarPlayer
{
    public static string Decide(GameStateData data, Action<Direction> snakeGoTo)
    {
        var rows = data.Grid.GetLength(0);
        var columns = data.Grid.GetLength(1);

        var percentage = data.SnakeSize * 1d / (rows * columns);

        var valids = new List<bool>();
        var directions = data.HeadPosition.GetStarDirections(rows, columns);
        var options = directions.Select(data.HeadPosition.MoveTo).Where(p => data.Grid[p.Row, p.Column] == 0).ToList();
        if (percentage > 0.10 && options.Count == 2)
        {
            foreach (var option in options)
            {
                data.Grid[option.Row, option.Column] = (int) CellType.SnakeBody;

                var foundGap = false;
                for (int row = 0; row < rows-1; row++)
                {
                    for (int column = 0; column < columns-1; column++)
                    {
                        var pos01 = new Position(row, column);
                        var pos02 = new Position(row, column+1);
                        var pos03 = new Position(row+1, column);
                        var pos04 = new Position(row+1, column+1);

                        if (data.Grid[pos01.Row, pos01.Column] != 0 ||
                            data.Grid[pos02.Row, pos02.Column] != 0 ||
                            data.Grid[pos03.Row, pos03.Column] != 0 ||
                            data.Grid[pos04.Row, pos04.Column] != 0)
                        {
                            continue;
                        }

                        var borders = new List<Position>()
                        {
                            pos01.MoveTo(Direction.Left), pos01.MoveTo(Direction.Up),
                            pos02.MoveTo(Direction.Up), pos02.MoveTo(Direction.Right),
                            pos03.MoveTo(Direction.Left), pos03.MoveTo(Direction.Down),
                            pos04.MoveTo(Direction.Right), pos04.MoveTo(Direction.Down),
                        };

                        var empty = false;
                        foreach (var border in borders)
                        {
                            if (border.IsInside(rows, columns))
                            {
                                empty = data.Grid[border.Row, border.Column] == 0;
                                if (empty) break;
                            }
                        }
                        if (empty) continue;

                        foundGap = true;
                    }
                    if (foundGap) break;
                }

                valids.Add(!foundGap);
                data.Grid[option.Row, option.Column] = (int) CellType.Empty;
            }

            if (!valids[0] && valids[1])
            {
                snakeGoTo(directions[1]);
                return directions[1].ToCharString();
            }
            if (!valids[1] && valids[0])
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
