using Game.Core;

namespace Game.Players.Neural;

public class NeuralNetwork
{
    // Range: [-1000.00, 1000.00]
    public double[][] IntermediateNeurons { get; }
    public double[][] OutputNeurons { get; }

    public int Score { get; set; }
    public int Steps { get; set; }

    private double[] _intermediateOutput;
    private double[] _finalOutput;

    public NeuralNetwork(double[][] intermediateNeurons, double[][] outputNeurons)
    {
        IntermediateNeurons = intermediateNeurons;
        OutputNeurons = outputNeurons;
        _intermediateOutput = new double[IntermediateNeurons.GetUpperBound(0)+1];
        _finalOutput = new double[OutputNeurons.GetUpperBound(0)+1];;
    }

    public Direction[] Calculate(double[] inputs)
    {
        var intermediateMaxValue = (inputs.Length-1) * 1000d;
        for (int neuron = 0; neuron <= IntermediateNeurons.GetUpperBound(0); neuron++)
        {
            var value = NeuralExtensions.Multiply(inputs, IntermediateNeurons[neuron]);
            _intermediateOutput[neuron] = value / intermediateMaxValue;
        }

        var outputMaxValue = _intermediateOutput.Length * 1000d;
        for (int neuron = 0; neuron <= OutputNeurons.GetUpperBound(0); neuron++)
        {
            var value = NeuralExtensions.Multiply(_intermediateOutput, OutputNeurons[neuron]);
            _finalOutput[neuron] = value / outputMaxValue;
        }

        var outputs = new List<NNOutput>
        {
            new() { Direction = Direction.Right, Value = Math.Abs(_finalOutput[0]) },
            new() { Direction = Direction.Down, Value = Math.Abs(_finalOutput[1]) },
            new() { Direction = Direction.Left, Value = Math.Abs(_finalOutput[2]) },
            new() { Direction = Direction.Up, Value = Math.Abs(_finalOutput[3]) },
        };

        return outputs.OrderByDescending(x => x.Value).Select(x => x.Direction).ToArray();
    }

    public static NeuralNetwork NewRandom(int intermediateLayerSize)
    {
        var random = new Random();
        double NextDouble()
        {
            return random.NextDouble() * 2000 - 1000;
        }

        double[][] intermediateNeurons = new double[intermediateLayerSize][];
        for (int i = 0; i < intermediateLayerSize; i++)
        {
            intermediateNeurons[i] = new double[4];
            for (int j = 0; j < 4; j++)
            {
                intermediateNeurons[i][j] = NextDouble();
            }
        }

        double[][] outputNeurons = new double[4][];
        for (int i = 0; i < 4; i++)
        {
            outputNeurons[i] = new double[intermediateLayerSize];
            for (int j = 0; j < intermediateLayerSize; j++)
            {
                outputNeurons[i][j] = NextDouble();
            }
        }

        return new NeuralNetwork(intermediateNeurons, outputNeurons);
    }
}

public class NNOutput
{
    public double Value { get; set; }
    public required Direction Direction { get; set; }
}
