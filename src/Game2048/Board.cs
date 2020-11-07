using System.Runtime.InteropServices;
using Troschuetz.Random;
using static System.FormattableString;

namespace Game2048
{
    /// <summary>Represents a board for the 2048 game.</summary>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public readonly struct Board
    {
        public static readonly Board Empty;
        private Board(Row r0, Row r1, Row r2, Row r3, int score)
        {
            Bits = default;
            R0 = r0;
            R1 = r1;
            R2 = r2;
            R3 = r3;
            Score = score;
        }
        private Board(ulong bits, int score)
        {
            R0 = default;
            R1 = default;
            R2 = default;
            R3 = default;
            Bits = bits;
            Score = score;
        }

        /// <summary>The underlying value.</summary>
        /// <remarks>
        /// 
        /// o----o----o----o----o
        /// | d3 | d2 | d1 | d0 |
        /// o----o----o----o----o
        /// | c3 | c2 | c1 | c0 |
        /// o----o----o----o----o
        /// | b3 | b2 | b1 | b0 |
        /// o----o----o----o----o
        /// | a3 | a2 | a1 | a0 |
        /// o----o----o----o----o
        /// </remarks>
        [FieldOffset(0)]
        private readonly ulong Bits;

        [FieldOffset(6)]
        public readonly Row R0;
        [FieldOffset(4)]
        public readonly Row R1;
        [FieldOffset(2)]
        public readonly Row R2;
        [FieldOffset(0)]
        public readonly Row R3;

        [FieldOffset(8)]
        public readonly int Score;

        public bool IsActive
            => FreeCells.Any(this)
            || this != MoveLeft()
            || this != MoveRight()
            || this != MoveUp()
            || this != MoveDown();

        /// <summary>Get the highest value.</summary>
        public int MaxValue
        {
            get
            {
                ulong max = 0;

                for (int i = 0; i < 16; i++)
                {
                    var test = Value.FromBits(Bits, i);
                    if (test > max)
                    {
                        max = test;
                    }
                }
                return Value.FromCell((int)max);
            }
        }

        public bool IsEmpty() => Bits == default;

        public Board Move(Move move)
        {
            Board board = this;

            switch (move)
            {
                case Game2048.Move.Left: board = board.MoveLeft(); break;
                case Game2048.Move.Right: board = board.MoveRight(); break;
                case Game2048.Move.Up: board = board.MoveUp(); break;
                case Game2048.Move.Down: board = board.MoveDown(); break;
                default: break;
            }
            return board;
        }
      
        public Board MoveLeft()
        {
            var r0 = R0.MoveLeft();
            var r1 = R1.MoveLeft();
            var r2 = R2.MoveLeft();
            var r3 = R3.MoveLeft();

            var score = Score
                + R0.ScoreRight()
                + R1.ScoreRight()
                + R2.ScoreRight()
                + R3.ScoreRight();

            return new Board(r0, r1, r2, r3, score);
        }

        public Board MoveRight()
        {
            var r0 = R0.MoveRight();
            var r1 = R1.MoveRight();
            var r2 = R2.MoveRight();
            var r3 = R3.MoveRight();
            
            var score = Score
                + R0.ScoreRight()
                + R1.ScoreRight()
                + R2.ScoreRight()
                + R3.ScoreRight();

            return new Board(r0, r1, r2, r3, score);
        }

        public Board MoveUp() => RotateLeft().MoveLeft().RotateRight();
        
        public Board MoveDown() => RotateLeft().MoveRight().RotateRight();
        
        public Board RotateLeft()
            => new Board(0
            | Bits.Move(00, 12)
            | Bits.Move(01, 08)
            | Bits.Move(02, 04)
            | Bits.Move(03, 00)

            | Bits.Move(04, 13)
            | Bits.Move(05, 09)
            | Bits.Move(06, 05)
            | Bits.Move(07, 01)

            | Bits.Move(08, 14)
            | Bits.Move(09, 10)
            | Bits.Move(10, 06)
            | Bits.Move(11, 02)

            | Bits.Move(12, 15)
            | Bits.Move(13, 11)
            | Bits.Move(14, 07)
            | Bits.Move(15, 03),
            Score);

        public Board RotateRight()
            => new Board(0
            | Bits.Move(12, 00)
            | Bits.Move(08, 01)
            | Bits.Move(04, 02)
            | Bits.Move(00, 03)

            | Bits.Move(13, 04)
            | Bits.Move(09, 05)
            | Bits.Move(05, 06)
            | Bits.Move(01, 07)

            | Bits.Move(14, 08)
            | Bits.Move(10, 09)
            | Bits.Move(06, 10)
            | Bits.Move(02, 11)

            | Bits.Move(15, 12)
            | Bits.Move(11, 13)
            | Bits.Move(07, 14)
            | Bits.Move(03, 15), 
            Score);

        public Board FillEmptySpot(IGenerator rnd)
        {
            var freeCells = FreeCells.FromBoard(this);
            if (freeCells.Any())
            {
                var bits = Bits | rnd.NextMask(freeCells);
                return new Board(bits, Score);
            }
            else
            {
                return Empty;
            }
        }

        public override string ToString()
            => Invariant($@"{R0}
{R1}
{R2}
{R3}
Score: {Score:#,##0}");

        public override bool Equals(object obj) => obj is Board other && Equals(other);

        public bool Equals(Board other) => Bits == other.Bits && Score == other.Score;

        public override int GetHashCode() => Bits.GetHashCode();

        public static bool operator ==(Board left, Board right) => left.Equals(right);

        public static bool operator !=(Board left, Board right) => !(left == right);

        public static Board Initial(IGenerator rnd) => Empty.FillEmptySpot(rnd);

        public static Board Create(int[] cells, int score = 0)
        {
            return FromValues(
                cells[15], cells[14], cells[13], cells[12],
                cells[11], cells[10], cells[09], cells[08],
                cells[07], cells[06], cells[05], cells[04],
                cells[03], cells[02], cells[01], cells[00], score);
        }

        public static Board FromValues(
            int d0, int d1, int d2, int d3,
            int c0, int c1, int c2, int c3,
            int b0, int b1, int b2, int b3,
            int a0, int a1, int a2, int a3,
            int score) =>
            new Board(
                Row.FromValues(d0, d1, d2, d3),
                Row.FromValues(c0, c1, c2, c3),
                Row.FromValues(b0, b1, b2, b3),
                Row.FromValues(a0, a1, a2, a3),
                score);

        public static Board FromBits(ulong bits, int score) => new Board(bits, score);
    }
}
