using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day03 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day03;
        public List<int> Report { get; private set; }

        public override long Part1()
        {
            int gammaRate = 0, epsilonRate = 0;
            var reportCount = Report.Count();
            var max = Report.Max();
            for (int i = 0; 1 << i < max; i++)
            {
                var mask = 1 << i;
                var count = Report.Count(x => (x & mask) != 0);
                if (count >= reportCount / 2)
                {
                    gammaRate += mask;
                }
                else
                {
                    epsilonRate += mask;
                }
            }
            long powerConsumption = (long)gammaRate * epsilonRate;
            return powerConsumption;
        }

        public override long Part2()
        {
            IEnumerable<int> O2List = Report.ToList();
            IEnumerable<int> CO2List = Report.ToList();
            var max = Report.Max();
            var shift = 0;
            while (1 << shift + 1 <= max) shift++;
            int oxygenGenRating = 0, CO2ScrubRating = 0;
            bool breakO2 = false, breakCO2 = false;
            for (int i = shift; i >= 0 && !(breakO2 && breakCO2); i--)
            {
                var mask = 1 << i;
                if (!breakO2)
                {
                    var O2TotalCount = O2List.Count();
                    var O2Count = O2List.Count(x => (x & mask) != 0);
                    O2List = O2List.Where(x => O2Count >= O2TotalCount / 2 ? ((x & mask) != 0) : ((x & mask) == 0));
                    breakO2 = O2List.Count() == 1;
                }
                if (!breakCO2)
                {
                    var CO2TotalCount = CO2List.Count();
                    var CO2Count = CO2List.Count(x => (x & mask) != 0);
                    CO2List = CO2List.Where(x => CO2Count >= CO2TotalCount / 2 ? ((x & mask) == 0) : ((x & mask) != 0));
                    breakCO2 = CO2List.Count() == 1;
                }
            }
            oxygenGenRating = O2List.First();
            CO2ScrubRating = CO2List.First();
            long lifeSupportRating = (long)oxygenGenRating * CO2ScrubRating;
            return lifeSupportRating;
        }

        protected override void LoadData(List<string> input)
        {
            Report = input.Select(i => Convert.ToInt32(i, 2)).ToList();
        }
    }
}
