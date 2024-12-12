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



        double[][] hidenNeurons =
        [
            [-681,  182,  -42,   95],
            [ 797,  598, -259, -374],
            [ -61,  549, -892, -755],
            [-759, -561,  576,  448],
            [   6,  346, -941, -107],
            [-996, -204, -509, -428],
            [ -42, -712,  775,  422],
            [-162, -564,  356, -227],
        ];
        double[][] outputNeurons =
        [
            [ 653,  919,  -45,  961,  226, 912,  -38, -565],
            [ 770, -136, -480, -183, -584, 106, -908, -810],
            [ 407, -970, -606,  224,  588, 740, -387, -259],
            [-680,  727,  765,  338,  -24, 340,  134,  620],
        ];



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
