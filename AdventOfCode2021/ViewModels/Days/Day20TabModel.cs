using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day20TabModel : DayTabModelBase
    {
        public override string Header => "Day 20";

        protected override Models.DayBase Puzzle => Day20Puzzle;
        Models.Days.Day20 Day20Puzzle = new Models.Days.Day20();

        public Day20TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
