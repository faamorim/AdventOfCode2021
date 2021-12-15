using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day23TabModel : DayTabModelBase
    {
        public override string Header => "Day 23";

        protected override Models.DayBase Puzzle => Day23Puzzle;
        Models.Days.Day23 Day23Puzzle = new Models.Days.Day23();

        public Day23TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
