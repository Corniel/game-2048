using System;
using System.Linq;

namespace Game2048
{
    public static class Cells
    {
        public static ushort FromValues(int c0, int c1, int c2, int c3)
           => Merge(Value.ToBits(c0), Value.ToBits(c1), Value.ToBits(c2), Value.ToBits(c3));
        
        public static ushort Merge(ushort c0, ushort c1, ushort c2, ushort c3)
            => (ushort)(c3
            | (c2 << 4)
            | (c1 << 8)
            | (c0 << 12));

        public static ushort Mirror(this ushort bits)
        {
            unchecked
            {
                int mirrored = bits << 12;
                mirrored |= (bits & 0x00F0) << 4;
                mirrored |= (bits & 0x0F00) >> 4;
                mirrored |= bits >> 12;
                return (ushort)mirrored;
            }
        }
    }
}
