using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day09 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day09;

        public int[,] HeightMap = new int[0,0];

        public override long Part1()
        {
            long dangerSum = 0;
            for (int i = 0; i < HeightMap.GetLength(0); i++)
            {
                for (int j = 0; j < HeightMap.GetLength(1); j++)
                {
                    if (IsLocalLow(i, j))
                    {
                        dangerSum += HeightMap[i,j] + 1;
                    }
                }
            }
            return dangerSum;
        }

        public override long Part2()
        {
            var basinSizes = new List<int>();
            var basins = new Dictionary<(int, int), HashSet<(int, int)>>();
            for (int i = 0; i < HeightMap.GetLength(0); i++)
            {
                for (int j = 0; j < HeightMap.GetLength(1); j++)
                {
                    if (IsLocalLow(i, j))
                    {
                        basins.Add((i, j), new HashSet<(int, int)>() { (i, j) });
                    }
                }
            }
            
            foreach (var basin in basins)
            {
                Queue<(int, int)> queue = new Queue<(int, int)>(GetNeighbors(basin.Key).Where(n => GetHeight(n) != 9));
                (var i, var j) = basin.Key;
                while (queue.Count > 0)
                {
                    var coord = queue.Dequeue();
                    if (basin.Value.Contains(coord))
                    {
                        continue;
                    }
                    var lowest = SelectLowest(coord.Item1, coord.Item2);
                    if (lowest.All(c => basin.Value.Contains(c)))
                    {
                        basin.Value.Add(coord);
                        GetNeighbors(coord).Where(c => !basin.Value.Contains(c) && GetHeight(c) != 9).ToList().ForEach(c => queue.Enqueue(c));
                    }
                }
            }

            var basinProduct = 1;
            basins.Select(b=> b.Value.Count).OrderByDescending(x => x).Take(3).ToList().ForEach(x => basinProduct *= x);
            return basinProduct;

            List<(int, int)> SelectLowest(int i, int j)
            {
                var coords = new List<(int, int)>();
                var lowest = HeightMap[i, j];
                var neighbors = GetNeighbors(i, j);
                foreach (var neighbor in neighbors)
                {
                    var height = GetHeight(neighbor);
                    if (height < lowest)
                    {
                        lowest = height;
                        coords.Clear();
                    }
                    if (height == lowest)
                    {
                        coords.Add(neighbor);
                    }
                }
                return coords;
            }
        }

        IEnumerable<(int, int)> GetNeighbors(int i, int j)
        {
            if (i - 1 >= 0) yield return (i - 1, j);
            if (i+1 < HeightMap.GetLength(0)) yield return (i + 1, j);
            if (j - 1 >= 0) yield return (i, j - 1);
            if (j + 1 < HeightMap.GetLength(1)) yield return (i, j + 1);
        }

        IEnumerable<(int, int)> GetNeighbors((int, int) coord)
        {
            return GetNeighbors(coord.Item1, coord.Item2);
        }

        bool IsLocalLow(int i, int j)
        {
            if (GetNeighbors(i, j).Any(x => GetHeight(x) <= GetHeight(i, j))) return false;
            return true;
        }

        int GetHeight(int i, int j)
        {
            return HeightMap[i, j];
        }

        int GetHeight((int, int) coord)
        {
            return GetHeight(coord.Item1, coord.Item2);
        }

        protected override void LoadData(List<string> input)
        {
            HeightMap = new int[input.Count, input[0].Length];
            for (int i = 0; i < input.Count; i++)
            {
                var row = input[i];
                for (int j = 0; j < row.Length; j++)
                {
                    HeightMap[i,j] = int.Parse(row[j].ToString());
                }
            }
        }
    }
}
