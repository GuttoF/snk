using Game.Core;

namespace Tests.Core;

public class GameStateTests
{
    [Test]
    public void Should_create_correct_game_state()
    {
        // Arrange
        var rows = 3;
        var columns = 4;

        // Act
        var state = new GameState(rows, columns);

        // Assert
        state.Rows.Should().Be(rows);
        state.Columns.Should().Be(columns);
        state.Grid.GetCellAt(1, 1).Should().Be(CellType.SnakeBody);
        state.Grid.GetCellAt(1, 2).Should().Be(CellType.SnakeBody);
        state.Snake.HeadDirection.Should().Be(Direction.Right);
    }

    [Test]
    public void Should_get_correct_game_state_data()
    {
        // Arrange
        var rows = 3;
        var columns = 4;

        var state = new GameState(rows, columns);

        // Act
        var data = state.GetData();

        // Assert
        data.Rows.Should().Be(rows);
        data.Columns.Should().Be(columns);
        data.Grid[1, 1].Should().Be(3);
        data.Grid[1, 2].Should().Be(1);
    }
}
