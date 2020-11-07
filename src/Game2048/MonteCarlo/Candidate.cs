using System;
using System.Diagnostics;
using static System.FormattableString;

namespace Game2048.MonteCarlo
{
    public class Candidate : IComparable<Candidate>
    {
        public Candidate(Move move, Board board)
        {
            Move = move;
            Board = board;
        }
        public Move Move { get; }
        public Board Board { get; }

        public int Simulations { get; private set; }
        public int Nodes { get; private set; }

        public double Score => Board.Score +
            (Simulations == 0 ? 0 : scores / (double)Simulations);
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long scores;

        public void Add(int score, int nodes)
        {
            Simulations++;
            scores += score;
            Nodes += nodes;
        }

        public override string ToString() => Invariant($"{Move}: {Score:#,##0.0} ({Simulations:#,##0})");

        public int CompareTo(Candidate other) => other.Score.CompareTo(Score);
    }
}
