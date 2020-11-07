using Game2048.Solving;
using System.Collections.Generic;
using System.Linq;

namespace Game2048.MonteCarlo
{
    public sealed class Candidates : List<Candidate>
    {
        public int Nodes => this.Sum(c => c.Nodes);

        public bool HasMultiple() => Count > 1;

        public MoveResult Best()
        {
            if (Count == 0) return MoveResult.None;
            {
                Sort();
                var best = this[0];
                return new MoveResult(best.Move, best.Score, Nodes);
            }
        }

        public static Candidates FromBoard(Board board)
        {
            var candidates = new Candidates();

            foreach (var move in Moves.All)
            {
                var moved = board.Move(move);
                if (moved != board)
                {
                    candidates.Add(new Candidate(move, moved));
                }
            }
            return candidates;
        }
    }
}
