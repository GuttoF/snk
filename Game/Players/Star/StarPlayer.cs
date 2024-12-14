using Game.Core;

namespace Game.Players.Star;

public static class StarPlayer
{
    public static string Decide(GameStateData data, Action<Direction> snakeGoTo)
    {
        // Antes de passar o grid pro FindPath, ver se tem duas opcoes possiveis e se uma delas vai criar lacunas
        // Se uma criar e a outra n, na posicao q criar deve ser colocado um SnakeBody ou Outside
        // Isso sempre vai fazer a cobra ir pelo caminho que nao cria lacunas
        var rows = data.Grid.GetLength(0);
        var columns = data.Grid.GetLength(1);

        var percentage = data.SnakeSize * 1d / (rows * columns);

        var exists = new List<bool>();
        var directions = data.HeadPosition.GetStarDirections(rows, columns);
        var options = directions.Select(data.HeadPosition.MoveTo).Where(p => data.Grid[p.Row, p.Column] == 0).ToList();
        if (percentage > 0.40 && options.Count == 2)
        {
            foreach (var currentPosition in options)
            {
                if (currentPosition == data.FoodPosition)
                {
                    exists.Add(true);
                    continue;
                }

                data.Grid[currentPosition.Row, currentPosition.Column] = (int) CellType.SnakeBody;
                data.Grid[data.TailPosition.Row, data.TailPosition.Column] = (int) CellType.Empty;

                var allDirections = new List<Direction>() { Direction.Right, Direction.Down, Direction.Left, Direction.Up };
                var positions = allDirections.Select(currentPosition.MoveTo).Where(p => p.Row >= 0 && p.Column >= 0 && p.Row < rows && p.Column < columns).ToList();
                var neighbors = new List<Position>();
                foreach (var position in positions)
                {
                    var cell = data.Grid[position.Row, position.Column];
                    if (cell == 0 && position != data.TailPosition) neighbors.Add(position);
                }

                var pathExists = true;
                foreach (var neighbor in neighbors)
                {
                    if (pathExists)
                    {
                        pathExists = StarPathfinder.PathExists(data.Grid, neighbor, data.FoodPosition);
                    }
                }
                exists.Add(pathExists);
                data.Grid[currentPosition.Row, currentPosition.Column] = (int) CellType.Empty;
                data.Grid[data.TailPosition.Row, data.TailPosition.Column] = (int) CellType.SnakeTail;
            }

            if (!exists[0] && exists[1])
            {
                snakeGoTo(directions[1]);
                return directions[1].ToCharString();
            }
            if (!exists[1] && exists[0])
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
