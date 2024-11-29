using Game.Core;

namespace Tests.Players.Neural;

public static class NeuralTestData
{
   public static IEnumerable<object[]> VelXDirections()
   {
      foreach (var value in new List<(Direction, double)>()
      {
         (Direction.Right, 1.00),
         (Direction.Down, 0.00),
         (Direction.Left, -1.00),
         (Direction.Up, 0.00),
      })
      {
         yield return [value];
      }
   }

   public static IEnumerable<object[]> VelYDirections()
   {
      foreach (var value in new List<(Direction, double)>()
      {
         (Direction.Right, 0.00),
         (Direction.Down, 1.00),
         (Direction.Left, 0.00),
         (Direction.Up, -1.00),
      })
      {
         yield return [value];
      }
   }
}
