using Game.Core;

namespace Game.Players.Star;

public class NodeBase
{
   public Position Position { get; set; }

   public int G { get; set; }
   public int H { get; set; }
   public int F => G + H;

   public bool Walkable { get; set; }

   public NodeBase Connection { get; set; }

   public List<NodeBase> Neighbors { get; set; }

   public NodeBase(Position position)
   {
      Position = position;
	  Neighbors = [];
   }

	public string GetDirection()
	{
		if (Connection == null) return "";
		if (Position.Row < Connection.Position.Row) return "U";
		if (Position.Row > Connection.Position.Row) return "D";
		if (Position.Column < Connection.Position.Column) return "L";
		return "R";
	}

	public override bool Equals(object? obj)
	{
		return obj is NodeBase node &&
			Position == node.Position;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Position.Row, Position.Column);
	}

	public static bool operator ==(NodeBase? left, NodeBase? right)
	{
		return EqualityComparer<NodeBase>.Default.Equals(left, right);
	}

	public static bool operator !=(NodeBase? left, NodeBase? right)
	{
		return !(left == right);
	}
}
