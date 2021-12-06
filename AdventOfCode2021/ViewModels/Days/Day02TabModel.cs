using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels.Days
{
    internal class Day02TabModel : DayTabModelBase
    {
        public override string Header => "Day 02";

        protected override Models.DayBase Puzzle => Day02Puzzle;
        Models.Days.Day02 Day02Puzzle = new Models.Days.Day02();

        public Day02TabModel()
        {
            Puzzle.SetupFromDefault();
        }
    }
}
