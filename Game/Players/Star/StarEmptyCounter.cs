using Game.Core;

namespace Game.Players.Star;

public static class StarEmptyCounter
{
    public static int Run(int[,] grid, Position startPostion)
    {
        var rows = grid.GetLength(0);
        var columns = grid.GetLength(1);

        var startNode = new NodeBase(startPostion);
        var neighbors = new List<NodeBase>();

        var directions = new List<Direction>() { Direction.Right, Direction.Down, Direction.Left, Direction.Up };
        var positions = directions.Select(startPostion.MoveTo).Where(p => p.Row >= 0 && p.Column >= 0 && p.Row < rows && p.Column < columns).ToList();
        foreach (var position in positions)
        {
            var cell = grid[position.Row, position.Column];
            if (cell == 0 || cell == 4) neighbors.Add(new NodeBase(position));
        }

        var min = int.MaxValue;
        foreach (var neighbor in neighbors)
        {
            var count = GetCount(grid, neighbor);
            min = count < min ? count : min;
        }

        return min;
    }

    private static int GetCount(int[,] grid, NodeBase startNode)
    {
        var rows = grid.GetLength(0);
        var columns = grid.GetLength(1);

        var toSearch = new List<NodeBase>() { startNode };
        var processed = new List<NodeBase>();

        while (true)
        {
            if (toSearch.Count == 0)
            {
                return processed.Count;
            }

            if (processed.Count >= rows + columns)
            {
                return processed.Count;
            }

            var current = toSearch[0];

            processed.Add(current);
            toSearch.Remove(current);

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
                if (!inSearch)
                {
                    toSearch.Add(neighbor);
                }
            }
        }
    }
}
