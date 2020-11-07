using Game2048.Solving;
using System;
using System.Linq;
using Troschuetz.Random;

namespace Game2048.Random
{
    /// <summary>Returns a random move.</summary>
    public class RandomSolver : ISolver
	{
		private readonly IGenerator Rnd;

        public RandomSolver(IGenerator rnd) => Rnd = rnd;

        public MoveResult Move(Board board, TimeSpan duration)
			=> new MoveResult(Rnd
				.NextMoves()
				.FirstOrDefault(move => board.Move(move) != board));
	}
}
