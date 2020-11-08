using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Game2048
{
    [DebuggerDisplay("{DebugToString()}")]
    public class RunCollection
    {
        public RunCollection() { }

        public void Apply(Board board)
        {
            total += board.Score;
            maxs[board.MaxValue]++;

            if (board.Score > max.Score)
            {
                max = board;
            }
        }

        public int Count => maxs.Values.Sum();
        public int MaxScore => max.Score;
        public double AvarageScore => (double)total / Count;

        public void Save(string filepath)
        {
            Save(new FileInfo(filepath));
        }
        public void Save(FileInfo file)
        {
            using var writer = new StreamWriter(file.FullName, false);
            Save(writer);
        }
        public void Save(TextWriter writer)
        {
            foreach (var kvp in maxs)
            {
                if (kvp.Value > 0)
                {
                    writer.WriteLine("{0,5} {1,8:#,##0} {2,5:0.0%}", kvp.Key, kvp.Value, (double)kvp.Value / Count);
                }
            }
            writer.WriteLine("Avg: {0:#,##0.0}", AvarageScore);
            writer.WriteLine("Max:");
            writer.WriteLine(max.ToString());
        }

        private string DebugToString()
        {
            return string.Format("Runs: {0}, Avg: {1:#,##0.0}, Max: {2:#,##0.0}", Count, AvarageScore, MaxScore);
        }

        private long total = 0;
        private Board max = Board.Empty;
        private Dictionary<int, int> maxs = new Dictionary<int, int>()
        {
            { 00002, 0 },
            { 00004, 0 },
            { 00008, 0 },
            { 00016, 0 },
            { 00032, 0 },
            { 00064, 0 },
            { 00128, 0 },
            { 00256, 0 },
            { 00512, 0 },
            { 01024, 0 },
            { 02048, 0 },
            { 04096, 0 },
            { 08192, 0 },
            { 16348, 0 },
            { 32768, 0 },
        };
    }
}
