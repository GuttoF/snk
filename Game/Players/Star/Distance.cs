using Game.Core;

namespace Game.Players.Star;

public static class Distance
{
   public static double Euclidian(Position a, Position b)
   {
      return Math.Sqrt(Math.Pow(a.Row - b.Row, 2) + Math.Pow(a.Column - b.Column, 2));
   }

   public static int Manhattan(Position a, Position b)
   {
      return Math.Abs(a.Row - b.Row) + Math.Abs(a.Column - b.Column);
   }
}
