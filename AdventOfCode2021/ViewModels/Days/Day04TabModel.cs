using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day04TabModel : DayTabModelBase
    {
        public override string Header => "Day 04";

        protected override Models.DayBase Puzzle => Day04Puzzle;
        Models.Days.Day04 Day04Puzzle = new Models.Days.Day04();

        public Day04TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
