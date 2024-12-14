using Game.Core;
using Game.Players.Star;

namespace Tests.Players.Star;

public class StarExtensionsTests
{
    [Test]
    public void Should_return_star_direction_on_0x0_cell()
    {
        // Arrange
        var position = new Position(0, 0);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_0x1_cell()
    {
        // Arrange
        var position = new Position(0, 1);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left]);
    }
    [Test]
    public void Should_return_star_direction_on_1x0_cell()
    {
        // Arrange
        var position = new Position(1, 0);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Right, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_1x1_cell()
    {
        // Arrange
        var position = new Position(1, 1);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Up, Direction.Right]);
    }

    [Test]
    public void Should_return_star_direction_on_0x2_cell()
    {
        // Arrange
        var position = new Position(0, 2);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_0x3_cell()
    {
        // Arrange
        var position = new Position(0, 3);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left]);
    }
    [Test]
    public void Should_return_star_direction_on_1x2_cell()
    {
        // Arrange
        var position = new Position(1, 0);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Right, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_1x3_cell()
    {
        // Arrange
        var position = new Position(1, 1);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Up, Direction.Right]);
    }

    [Test]
    public void Should_return_star_direction_on_0x4_cell()
    {
        // Arrange
        var position = new Position(0, 4);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_0x5_cell()
    {
        // Arrange
        var position = new Position(0, 5);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left]);
    }
    [Test]
    public void Should_return_star_direction_on_1x4_cell()
    {
        // Arrange
        var position = new Position(1, 4);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Right, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_1x5_cell()
    {
        // Arrange
        var position = new Position(1, 5);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Up]);
    }

    [Test]
    public void Should_return_star_direction_on_2x0_cell()
    {
        // Arrange
        var position = new Position(2, 0);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_2x1_cell()
    {
        // Arrange
        var position = new Position(2, 1);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Up]);
    }
    [Test]
    public void Should_return_star_direction_on_3x0_cell()
    {
        // Arrange
        var position = new Position(3, 0);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Right, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_3x1_cell()
    {
        // Arrange
        var position = new Position(3, 1);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Up, Direction.Right]);
    }

    [Test]
    public void Should_return_star_direction_on_2x2_cell()
    {
        // Arrange
        var position = new Position(2, 2);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_2x3_cell()
    {
        // Arrange
        var position = new Position(2, 3);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Up]);
    }
    [Test]
    public void Should_return_star_direction_on_3x2_cell()
    {
        // Arrange
        var position = new Position(3, 2);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Right, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_3x3_cell()
    {
        // Arrange
        var position = new Position(3, 3);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Up, Direction.Right]);
    }

    [Test]
    public void Should_return_star_direction_on_2x4_cell()
    {
        // Arrange
        var position = new Position(2, 4);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_2x5_cell()
    {
        // Arrange
        var position = new Position(2, 5);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Up]);
    }
    [Test]
    public void Should_return_star_direction_on_3x4_cell()
    {
        // Arrange
        var position = new Position(3, 4);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Right, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_3x5_cell()
    {
        // Arrange
        var position = new Position(3, 5);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Up]);
    }

    [Test]
    public void Should_return_star_direction_on_4x0_cell()
    {
        // Arrange
        var position = new Position(4, 0);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_4x1_cell()
    {
        // Arrange
        var position = new Position(4, 1);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Up]);
    }
    [Test]
    public void Should_return_star_direction_on_5x0_cell()
    {
        // Arrange
        var position = new Position(5, 0);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Right]);
    }
    [Test]
    public void Should_return_star_direction_on_5x1_cell()
    {
        // Arrange
        var position = new Position(5, 1);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Up, Direction.Right]);
    }

    [Test]
    public void Should_return_star_direction_on_4x2_cell()
    {
        // Arrange
        var position = new Position(4, 2);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_4x3_cell()
    {
        // Arrange
        var position = new Position(4, 1);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Up]);
    }
    [Test]
    public void Should_return_star_direction_on_5x2_cell()
    {
        // Arrange
        var position = new Position(5, 2);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Right]);
    }
    [Test]
    public void Should_return_star_direction_on_5x3_cell()
    {
        // Arrange
        var position = new Position(5, 3);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Up, Direction.Right]);
    }

    [Test]
    public void Should_return_star_direction_on_4x4_cell()
    {
        // Arrange
        var position = new Position(4, 4);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Down]);
    }
    [Test]
    public void Should_return_star_direction_on_4x5_cell()
    {
        // Arrange
        var position = new Position(4, 5);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Left, Direction.Up]);
    }
    [Test]
    public void Should_return_star_direction_on_5x4_cell()
    {
        // Arrange
        var position = new Position(5, 4);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Right]);
    }
    [Test]
    public void Should_return_star_direction_on_5x5_cell()
    {
        // Arrange
        var position = new Position(5, 5);
        // Act
        var directions = position.GetStarDirections(6, 6);
        // Assert
        directions.Should().BeEquivalentTo([Direction.Up]);
    }
}
