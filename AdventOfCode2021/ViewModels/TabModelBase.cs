﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModel
{
    abstract class TabModelBase : ViewModelBase
    {
        public abstract string Header { get; }
    }
}
