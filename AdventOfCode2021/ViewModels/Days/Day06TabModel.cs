using AdventOfCode2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day06TabModel : DayTabModelBase
    {
        public override string Header => "Day 06";

        protected override Models.DayBase Puzzle => Day06Puzzle;
        Models.Days.Day06 Day06Puzzle = new Models.Days.Day06();

        public Day06TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
