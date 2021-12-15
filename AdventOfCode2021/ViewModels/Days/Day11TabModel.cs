using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day11TabModel : DayTabModelBase
    {
        public override string Header => "Day 11";

        protected override Models.DayBase Puzzle => Day11Puzzle;
        Models.Days.Day11 Day11Puzzle = new Models.Days.Day11();

        public Day11TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
