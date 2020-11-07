using Game2048.Solving;
using System;
using System.Diagnostics;
using Troschuetz.Random;

namespace Game2048.MonteCarlo
{
    public class MonteCarloSolver : ISolver
	{
		private Stopwatch sw = new Stopwatch();

		public MonteCarloSolver(IGenerator rnd) => Rnd = rnd;

		/// <summary>Gets the evaluator.</summary>
		private readonly IGenerator Rnd;

		public MoveResult Move(Board board, TimeSpan duration)
		{
			sw.Restart();

			var candidates = Candidates.FromBoard(board);
			if (candidates.HasMultiple())
			{
				while (sw.Elapsed < duration)
				{
                    foreach (var candidate in candidates)
                    {
                        Simulate(candidate);
                    }
                }
			}
			return candidates.Best();
		}

        private void Simulate(Candidate candidate)
        {
			var nodes = 0;
			var board = candidate.Board;
			int score;

			do
			{
				nodes++;
				score = board.Score;
                board = Next(board);
            }
            while (!board.IsEmpty());

            candidate.Add(score, nodes);
        }

		private Board Next(Board board)
        {
			board = board.FillEmptySpot(Rnd);

			if (board.IsEmpty())
			{
				return board;
			}
			else
			{
				foreach (var move in Rnd.NextMoves())
				{
					var next = board.Move(move);
					if (next != board)
					{
						return next;
					}
				}
				return Board.Empty;
			}
		}
    }
}
