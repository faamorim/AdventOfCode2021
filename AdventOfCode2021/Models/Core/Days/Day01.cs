using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models
{
    internal class Day01 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day01;
        public List<int> Depths { get; private set; }

        public override long Part1()
        {
            var count = 0;
            for (int i = 1; i < Depths.Count; i++)
            {
                if (Depths[i] > Depths[i - 1])
                {
                    count++;
                }
            }
            return count;
        }

        public override long Part2()
        {
            var count = 0;
            for (int i = 3; i < Depths.Count; i++)
            {
                if (Depths[i] > Depths[i - 3])
                {
                    count++;
                }
            }
            return count;
        }

        protected override void LoadData(List<string> input)
        {
            Depths = input.Select(i => int.Parse(i)).ToList();
        }
    }
}
