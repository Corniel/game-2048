using Game2048;
using Game2048.MonteCarlo;
using NUnit.Framework;
using System;
using Troschuetz.Random.Generators;

namespace Monte_Carlo_specs
{
    public class Performance
	{
		[Test]
		public void GetMoveResult_Performance()
        {
			var rnd = new MT19937Generator(17);

			var board = Board.Initial(rnd);
			var solver = new MonteCarloSolver(rnd);

			var result = solver.Move(board, TimeSpan.FromSeconds(1));
			Console.WriteLine(result);
		}
	}
}
