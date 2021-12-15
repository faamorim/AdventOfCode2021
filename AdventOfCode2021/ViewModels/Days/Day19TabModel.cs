using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day19TabModel : DayTabModelBase
    {
        public override string Header => "Day 19";

        protected override Models.DayBase Puzzle => Day19Puzzle;
        Models.Days.Day19 Day19Puzzle = new Models.Days.Day19();

        public Day19TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
