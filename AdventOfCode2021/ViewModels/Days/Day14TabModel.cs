using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day14TabModel : DayTabModelBase
    {
        public override string Header => "Day 14";

        protected override Models.DayBase Puzzle => Day14Puzzle;
        Models.Days.Day14 Day14Puzzle = new Models.Days.Day14();

        public Day14TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
