using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day07TabModel : DayTabModelBase
    {
        public override string Header => "Day 07";

        protected override Models.DayBase Puzzle => Day07Puzzle;
        Models.Days.Day07 Day07Puzzle = new Models.Days.Day07();

        public Day07TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
