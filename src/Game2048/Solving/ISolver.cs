using System;

namespace Game2048.Solving
{
    public interface ISolver
	{
		MoveResult Move(Board board, TimeSpan duration);
	}
}
