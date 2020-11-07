using Game2048;
using NUnit.Framework;

namespace Board_specs
{
    public class All
    {
        [Test]
        public void ToString_displays_all_fields()
        {
            var board = Board.FromValues(
                00000, 00002, 00004, 00008,
                00016, 00032, 00064, 00128,
                00256, 00512, 01024, 02048,
                04096, 08192, 16384, 32768,
                17345);
            var display = board.ToString();

            Assert.AreEqual(@"[    0,    2,    4,    8]
[   16,   32,   64,  128]
[  256,  512, 1024, 2048]
[ 4096, 8192,16384,32768]
Score: 17,345", display);
        }

        [Test]
        public void Bits_map_to_rows()
        {
            var board = Board.FromBits(
                0x_0123_4567_89AB_CDEF,
                17345);
            var display = board.ToString();

            Assert.AreEqual(@"[    0,    2,    4,    8]
[   16,   32,   64,  128]
[  256,  512, 1024, 2048]
[ 4096, 8192,16384,32768]
Score: 17,345", display);
        }


        [Test]
        public void Rotate_left_rotates()
        {
            var board = Board.FromBits(0x_0123_4567_89AB_CDEF, 0);
            var rotated = board.RotateLeft();
            Assert.AreEqual(Board.FromBits(0x_37BF_26AE_159D_048C, 0), rotated);
        }

        [Test]
        public void Rotate_right_rotates()
        {
            var board = Board.FromBits(0x_0123_4567_89AB_CDEF, 0);
            var rotated = board.RotateRight();
            Assert.AreEqual(Board.FromBits(0x_C840_D951_EA62_FB73, 0), rotated);
        }


        [Test]
        public void Move_right_shift_cells_right()
        {
            var board = Board.FromBits(0x_0123_1204_1100_1234, 0);
            var moved = board.MoveRight();
            Assert.AreEqual(Board.FromBits(0x0123_0124_0002_1234, 4), moved);
        }

        [Test]
        public void Move_left_shift_cells_left()
        {
            var board = Board.FromBits(0x_0123_1204_1100_1234, 0);
            var moved = board.MoveLeft();
            Assert.AreEqual(Board.FromBits(0x1230_1240_2000_1234, 4), moved);
        }

        [Test]
        public void Move_up_shift_cells_up()
        {
            var board = Board.FromValues(
                0, 2, 4, 8,
                2, 2, 0, 4,
                2, 8, 0, 0,
                2, 2, 2, 2,
                0);

            var moved = board.MoveUp();
            Assert.AreEqual(Board.FromValues(
                4, 4, 4, 8,
                2, 8, 2, 4,
                0, 2, 0, 2,
                0, 0, 0, 0,
                8), moved);
        }

        [Test]
        public void Move_up_down_cells_down()
        {
            var board = Board.FromValues(
                0, 2, 4, 8,
                2, 2, 0, 4,
                2, 8, 0, 0,
                2, 2, 2, 2,
                0);

            var moved = board.MoveDown();
            Assert.AreEqual(Board.FromValues(
                0, 0, 0, 0,
                0, 4, 0, 8,
                2, 8, 4, 4,
                4, 2, 2, 2,
                8), moved);
        }
    }
}
