using Game.UI;
using Game.Core;

namespace Tests.UI;

public class UIExtensionsTests
{
    [Test]
    public void Should_return_correct_image_from_cell_type()
    {
        // Arrange / Act / Assert
        CellType.Empty.ToImage().Should().Be(Images.Empty);
        CellType.Snake.ToImage().Should().Be(Images.Body);
        CellType.Food.ToImage().Should().Be(Images.Food);
    }

    [Test]
    public void Should_return_correct_rotation_from_direction()
    {
        // Arrange / Act / Assert
        Direction.Up.ToRotation().Should().Be(0);
        Direction.Right.ToRotation().Should().Be(90);
        Direction.Down.ToRotation().Should().Be(180);
        Direction.Left.ToRotation().Should().Be(270);
    }
}
