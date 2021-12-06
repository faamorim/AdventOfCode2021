using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModel
{
    internal abstract class DayTabModelBase : TabModelBase
    {
        public abstract long Part1 { get; }
        public abstract long Part2 { get; }
    }
}
