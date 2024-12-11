using Game.Core;

namespace Tests.Core;

public class CoreExtensionsTests
{
    [Test]
    [TestCase(CellType.Empty)]
    [TestCase(CellType.SnakeTail)]
    [TestCase(CellType.Food)]
    public void Should_return_true_when_cell_is_safe_for_snake_head_access(CellType cellType)
    {
        // Arrange / Act
        var isSafe = cellType.IsSafe();

        // Assert
        isSafe.Should().BeTrue();
    }

    [Test]
    [TestCase(CellType.SnakeBody)]
    [TestCase(CellType.Outside)]
    public void Should_return_false_when_cell_is_not_safe_for_snake_head_access(CellType cellType)
    {
        // Arrange / Act
        var isSafe = cellType.IsSafe();

        // Assert
        isSafe.Should().BeFalse();
    }
}
