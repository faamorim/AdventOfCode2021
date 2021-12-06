using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels
{
    internal abstract class DayTabModelBase : TabModelBase
    {
        public virtual long Part1 => Puzzle.Part1();
        public virtual long Part2 => Puzzle.Part2();

        protected abstract Models.DayBase Puzzle { get; }
    }
}
