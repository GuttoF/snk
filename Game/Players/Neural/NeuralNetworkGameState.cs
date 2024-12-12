using Game.Core;

namespace Game.Players.Neural;

public class GameStateNN : GameState
{
    public NeuralNetwork NeuralNetwork { get; set; }

    public GameStateNN(int rows, int columns, GameMode mode) : base(rows, columns, mode)
    {
        NeuralNetwork = NeuralNetwork.NewRandom(8);
    }
}
