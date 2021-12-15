using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day22TabModel : DayTabModelBase
    {
        public override string Header => "Day 22";

        protected override Models.DayBase Puzzle => Day22Puzzle;
        Models.Days.Day22 Day22Puzzle = new Models.Days.Day22();

        public Day22TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
