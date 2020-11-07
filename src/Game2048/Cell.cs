using System.Runtime.CompilerServices;

namespace Game2048
{
    public static class Cell
    {
        public const int MaxValue = 15;

        public const ushort Mask = 0b_0000_1111;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort C3(this ushort bits) => (ushort)(bits & 0xF);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort C2(this ushort bits) => (ushort)((bits & 0xF0) >> 4);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort C1(this ushort bits) => (ushort)((bits & 0xF00) >> 8);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort C0(this ushort bits) => (ushort)((bits & 0xF000) >> 12);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Move(this ulong bits, int from, int to)
            => ((bits >> (from * 4)) & Mask) << (to * 4);
    }
}
