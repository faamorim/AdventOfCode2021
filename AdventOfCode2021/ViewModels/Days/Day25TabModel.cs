using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day25TabModel : DayTabModelBase
    {
        public override string Header => "Day 25";

        protected override Models.DayBase Puzzle => Day25Puzzle;
        Models.Days.Day25 Day25Puzzle = new Models.Days.Day25();

        public Day25TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
