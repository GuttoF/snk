using Game.Core;

namespace Game.Players.Star;

public static class StarPathfinder
{
    public static string FindPath(int[,] grid, int snakeSize, Position headPosition, Position tailPosition, Position foodPosition)
    {
        var path = FindPathTo(grid, snakeSize, headPosition, tailPosition, foodPosition);
        if (path == "NOT_FOUND")
            path = FindPathTo(grid, snakeSize, headPosition, tailPosition, tailPosition);

        return path;
    }

    private static string FindPathTo(int[,] grid, int snakeSize, Position headPosition, Position tailPosition, Position targetPosition)
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
            var directions = current.Position.GetStarDirection(rows, columns);
            var positions = directions.Select(current.Position.MoveTo).ToList();
            foreach (var position in positions)
            {
                var cell = grid[position.Row, position.Column];
                if (cell == 0 || (cell == 3 && snakeSize > 2) || (cell == 3 && snakeSize > 2 && target == tail) || cell == 4) current.Neighbors.Add(new NodeBase(position));
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
