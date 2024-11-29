namespace Game.Core;

public class Grid
{
    private readonly int _rows;
    private readonly int _columns;
    private readonly CellType[,] _cells;
    private readonly Random _random = new();
    private Position _foodPosition = null!;

	public Grid(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
        _cells = new CellType[rows, columns];
    }

    public Position GetFoodPosition()
    {
        return _foodPosition;
    }
    
    public CellType GetCellAt(Position position)
    {
        return _cells[position.Row, position.Column];
    }
    public CellType GetCellAt(int row, int column)
    {
        return _cells[row, column];
    }

    private void Put(int row, int column, CellType cellType)
    {
        _cells[row, column] = cellType;
    }
    private void Put(Position position, CellType cellType)
    {
        Put(position.Row, position.Column, cellType);
    }

    public void PutSnake(Position position)
    {
        Put(position, CellType.Snake);
    }
    public void PutSnake(int row, int column)
    {
        Put(row, column, CellType.Snake);
    }

    public void PutEmpty(Position position)
    {
        Put(position, CellType.Empty);
    }

    public int AddFood()
    {
        var emptyPositions = GetEmptyPositions();

        var total = emptyPositions.Count;
        if (total == 0) return 0;

        _foodPosition = emptyPositions[_random.Next(total)];
        _cells[_foodPosition.Row, _foodPosition.Column] = CellType.Food;

        return total;
    }

    private List<Position> GetEmptyPositions()
    {
        var positions = new List<Position>();
        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                if (_cells[row, column] == CellType.Empty)
                {
                    positions.Add(new(row, column));
                }
            }
        }
        return positions;
    }

    public bool IsOutside(Position position)
    {
        return position.Row < 0 || position.Row >= _rows || position.Column < 0 || position.Column >= _columns;
    }
}
