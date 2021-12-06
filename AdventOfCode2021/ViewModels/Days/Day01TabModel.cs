using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day01TabModel : DayTabModelBase
    {
        public override string Header => "Day 01";

        protected override Models.DayBase Puzzle => Day01Puzzle;
        Models.Days.Day01 Day01Puzzle = new Models.Days.Day01();

        public Day01TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
