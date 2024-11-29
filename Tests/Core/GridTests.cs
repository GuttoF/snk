using Game.Core;

namespace Tests.Core;

public class GridTests
{
    [Test]
    public void Should_create_correct_grid()
    {
        // Arrange
        var rows = 3;
        var columns = 4;

        // Act
        var grid = new Grid(rows, columns);

        // Assert
        grid.GetCellAt(0, 0).Should().Be(CellType.Empty);
    }
}
