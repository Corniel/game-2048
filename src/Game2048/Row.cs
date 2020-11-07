using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;

namespace Game2048
{

    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public readonly struct Row : IEquatable<Row>
    {
        public static readonly Row Empty;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [FieldOffset(0)]
        private readonly ushort bits;

        public Row(ushort bits) => this.bits = bits;

        public Row Mirror() => new Row(bits.Mirror());
        public Row MoveLeft() => new Row(bits.MoveLeft());
        public Row MoveRight() => new Row(bits.MoveRight());

        public int ScoreLeft() => bits.ScoreLeft();
        public int ScoreRight() => bits.ScoreRight();
        
        public override bool Equals(object obj) => obj is Row other && Equals(other);
        public bool Equals(Row other) => bits == other.bits;
        public override int GetHashCode() => bits;

        public override string ToString() => "[" +
            $"{Value.FromCell(bits.C0()),5}," +
            $"{Value.FromCell(bits.C1()),5}," +
            $"{Value.FromCell(bits.C2()),5}," +
            $"{Value.FromCell(bits.C3()),5}]";

        public static Row FromBits(int bits) => new Row((ushort)bits);
        public static Row FromValues(int c0, int c1, int c2, int c3) 
            => new Row(Cells.FromValues(c0, c1, c2, c3));
    }
}
