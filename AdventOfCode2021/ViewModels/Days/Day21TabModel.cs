using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day21TabModel : DayTabModelBase
    {
        public override string Header => "Day 21";

        protected override Models.DayBase Puzzle => Day21Puzzle;
        Models.Days.Day21 Day21Puzzle = new Models.Days.Day21();

        public Day21TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
