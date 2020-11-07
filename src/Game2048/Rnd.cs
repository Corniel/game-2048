using Troschuetz.Random;

namespace Game2048
{
    public static class Rnd
    {
		public static ulong NextMask(this IGenerator rnd, FreeCells cells)
		{
			var index = cells[rnd.Next(cells.Count)];
			ulong mask = rnd.NextCell() << (index * 4);
			return mask;
		}

		public static ulong NextCell(this IGenerator rnd)
			=> rnd.Next(10) == 0 ? 2UL : 1UL;


		public static Move[] NextMoves(this IGenerator rnd) => nextMoves[rnd.Next(24)];

		private static readonly Move[][] nextMoves = new Move[][]
		{
			new Move[]{ Move.Left, Move.Right, Move.Up, Move.Down },
			new Move[]{ Move.Left, Move.Right, Move.Down, Move.Up },
			new Move[]{ Move.Left, Move.Up, Move.Right, Move.Down },
			new Move[]{ Move.Left, Move.Up, Move.Down, Move.Right },
			new Move[]{ Move.Left, Move.Down, Move.Right, Move.Up },
			new Move[]{ Move.Left, Move.Down, Move.Up, Move.Right },

			new Move[]{ Move.Right, Move.Left, Move.Up, Move.Down },
			new Move[]{ Move.Right, Move.Left, Move.Down, Move.Up },
			new Move[]{ Move.Right, Move.Up, Move.Left, Move.Down },
			new Move[]{ Move.Right, Move.Up, Move.Down, Move.Left },
			new Move[]{ Move.Right, Move.Down, Move.Left, Move.Up },
			new Move[]{ Move.Right, Move.Down, Move.Up, Move.Left },

			new Move[]{ Move.Up, Move.Left, Move.Right, Move.Down },
			new Move[]{ Move.Up, Move.Left, Move.Down, Move.Right },
			new Move[]{ Move.Up, Move.Right, Move.Left, Move.Down },
			new Move[]{ Move.Up, Move.Right, Move.Down, Move.Left },
			new Move[]{ Move.Up, Move.Down, Move.Left, Move.Right },
			new Move[]{ Move.Up, Move.Down, Move.Right, Move.Left },

			new Move[]{ Move.Down, Move.Left, Move.Right, Move.Up },
			new Move[]{ Move.Down, Move.Left, Move.Up, Move.Right },
			new Move[]{ Move.Down, Move.Right, Move.Left, Move.Up },
			new Move[]{ Move.Down, Move.Right, Move.Up, Move.Left },
			new Move[]{ Move.Down, Move.Up, Move.Left, Move.Right },
			new Move[]{ Move.Down, Move.Up, Move.Right, Move.Left },
		};
	}
}
