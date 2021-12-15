using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day09TabModel : DayTabModelBase
    {
        public override string Header => "Day 09";

        protected override Models.DayBase Puzzle => Day09Puzzle;
        Models.Days.Day09 Day09Puzzle = new Models.Days.Day09();

        public Day09TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
