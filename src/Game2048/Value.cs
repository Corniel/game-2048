using System;

namespace Game2048
{
    public static class Value
    {
        public static ulong FromBits(ulong bits, int index) => (bits >> (index * 4)) & Cell.Mask;

        public static ushort ToBits(int value)
        {
            return value switch
            {
                00000 => 00,
                00002 => 01,
                00004 => 02,
                00008 => 03,
                00016 => 04,
                00032 => 05,
                00064 => 06,
                00128 => 07,
                00256 => 08,
                00512 => 09,
                01024 => 10,
                02048 => 11,
                04096 => 12,
                08192 => 13,
                16384 => 14,
                32768 => 15,
                _ => throw new InvalidOperationException(),
            };
        }

        public static int FromCell(int bits) => values[bits];

        private static readonly int[] values = new[]
        {
            00000,
            00002,
            00004,
            00008,
            00016,
            00032,
            00064,
            00128,
            00256,
            00512,
            01024,
            02048,
            04096,
            08192,
            16384,
            32768,
         };
    }
}
