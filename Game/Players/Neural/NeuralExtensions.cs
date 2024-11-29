using Game.Core;

namespace Game.Players.Neural;

public static class NeuralExtensions
{
   public static double VelX(this Direction direction)
   {
      if (direction == Direction.Right) return 1.00;
      if (direction == Direction.Left) return -1.00;
      return 0.00;
   }

   public static double VelY(this Direction direction)
   {
      if (direction == Direction.Down) return 1.00;
      if (direction == Direction.Up) return -1.00;
      return 0.00;
   }

   public static double AbsoluteDeltaX(Position a, Position b)
   {
      if (b.Column > a.Column) return 1.00;
      if (b.Column < a.Column) return -1.00;
      return 0.00;
   }

   public static double AbsoluteDeltaY(Position a, Position b)
   {
      if (b.Row > a.Row) return 1.00;
      if (b.Row < a.Row) return -1.00;
      return 0.00;
   }









   public static double Multiply(double[] a, double[] b)
   {
      var value = 0d;
      for (int i = 0; i < a.Length; i++)
      {
         value += a[i] * b[i];
      }
      return value;
   }

   public static List<T> TakePercent<T>(this List<T> source, double percent)
   {
      int count = (int)(source.Count * percent / 100);
      return source.Take(count).ToList();
   }

   public static double[] Merge(double[] a, double[] b)
   {
      var random = new Random();
      var value = new double[a.Length];
      for (int i = 0; i < a.Length; i++)
      {
         value[i] = random.NextDouble() > 0.5 ? a[i] : b[i];
      }
      return value;
   }

   public static NeuralNetwork Merge(NeuralNetwork a, NeuralNetwork b)
   {
      var result = NeuralNetwork.NewRandom(8);

      for (int neuron = 0; neuron <= a.IntermediateNeurons.GetUpperBound(0); neuron++)
      {
         result.IntermediateNeurons[neuron] = Merge(a.IntermediateNeurons[neuron], b.IntermediateNeurons[neuron]);
      }

      for (int neuron = 0; neuron <= a.OutputNeurons.GetUpperBound(0); neuron++)
      {
         result.OutputNeurons[neuron] = Merge(a.OutputNeurons[neuron], b.OutputNeurons[neuron]);
      }

      return result;
   }
}
