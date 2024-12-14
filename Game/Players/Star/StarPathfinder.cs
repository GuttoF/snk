using Game.Core;

namespace Game.Players.Star;

public static class StarPathfinder
{
    public static string FindPath(int[,] grid, Position headPosition, Position tailPosition, Position foodPosition)
    {
        var path = FindPathTo(grid, headPosition, tailPosition, foodPosition);
        if (path == "NOT_FOUND")
            path = FindPathTo(grid, headPosition, tailPosition, tailPosition);

        return path;
    }

    private static string FindPathTo(int[,] grid, Position headPosition, Position tailPosition, Position targetPosition)
    {
        var rows = grid.GetLength(0);
        var columns = grid.GetLength(1);

        var head = new NodeBase(headPosition);
        var tail = new NodeBase(tailPosition);

        var target = new NodeBase(targetPosition);
        var toSearch = new List<NodeBase>() { head };
        var processed = new List<NodeBase>();

        while (true)
        {
            if (toSearch.Count == 0)
            {
                return "NOT_FOUND";
            }

            var current = toSearch[0];

            // Get the best node, with lower F or then lower H
            // USE PRIORITY QUEUE
            foreach (var node in toSearch)
            {
                if (node.F < current.F || node.F == current.F && node.H < current.H) current = node;
            }

            processed.Add(current);
            toSearch.Remove(current);

            if (current == target)
            {
                var path = "";
                var node = current;
                while (node != head)
                {
                    var dir = node.GetDirection();
                    path += dir;
                    node = node.Connection;
                }
                return new string(path.Reverse().ToArray());
            }

            // Calculate neighbors
            var directions = current.Position.GetStarDirections(rows, columns);
            var positions = directions.Select(current.Position.MoveTo).ToList();
            foreach (var position in positions)
            {
                var cell = grid[position.Row, position.Column];
                if (cell == 0 || (cell == 3 && target == tail) || cell == 4) current.Neighbors.Add(new NodeBase(position));
            }

            foreach (var neighbor in current.Neighbors.Where(t => !processed.Contains(t)))
            {
                var inSearch = toSearch.Contains(neighbor);

                var costToNeighbor = current.G + 1;

                if (!inSearch || costToNeighbor < neighbor.G) {
                    neighbor.G = costToNeighbor;
                    neighbor.Connection = current;

                    if (!inSearch) {
                        neighbor.H = Distance.Manhattan(neighbor.Position, target.Position);
                        toSearch.Add(neighbor);
                    }
                }
            }
        }
    }

    public static (bool, int) PathExists(int[,] grid, Position startPosition, Position targetPosition)
    {
        var rows = grid.GetLength(0);
        var columns = grid.GetLength(1);

        var start = new NodeBase(startPosition);
        var target = new NodeBase(targetPosition);

        var toSearch = new List<NodeBase>() { start };
        var processed = new List<NodeBase>();

        while (true)
        {
            if (toSearch.Count == 0)
            {
                return (false, processed.Count);
            }

            var current = toSearch[0];

            // Get the best node, with lower F or then lower H
            // USE PRIORITY QUEUE
            foreach (var node in toSearch)
            {
                if (node.F < current.F || node.F == current.F && node.H < current.H) current = node;
            }

            processed.Add(current);
            toSearch.Remove(current);

            if (current == target)
            {
                return (true, processed.Count);
            }

            // Calculate neighbors
            var directions = new List<Direction>() { Direction.Right, Direction.Down, Direction.Left, Direction.Up };
            var positions = directions.Select(current.Position.MoveTo).Where(p => p.Row >= 0 && p.Column >= 0 && p.Row < rows && p.Column < columns).ToList();
            foreach (var position in positions)
            {
                var cell = grid[position.Row, position.Column];
                if (cell == 0 || cell == 4) current.Neighbors.Add(new NodeBase(position));
            }

            foreach (var neighbor in current.Neighbors.Where(t => !processed.Contains(t)))
            {
                var inSearch = toSearch.Contains(neighbor);

                var costToNeighbor = current.G + 1;

                if (!inSearch || costToNeighbor < neighbor.G) {
                    neighbor.G = costToNeighbor;
                    neighbor.Connection = current;

                    if (!inSearch) {
                        neighbor.H = Distance.Manhattan(neighbor.Position, target.Position);
                        toSearch.Add(neighbor);
                    }
                }
            }
        }
    }
}
