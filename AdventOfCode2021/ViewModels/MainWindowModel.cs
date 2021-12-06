﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModel
{
    internal class MainWindowModel : ViewModelBase
    {
        public ObservableCollection<TabModelBase> Tabs { get; set; }
        public MainWindowModel()
        {
            Tabs = new ObservableCollection<TabModelBase>();
            var home = new HomeTabModel();
            Tabs.Add(home);
        }
    }
}
