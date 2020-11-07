using Game2048.MonteCarlo;
using Game2048.Random;
using Game2048.Solving;
using System;
using Troschuetz.Random;
using Troschuetz.Random.Generators;

namespace Game2048
{
	class Program
	{
		static void Main(string[] args)
		{
			var rnd = new MT19937Generator();

			while (true)
			{
				Console.WriteLine("[M]manual - [R]andom - Monte [C]arlo - [Q]uit");
				bool valid = false;
				while (!valid)
				{
					valid = true;
					switch (Console.ReadKey().Key)
					{
						case ConsoleKey.R: Play(rnd, new RandomSolver(rnd), TimeSpan.Zero, 100000, false, @"C:\code\game-2048\logs\randomsolver.log"); break;
						case ConsoleKey.C: Play(rnd, new MonteCarloSolver(rnd),TimeSpan.FromMilliseconds(50), 200, false, @"C:\code\game-2048\logs\MonteCarloSolver.log"); break;
						case ConsoleKey.M: PlayManual(rnd); break;
						case ConsoleKey.Q: return;
						default:
							valid = false; 
							break;
					}
				}
			}
		}

		private static void Play(IGenerator rnd, ISolver solver, TimeSpan movetime, int runs, bool showGames, string logfile)
		{
			var running = new RunCollection();

			Console.Clear();
			for (int i = 1; i <= runs; i++)
			{
				if (!showGames)
				{
					Console.Write("\r{0}", i);
				}
				var game = Game.Create(rnd);
				
				while (game.IsActive)
				{
					if (showGames)
					{
						Console.Clear();
						Console.WriteLine(game.Formatted());
					}
					var result = solver.Move(game.Current, movetime);
					
					if (result.NoResult) { break; }
					else
					{
						game.Move(result.Move, rnd);
					}
				}
				running.Apply(game.Current);

				if (showGames)
				{
					Console.Clear();
					Console.Write(game.Formatted());
					Console.WriteLine();
				}
			}
			Console.WriteLine();

			running.Save(logfile);
			running.Save(Console.Out);

			Console.WriteLine("Done");
		}

		private static void PlayManual(IGenerator rnd)
		{
			var game = Game.Create(rnd);

			while (game.IsActive)
			{
				Console.Clear();
				Console.WriteLine(game.Formatted());

				var key = Console.ReadKey();
				Move move = Move.Right;

				switch (key.Key)
				{
					case ConsoleKey.LeftArrow: move = Move.Left; break;
					case ConsoleKey.RightArrow: move = Move.Right; break;
					case ConsoleKey.DownArrow: move = Move.Down; break;
					case ConsoleKey.UpArrow: move = Move.Up; break;
				}
				game.Move(move, rnd);
			}
			Console.Clear();
			Console.Write(game.Formatted());
			Console.WriteLine();
			Console.WriteLine("Done");
		}
	}
}
