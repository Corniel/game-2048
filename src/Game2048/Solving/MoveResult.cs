using static System.FormattableString;

namespace Game2048.Solving
{
    public readonly struct MoveResult
    {
        public static readonly MoveResult None;

        public MoveResult(Move move) : this(move, default, 1) { }
        public MoveResult(Move move, double score, int simulations)
        {
            Move = move;
            Score = score;
            Simulations = simulations;
        }

        public Move Move { get; }
        public double Score { get; }
        public int Simulations { get; }

        public bool NoResult => Move == Move.None;

        public override string ToString()
        {
            return Invariant($"Move: {Move}, Score: {Score}, Sims: {Simulations:#,##0}");
        }
    }
}
