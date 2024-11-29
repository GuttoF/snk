using Game.Core;

namespace Tests.Core;

public class DirectionTests
{
    [Test]
    public void Should_return_correct_up_direction()
    {
        // Arrange / Act
        var up = Direction.Up;

        // Assert
        up.X.Should().Be(0);
        up.Y.Should().Be(-1);
    }

    [Test]
    public void Should_return_correct_right_direction()
    {
        // Arrange / Act
        var right = Direction.Right;

        // Assert
        right.X.Should().Be(1);
        right.Y.Should().Be(0);
    }

    [Test]
    public void Should_return_correct_down_direction()
    {
        // Arrange / Act
        var down = Direction.Down;

        // Assert
        down.X.Should().Be(0);
        down.Y.Should().Be(1);
    }

    [Test]
    public void Should_return_correct_left_direction()
    {
        // Arrange / Act
        var left = Direction.Left;

        // Assert
        left.X.Should().Be(-1);
        left.Y.Should().Be(0);
    }

    [Test]
    public void Should_return_opposite_of_up()
    {
        // Arrange
        var up = Direction.Up;
        
        // Act
        var opposite = up.Opposite();

        // Assert
        opposite.Should().Be(Direction.Down);
    }

    [Test]
    public void Should_return_opposite_of_right()
    {
        // Arrange
        var right = Direction.Right;
        
        // Act
        var opposite = right.Opposite();

        // Assert
        opposite.Should().Be(Direction.Left);
    }

    [Test]
    public void Should_return_opposite_of_down()
    {
        // Arrange
        var down = Direction.Down;
        
        // Act
        var opposite = down.Opposite();

        // Assert
        opposite.Should().Be(Direction.Up);
    }

    [Test]
    public void Should_return_opposite_of_left()
    {
        // Arrange
        var left = Direction.Left;
        
        // Act
        var opposite = left.Opposite();

        // Assert
        opposite.Should().Be(Direction.Right);
    }

    [Test]
    public void Should_equals_when_has_same_x_and_y()
    {
        // Arrange
        var directionA = Direction.Left;
        var directionB = Direction.Left;

        // Act
        var result = directionA.Equals(directionB);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Should_not_equals_when_has_diferent_x_or_y()
    {
        // Arrange
        var directionA = Direction.Right;
        var directionB = Direction.Left;

        // Act
        var result = directionA.Equals(directionB);

        // Assert
        result.Should().BeFalse();
    }
}
