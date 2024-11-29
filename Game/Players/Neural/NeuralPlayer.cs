using Game.Core;

namespace Game.Players.Neural;

public static class NeuralPlayer
{
    public static void Decide(GameStateData data, Action<Direction> snakeGoTo)
    {
        var headPosition = data.HeadPosition;
        var foodPosition = data.FoodPosition;

        var velX = data.HeadDirection.VelX();
        var velY = data.HeadDirection.VelY();
        var deltaX = NeuralExtensions.AbsoluteDeltaX(headPosition, foodPosition);
        var deltaY = NeuralExtensions.AbsoluteDeltaY(headPosition, foodPosition);

        double[] inputs = [velX, velY, deltaX, deltaY];

        // DROP NewRandom
        var directions = NeuralNetwork.NewRandom(8).Calculate(inputs);

        foreach (var direction in directions)
        {
            var targetCell = data.CellAt(direction);
            if (targetCell == CellType.Empty || targetCell == CellType.Food)
            {
                snakeGoTo(direction);
                break;
            }
        }
    }
}
