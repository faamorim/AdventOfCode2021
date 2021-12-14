using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.ViewModels
{
    internal class MainWindowModel : ViewModelBase
    {
        public ObservableCollection<TabModelBase> Tabs { get; set; }
        public MainWindowModel()
        {
            Tabs = new ObservableCollection<TabModelBase>();
            Tabs.Add(new HomeTabModel());
            Tabs.Add(new Days.Day07TabModel());
        }
    }
}
