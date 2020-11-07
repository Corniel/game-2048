using Game2048;
using NUnit.Framework;

namespace Row_specs
{
    public class All
    {
        [TestCase("[16384,   16,    8,    2]", 0xE431)]
        [TestCase("[   32,    0,    4,    0]", 0x5020)]
        public void ToString_displays_column_values(string expected, int bits)
        {
            var display = new Row((ushort)bits).ToString();
            Assert.AreEqual(expected, display);
        }

        [TestCase(0xE431, 0x134E)]
        [TestCase(0x543A, 0xA345)]
        [TestCase(0x0000, 0x0000)]
        [TestCase(0x1030, 0x0301)]
        public void Rows_mirror_by_swapping_possitions(int output, int input)
        {
            var row = new Row((ushort)input);
            var expected = new Row((ushort)output);
            var mirrored = row.Mirror();
            var roundtrip = row.Mirror().Mirror();

            Assert.AreEqual(expected, mirrored);
            Assert.AreEqual(row, roundtrip);
        }
    
        [TestCase(0x0123, 0x1230)]
        [TestCase(0x0123, 0x1203)]
        [TestCase(0x0123, 0x1023)]
        [TestCase(0x0012, 0x1200)]
        [TestCase(0x0012, 0x1020)]
        [TestCase(0x0012, 0x1002)]
        [TestCase(0x0012, 0x0102)]
        [TestCase(0x0001, 0x0010)]
        [TestCase(0x0001, 0x0100)]
        [TestCase(0x0001, 0x1000)]
        public void Moving_right_moves_empty_cells_left(int exp, int bits)
        {
            var row = Row.FromBits(bits);
            var moved = row.MoveRight();
            Assert.AreEqual(Row.FromBits(exp), moved);
        }

        [Test]
        public void Moving_right_with_4_equal_values_results_in_2_equal_cells_left_both_1_higher()
        {
            var row = Row.FromBits(0x3333);
            var moved = row.MoveRight();
            Assert.AreEqual(Row.FromBits(0x0044), moved);
        }

        [TestCase(0x0234,0x2333)]
        [TestCase(0x0451, 0x4441)]
        public void Moving_right_with_3_equal_values_results_in_the_right_2_being_merged(int exp, int bits)
        {
            var row = Row.FromBits(bits);
            var moved = row.MoveRight();
            Assert.AreEqual(Row.FromBits(exp), moved);
        }

        [TestCase(0x0223, 0x1123)]
        [TestCase(0x0133, 0x1223)]
        [TestCase(0x0124, 0x1233)]
        public void Moving_right_with_2_equal_values_results_in_one_merge(int exp, int bits)
        {
            var row = Row.FromBits(bits);
            var moved = row.MoveRight();
            Assert.AreEqual(Row.FromBits(exp), moved);
        }
    }
}
