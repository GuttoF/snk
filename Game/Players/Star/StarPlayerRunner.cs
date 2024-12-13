using Game.Core;
using System.IO;
using System.Text.Json;
using System.Diagnostics;

namespace Game.Players.Star;

public static class StarPlayerRunner
{
    public static void Run()
    {
        var timer = new Stopwatch();
        timer.Start();

        var game = new GameState(30, 30, GameMode.Classic);

        while (!game.GameOver & !game.Zerou)
        {
            StarPlayer.Decide(game.GetData(), game.SnakeGoTo);
            game.MoveSnake();
            if (game.Steps % 10_000 == 0) Console.WriteLine($"{game.Score} | {game.Steps}");
        }

        Console.WriteLine("--------------------");
        timer.Stop();

        Console.WriteLine(JsonSerializer.Serialize(game.Score));
        Console.WriteLine(JsonSerializer.Serialize(game.Steps));

        TimeSpan timeTaken = timer.Elapsed;
        Console.WriteLine(">>>>> Duration: " + timeTaken.ToString(@"mm\:ss"));

        string myfile = @"result.txt";
        using StreamWriter sw = File.CreateText(myfile);
        sw.WriteLine(JsonSerializer.Serialize(game.Score));
        sw.WriteLine("---------");
        sw.WriteLine(JsonSerializer.Serialize(game.Steps));
    }
}
