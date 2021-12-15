using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day15TabModel : DayTabModelBase
    {
        public override string Header => "Day 15";

        protected override Models.DayBase Puzzle => Day15Puzzle;
        Models.Days.Day15 Day15Puzzle = new Models.Days.Day15();

        public Day15TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
