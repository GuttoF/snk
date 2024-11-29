using Game.Core;
using Game.Players.Neural;

namespace Tests.Players.Neural;

public class NeuralExtensionsTests
{
    [Test]
    [TestCaseSource(typeof(NeuralTestData), nameof(NeuralTestData.VelXDirections))]
    public void Should_return_correct_value_for_vel_x_direction((Direction direction, double value) data)
    {
        // Arrange / Act
        var result = data.direction.VelX();
        
        // Assert
        result.Should().Be(data.value);
    }

    [Test]
    [TestCaseSource(typeof(NeuralTestData), nameof(NeuralTestData.VelYDirections))]
    public void Should_return_correct_value_for_vel_y_direction((Direction direction, double value) data)
    {
        // Arrange / Act
        var result = data.direction.VelY();
        
        // Assert
        result.Should().Be(data.value);
    }
}
