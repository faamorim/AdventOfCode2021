using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day05TabModel : DayTabModelBase
    {
        public override string Header => "Day 05";

        protected override Models.DayBase Puzzle => Day05Puzzle;
        Models.Days.Day05 Day05Puzzle = new Models.Days.Day05();

        public Day05TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
