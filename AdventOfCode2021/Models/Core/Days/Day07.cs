using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day07 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day07;

        public List<int> CrabPositions { get; private set; } = new List<int>();

        public override long Part1()
        {
            var positions = CrabPositions.ToList();
            positions.Sort();
            var median = positions[positions.Count / 2];

            return positions.Sum(x => Math.Abs((long)median - x));
        }

        public override long Part2()
        {
            var average = CrabPositions.Average();
            var avgFloor = (long)Math.Floor(average);
            var avgCeil = (long)Math.Ceiling(average);
            var avgFloorSum = CrabPositions.Sum(x => (Math.Abs(avgFloor - x) * (Math.Abs(avgFloor - x) + 1)) / 2);
            var avgCeilSum = CrabPositions.Sum(x => (Math.Abs(avgCeil - x) * (Math.Abs(avgCeil - x) + 1)) / 2);
            var lowestSum = Math.Min(avgFloorSum, avgCeilSum);
            return lowestSum;
        }

        protected override void LoadData(List<string> input)
        {
            CrabPositions = input[0].Split(',').Select(x => int.Parse(x)).ToList();
        }
    }
}
