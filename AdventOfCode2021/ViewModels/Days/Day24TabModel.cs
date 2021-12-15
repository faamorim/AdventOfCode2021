using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day24TabModel : DayTabModelBase
    {
        public override string Header => "Day 24";

        protected override Models.DayBase Puzzle => Day24Puzzle;
        Models.Days.Day24 Day24Puzzle = new Models.Days.Day24();

        public Day24TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
