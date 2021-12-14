using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day08TabModel : DayTabModelBase
    {
        public override string Header => "Day 08";

        protected override Models.DayBase Puzzle => Day08Puzzle;
        Models.Days.Day08 Day08Puzzle = new Models.Days.Day08();

        public Day08TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
