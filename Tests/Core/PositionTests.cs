using Game.Core;

namespace Tests.Core;

public class PositionTests
{
    [Test]
    public void Should_create_correct_position()
    {
        // Arrange / Act
        var position = new Position(2, 3);

        // Assert
        position.Row.Should().Be(2);
        position.Column.Should().Be(3);
    }

    [Test]
    public void Should_move_to_up()
    {
        // Arrange
        var oldPosition = new Position(2, 2);

        // Act
        var newPosition = oldPosition.MoveTo(Direction.Up);

        // Assert
        newPosition.Row.Should().Be(1);
        newPosition.Column.Should().Be(2);
    }

    [Test]
    public void Should_move_to_right()
    {
        // Arrange
        var oldPosition = new Position(2, 2);

        // Act
        var newPosition = oldPosition.MoveTo(Direction.Right);

        // Assert
        newPosition.Row.Should().Be(2);
        newPosition.Column.Should().Be(3);
    }

    [Test]
    public void Should_move_to_down()
    {
        // Arrange
        var oldPosition = new Position(2, 2);

        // Act
        var newPosition = oldPosition.MoveTo(Direction.Down);

        // Assert
        newPosition.Row.Should().Be(3);
        newPosition.Column.Should().Be(2);
    }

    [Test]
    public void Should_move_to_left()
    {
        // Arrange
        var oldPosition = new Position(2, 2);

        // Act
        var newPosition = oldPosition.MoveTo(Direction.Left);

        // Assert
        newPosition.Row.Should().Be(2);
        newPosition.Column.Should().Be(1);
    }

    [Test]
    public void Should_equals_when_has_same_row_and_column()
    {
        // Arrange
        var positionA = new Position(2, 2);
        var positionB = new Position(2, 2);

        // Act
        var result = positionA.Equals(positionB);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Should_not_equals_when_has_diferent_row_or_column()
    {
        // Arrange
        var positionA = new Position(1, 2);
        var positionB = new Position(3, 4);

        // Act
        var result = positionA.Equals(positionB);

        // Assert
        result.Should().BeFalse();
    }
}
