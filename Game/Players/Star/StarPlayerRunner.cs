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

        var game = new GameState(20, 20, GameMode.Classic);

        while (!game.GameOver & !game.Zerou)
        {
            StarPlayer.Decide(game.GetData(), game.SnakeGoTo);
            game.MoveSnake();
        }

        Console.WriteLine("--------------------");
        timer.Stop();

        TimeSpan timeTaken = timer.Elapsed;
        Console.WriteLine(">>>>> Duration: " + timeTaken.ToString(@"hh\:mm\:ss"));

        string myfile = @"result.txt";
        using StreamWriter sw = File.CreateText(myfile);
        sw.WriteLine(JsonSerializer.Serialize(game.Score));
        sw.WriteLine("---------");
        sw.WriteLine(JsonSerializer.Serialize(game.Steps));
        sw.WriteLine("---------");
        sw.WriteLine(JsonSerializer.Serialize(game.StepsPerFood));
        sw.WriteLine("---------");
        sw.WriteLine(timeTaken.ToString(@"hh\:mm\:ss"));
    }
}
