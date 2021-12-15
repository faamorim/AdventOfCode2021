using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day16TabModel : DayTabModelBase
    {
        public override string Header => "Day 16";

        protected override Models.DayBase Puzzle => Day16Puzzle;
        Models.Days.Day16 Day16Puzzle = new Models.Days.Day16();

        public Day16TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
