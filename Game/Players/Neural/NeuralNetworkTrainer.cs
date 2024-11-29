// using System.IO;
// using System.Text.Json;
// using System.Diagnostics;

// namespace Game.Players.Neural;

// public static class NeuralNetworkTrainer
// {
//    public static void Run()
//    {
//       const int epochs = 1000;
//       const int populationSize = 50_000;

//       var timer = new Stopwatch();
//       timer.Start();

//       var scores = new List<int>();
//       var steps = new List<int>();

//       var games = new List<GameState>();
//       for (int i = 0; i < populationSize; i++)
//       {
//          games.Add(new GameState(10, 10));
//       }

//       for (int epoch = 0; epoch < epochs; epoch++)
//       {
//          // Botar pra jogar
//          Parallel.ForEach(games, game =>
//          {
//             while (!game.GameOver & !game.Zerou & game.Steps < 2_500)
//             {
//                if (game.Steps > 500 & game.Score < 10) break;
//                game.NeuralNetworkDecision();
//                game.MoveSnake();
//             }
//             game.NeuralNetwork.Score = game.Score;
//             game.NeuralNetwork.Steps = game.Steps;
//          });

//          // Ordenar redes segundo o score de cada uma
//          // Pras de mesmo score, ordenar as que precisaram de menos steps
//          var bests = games.OrderByDescending(x => x.Score).ThenBy(x => x.Steps).Select(x => x.NeuralNetwork).ToList();
//          var slice1 = bests.TakePercent(20);
//          var slice2 = bests.Skip(slice1.Count).ToList().TakePercent(40);

//          Console.WriteLine($"{epoch} | {bests.First().Score} | {bests.First().Steps}");
//          scores.Add(bests.First().Score);
//          steps.Add(bests.First().Steps);

//          // Montar nova lista de redes
//          var newNetworks = new List<NeuralNetwork>();

//          // - As 20% melhores passam pra nova
//          newNetworks.AddRange(slice1);

//          // - 40% das melhores restantes são cruzadas com as 20% melhores
//          for (int i = 0; i < slice1.Count; i++)
//          {
//             var a = slice1.OrderBy(x => Guid.NewGuid()).First();
//             var b = slice2.OrderBy(x => Guid.NewGuid()).First();
//             newNetworks.Add(SnakExtensions.Merge(a, b));
//          }

//          // - A lista é completada novamente com redes random
//          var missing = populationSize - newNetworks.Count;
//          for (int i = 0; i < missing; i++)
//          {
//             newNetworks.Add(NeuralNetwork.NewRandom(8));
//          }

//          games = [];
//          foreach (var net in newNetworks)
//          {
//             games.Add(new GameState(10, 10) { NeuralNetwork = net });
//          }
//       }

//       timer.Stop();

//       Console.WriteLine(JsonSerializer.Serialize(scores));
//       Console.WriteLine(JsonSerializer.Serialize(steps));

//       TimeSpan timeTaken = timer.Elapsed;
//       Console.WriteLine(">>>>> Duration: " + timeTaken.ToString(@"mm\:ss"));

//       var best = games.Select(x => x.NeuralNetwork).OrderByDescending(x => x.Score).ThenBy(x => x.Steps).First();
//       string jsonString = JsonSerializer.Serialize(best);
//       Console.WriteLine(jsonString);

//       string myfile = @"result.txt";
//       using StreamWriter sw = File.CreateText(myfile);
//       sw.WriteLine(JsonSerializer.Serialize(scores));
//       sw.WriteLine("---------");
//       sw.WriteLine(JsonSerializer.Serialize(steps));
//       sw.WriteLine("---------");
//       sw.WriteLine(jsonString);
//    }
// }
