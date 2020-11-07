namespace Game2048
{
    public enum Move
	{
		None = 0,
		Left = 1,
		Right = 2,
		Up = 3,
		Down = 4,
	}
	public class Moves
	{
		public static readonly Move[] All = new Move[] { Move.Left, Move.Right, Move.Up, Move.Down };
	}
}
