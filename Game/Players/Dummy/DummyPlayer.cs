using Game.Core;

namespace Game.Players.Dummy;

public static class DummyPlayer
{
    public static void Decide(GameStateData data, Action<Direction> snakeGoTo)
    {
        var head = data.HeadPosition;
        var food = data.FoodPosition;

        if (head.Row < food.Row)
        {
            if (data.HeadDown.IsSafe())
            {
                snakeGoTo(Direction.Down); return;
            }
            if (data.HeadLeft.IsSafe())
            {
                snakeGoTo(Direction.Left); return;
            }
            if (data.HeadUp.IsSafe())
            {
                snakeGoTo(Direction.Up); return;
            }
            if (data.HeadRight.IsSafe())
            {
                snakeGoTo(Direction.Right); return;
            }
        }
        else if (head.Row > food.Row)
        {
            if (data.HeadUp.IsSafe())
            {
                snakeGoTo(Direction.Up); return;
            }
            if (data.HeadRight.IsSafe())
            {
                snakeGoTo(Direction.Right); return;
            }
            if (data.HeadDown.IsSafe())
            {
                snakeGoTo(Direction.Down); return;
            }
            if (data.HeadLeft.IsSafe())
            {
                snakeGoTo(Direction.Left); return;
            }
        }
        else if (head.Column < food.Column)
        {
            if (data.HeadRight.IsSafe())
            {
                snakeGoTo(Direction.Right); return;
            }
            if (data.HeadDown.IsSafe())
            {
                snakeGoTo(Direction.Down); return;
            }
            if (data.HeadLeft.IsSafe())
            {
                snakeGoTo(Direction.Left); return;
            }
            if (data.HeadUp.IsSafe())
            {
                snakeGoTo(Direction.Up); return;
            }
        }
        else if (head.Column > food.Column)
        {
            if (data.HeadLeft.IsSafe())
            {
                snakeGoTo(Direction.Left); return;
            }
            if (data.HeadUp.IsSafe())
            {
                snakeGoTo(Direction.Up); return;
            }
            if (data.HeadRight.IsSafe())
            {
                snakeGoTo(Direction.Right); return;
            }
            if (data.HeadDown.IsSafe())
            {
                snakeGoTo(Direction.Down); return;
            }
        }
    }
}
