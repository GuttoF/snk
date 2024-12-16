using Game.Players.Star;

namespace Tests.Core;

public class StarEmptyCounterTests
{
    [Test]
    public void Should_get_correct_empty_counter_for_8x8_with_2x2_hole()
    {
        // Arrange
        int[,] grid =
        {
            {2, 2, 2, 2, 2, 2, 2, 2},
            {2, 0, 0, 2, 2, 2, 2, 2},
            {2, 0, 0, 2, 2, 2, 2, 2},
            {2, 2, 2, 0, 0, 2, 2, 2},
            {0, 0, 0, 0, 0, 2, 2, 2},
            {0, 0, 0, 0, 2, 2, 2, 2},
            {0, 0, 0, 0, 2, 2, 2, 2},
            {0, 0, 4, 0, 2, 2, 2, 2},
        };

        // Act
        var count = StarEmptyCounter.Run(grid, new(3, 2));

        // Assert
        count.Should().Be(4);
    }

    [Test]
    public void Should_get_correct_empty_counter_for_8x8_without_2x2_hole()
    {
        // Arrange
        int[,] grid =
        {
            {2, 2, 2, 2, 2, 2, 2, 2},
            {2, 0, 0, 2, 2, 2, 2, 2},
            {2, 2, 0, 2, 2, 2, 2, 2},
            {2, 2, 0, 0, 0, 2, 2, 2},
            {0, 0, 0, 0, 0, 2, 2, 2},
            {0, 0, 0, 0, 2, 2, 2, 2},
            {0, 0, 0, 0, 2, 2, 2, 2},
            {0, 0, 4, 0, 2, 2, 2, 2},
        };

        // Act
        var count = StarEmptyCounter.Run(grid, new(2, 1));

        // Assert
        count.Should().Be(8);
    }
}
