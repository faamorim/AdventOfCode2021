using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day03TabModel : DayTabModelBase
    {
        public override string Header => "Day 03";

        protected override Models.DayBase Puzzle => Day02Puzzle;
        Models.Days.Day03 Day02Puzzle = new Models.Days.Day03();

        public Day03TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
