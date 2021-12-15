using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day13TabModel : DayTabModelBase
    {
        public override string Header => "Day 13";

        protected override Models.DayBase Puzzle => Day13Puzzle;
        Models.Days.Day13 Day13Puzzle = new Models.Days.Day13();

        public Day13TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
