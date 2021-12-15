using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day17TabModel : DayTabModelBase
    {
        public override string Header => "Day 17";

        protected override Models.DayBase Puzzle => Day17Puzzle;
        Models.Days.Day17 Day17Puzzle = new Models.Days.Day17();

        public Day17TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
