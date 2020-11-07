using System.Collections;
using System.Collections.Generic;

namespace Game2048
{
    public sealed class FreeCells : IEnumerable<byte>
    {
        private readonly byte[] indexes;

        internal FreeCells(List<byte> indexes) => this.indexes = indexes.ToArray();

        public int Count => indexes.Length;

        public byte this[int index]=> indexes[index];

        public bool Any() => Count != 0;

        public override string ToString() => string.Join(", ", indexes);

        public IEnumerator<byte> GetEnumerator() => ((IEnumerable<byte>)indexes).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static bool Any(Board board)
            => cells[board.R0.GetHashCode()].Any()
            || cells[board.R1.GetHashCode()].Any()
            || cells[board.R2.GetHashCode()].Any()
            || cells[board.R3.GetHashCode()].Any();

        public static FreeCells FromBoard(Board board)
        {
            int mask = rows[board.R3.GetHashCode()];
            mask |= rows[board.R2.GetHashCode()] << 4;
            mask |= rows[board.R1.GetHashCode()] << 8;
            mask |= rows[board.R0.GetHashCode()] << 12;
            return cells[mask];
        }

        static FreeCells() => Init();

        private static void Init()
        {
            InitRows();
            InitCells();
        }

        private static void InitRows()
        {
            for (ushort row = 0; row < ushort.MaxValue; row++)
            {
                int mask = 0;
                mask |= row.C0() == 0 ? 8 : 0;
                mask |= row.C1() == 0 ? 4 : 0;
                mask |= row.C2() == 0 ? 2 : 0;
                mask |= row.C3() == 0 ? 1 : 0;
                rows[row] |= (byte)mask;
            }
        }

        private static void InitCells()
        {
            var buffer = new List<byte>(16);

            for (int row = 0; row <= ushort.MaxValue; row++)
            {
                buffer.Clear();

                for (byte i = 0; i < 16; i++)
                {
                    if (((row >> i) & 1) == 1)
                    {
                        buffer.Add(i);
                    }
                }
                cells[row] = new FreeCells(buffer);
            }
        }

        private static readonly byte[] rows = new byte[ushort.MaxValue];
        private static readonly FreeCells[] cells = new FreeCells[ushort.MaxValue + 1];
    }
}
