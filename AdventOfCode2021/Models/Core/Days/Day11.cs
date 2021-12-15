using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day11 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day11;

        int[,] Octopuses = new int[0, 0];

        public override long Part1()
        {
            int[,] octopuses = (int[,])Octopuses.Clone();
            int targetDay = 100;
            long flashCount = 0;
            for (int d = 1; d <= targetDay; d++)
            {
                flashCount += DayStep(octopuses);
            }
            return flashCount;
        }

        public override long Part2()
        {
            int[,] octopuses = (int[,])Octopuses.Clone();
            long dayCount = 0;
            do
            {
                dayCount++;
            }
            while (DayStep(octopuses) != octopuses.Length);
            return dayCount;
        }


        long DayStep(int[,] octopuses)
        {
            var flashed = new HashSet<(int, int)>();
            long flashCount = 0;
            for (var i = 0; i < octopuses.GetLength(0); i++)
            {
                for (var j = 0; j < octopuses.GetLength(1); j++)
                {
                    if (++octopuses[i, j] >= 10)
                    {
                        Flash(i, j);
                    }
                }
            }
            foreach (var coord in flashed)
            {
                octopuses[coord.Item1, coord.Item2] = 0;
            }
            return flashCount;


            void Flash(int i, int j)
            {
                if (flashed.Contains((i, j))) return;
                flashed.Add((i, j));
                flashCount++;
                var neighbors = GetNeighbors(i, j);
                neighbors.ToList().ForEach(n => { if (++octopuses[n.Item1, n.Item2] >= 10) Flash(n.Item1, n.Item2); });
            }
        }

        IEnumerable<(int, int)> GetNeighbors(int i, int j)
        {
            for (var di = -1; di <= 1; di++)
            {
                if (i + di < 0 || i + di >= Octopuses.GetLength(0)) continue;
                for (var dj = -1; dj <= 1; dj++)
                {
                    if (j + dj < 0 || j + dj >= Octopuses.GetLength(1)) continue;
                    if (di == 0 && dj == 0) continue;
                    yield return (i + di, j + dj);
                }
            }
        }

        protected override void LoadData(List<string> input)
        {
            Octopuses = new int[input.Count, input[0].Length];
            for (int i = 0; i < 10; i++)
            {
                var line = input[i];
                for (var j = 0; j < line.Length; j++)
                {
                    Octopuses[i, j] = int.Parse(line[j].ToString());
                }
            }
        }
    }
}
