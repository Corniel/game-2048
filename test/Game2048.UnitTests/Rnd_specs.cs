using Game2048;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troschuetz.Random.Generators;

namespace Rnd_specs
{
    public class Distribution
    {
        [Test]
        public void Of_2_vs_4_is_9_to_10()
        {
            var simulations = 100_000;
            var rnd = new MT19937Generator(simulations);
            
            var twos = Enumerable.Repeat(0, simulations)
                .Where(i => rnd.NextCell() == 1)
                .Count();

            Console.WriteLine($"{twos / (decimal)simulations:0.00%}");

            Assert.That(twos, Is.InRange(89_500, 90_500));
        }
    }
}
