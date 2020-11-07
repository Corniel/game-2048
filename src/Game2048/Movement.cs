using System.Linq;

namespace Game2048
{
    public static class Movement
    {
        static Movement() => Init();

        public static ushort MoveLeft(this ushort bits) => moveLeft[bits];
        public static ushort MoveRight(this ushort bits) => moveRight[bits];

        public static int ScoreLeft(this ushort bits) => scoreLeft[bits];
        public static int ScoreRight(this ushort bits) => scoreRight[bits];

        private static void Init()
        {
            for (ushort bits = 1; bits < ushort.MaxValue; bits++)
            {
                Move(bits);
            }
        }
        private static void Move(ushort bits)
        {
            int score = 0;
            var cells = new ushort[]
            {
                bits.C0(),
                bits.C1(),
                bits.C2(),
                bits.C3()
            }
            .FetchLeft();

            if (cells.Any(cell => cell == Cell.Mask)) { return; }

            if(cells.Aaaa())
            {
                cells.Update(cells[0] + 1, cells[0] + 1, 0, 0);
                score = Value.FromCell(cells[0]) * 2;
            }
            else
            {
                var aa = cells.AaPosition();
                if(aa != -1)
                {
                    cells[aa]++;
                    cells.ShiftLeft(aa + 1);
                    score = Value.FromCell(cells[aa]);
                }
            }

            var left = Cells.Merge(cells[0], cells[1], cells[2], cells[3]);

            moveLeft[bits] = left;
            scoreLeft[bits] = score;

            moveRight[bits.Mirror()] = left.Mirror();
            scoreRight[bits.Mirror()] = score;
        }

        private static bool Aaaa(this ushort[] cells) 
            => cells[0] == cells[1]
            && cells[0] == cells[2]
            && cells[0] == cells[3];
   
        private static readonly ushort[] moveRight = new ushort[ushort.MaxValue];
        private static readonly ushort[] moveLeft = new ushort[ushort.MaxValue];
        private static readonly int[] scoreRight = new int[ushort.MaxValue];
        private static readonly int[] scoreLeft = new int[ushort.MaxValue];

        private static void Update(this ushort[] cells, int c0, int c1, int c2, int c3)
        {
            cells[0] = (ushort)c0;
            cells[1] = (ushort)c1;
            cells[2] = (ushort)c2;
            cells[3] = (ushort)c3;
        }
        private static ushort[] FetchLeft(this ushort[] cells)
        {
            var fetched = new ushort[4];
            var index = 0;

            foreach (var cell in cells)
            {
                if (cell != 0)
                {
                    fetched[index++] = cell;
                }
            }
            return fetched;
        }

        private static void ShiftLeft(this ushort[] cells, int start)
        {
            for (var i = start; i < 3; i++)
            {
                cells[i] = cells[i + 1];
            }
            cells[3] = 0;
        }

        private static int AaPosition(this ushort[] cells)
        {
            for (var i = 0; i < 3; i++)
            {
                if (cells[i] != 0 && cells[i] == cells[i + 1])
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
