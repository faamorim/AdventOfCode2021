using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day06 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day06;
        public List<int> FishInternalClock = new List<int>();

        public override long Part1()
        {
            return CountAtTargetDay(80);
        }

        public override long Part2()
        {
            return CountAtTargetDay(256);
        }

        long CountAtTargetDay(int targetDay)
        {
            long[] fishes = new long[9];
            foreach (var fish in FishInternalClock)
            {
                fishes[fish]++;
            }
            var completeCycles = targetDay / 7;
            var daysLeft = targetDay % 7;
            for (int c = 0; c < completeCycles; c++)
            {
                long old8 = fishes[8], old7 = fishes[7];
                fishes[8] = 0;
                fishes[7] = 0;
                for (int i = 6; i >= 0; i--)
                {
                    fishes[i + 2] += fishes[i];
                }
                fishes[0] += old7;
                fishes[1] += old8;
            }
            for (int d = 0; d < daysLeft; d++)
            {
                var old0 = fishes[0];
                for (int i = 1; i <= 8; i++)
                {
                    fishes[i - 1] = fishes[i];
                }
                fishes[8] = old0;
                fishes[6] += old0;
            }
            return fishes.Sum();
        }

        protected override void LoadData(List<string> input)
        {
            FishInternalClock = input[0].Split(',').Select(x => int.Parse(x)).ToList();
        }
    }
}
