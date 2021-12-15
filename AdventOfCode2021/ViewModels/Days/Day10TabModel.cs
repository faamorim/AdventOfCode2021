using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day10TabModel : DayTabModelBase
    {
        public override string Header => "Day 10";

        protected override Models.DayBase Puzzle => Day10Puzzle;
        Models.Days.Day10 Day10Puzzle = new Models.Days.Day10();

        public Day10TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
