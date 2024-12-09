using Game.Core;

namespace Game.Players.Star;

public static class StarPlayer
{
    public static void Decide(GameStateData data, Action<Direction> snakeGoTo)
    {
        var headPosition = data.HeadPosition;

        var directions = headPosition.GetStarDirection(data.Rows, data.Columns);
        if (directions.Count == 1)
        {
            snakeGoTo(directions[0]); return;
        }

        if (data.CellAt(directions[0]) == CellType.SnakeBody)
        {
            snakeGoTo(directions[1]); return;
        }
        if (data.CellAt(directions[1]) == CellType.SnakeBody)
        {
            snakeGoTo(directions[0]); return;
        }






        // Como decidir entre duas direções livres? A*
        var firstPosition = headPosition.MoveTo(directions[0]);
        var secondPosition = headPosition.MoveTo(directions[1]);

        var foodPosition = data.FoodPosition;
        var firstDistance = Distance.Euclidian(firstPosition, foodPosition);
        var secondDistance = Distance.Euclidian(secondPosition, foodPosition);

        if (firstDistance < secondDistance)
        {
            snakeGoTo(directions[0]); return;
        }
        if (secondDistance < firstDistance)
        {
            snakeGoTo(directions[1]); return;
        }

        var random = new Random();
        snakeGoTo(directions[random.Next(2)]);
    }
}
