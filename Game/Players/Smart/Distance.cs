using Game.Core;

namespace Game.Players.Smart;

public static class Distance
{
   public static double Euclidian(Position a, Position b)
   {
      return Math.Sqrt(Math.Pow(a.Row - b.Row, 2) + Math.Pow(a.Column - b.Column, 2));
   }
}