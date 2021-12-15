using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day12TabModel : DayTabModelBase
    {
        public override string Header => "Day 12";

        protected override Models.DayBase Puzzle => Day12Puzzle;
        Models.Days.Day12 Day12Puzzle = new Models.Days.Day12();

        public Day12TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
