using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day15 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day15;
        int[,] Map = new int[0, 0];

        public override long Part1()
        {
            var start = (0, 0);
            var end = (Map.GetLength(0) - 1, Map.GetLength(1) - 1);
            var cost = new Dictionary<(int, int), long> ();
            var from = new Dictionary<(int, int), (int, int)>();
            cost[start] = 0;
            var queue = new PriorityQueue<(int, int), long>();
            queue.Enqueue(start, 0);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current == end) break;
                foreach (var neighbor in GetNeighbors(current))
                {
                    var newCost = cost[current] + GetMapCost(neighbor);
                    if (!cost.ContainsKey(neighbor) || cost[neighbor] > newCost)
                    {
                        cost[neighbor] = newCost;
                        queue.Enqueue(neighbor, newCost + Heuristic(neighbor, end));
                        from[neighbor] = current;
                    }
                }
            }
            return cost[end];
        }

        public override long Part2()
        {
            var sizeMult = 5;
            var sizeX = Map.GetLength(0);
            var sizeY = Map.GetLength(1);
            var start = (0, 0);
            var end = (sizeMult * sizeX - 1, sizeMult * sizeY - 1);
            var cost = new Dictionary<(int, int), long>();
            var from = new Dictionary<(int, int), (int, int)>();
            cost[start] = 0;
            var queue = new PriorityQueue<(int, int), long>();
            queue.Enqueue(start, 0);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current == end) break;
                foreach (var neighbor in GetNeighbors(current, sizeMult))
                {
                    var newCost = cost[current] + GetBigMapCost(neighbor);
                    if (!cost.ContainsKey(neighbor) || cost[neighbor] > newCost)
                    {
                        cost[neighbor] = newCost;
                        queue.Enqueue(neighbor, newCost + Heuristic(neighbor, end));
                        from[neighbor] = current;
                    }
                }
            }
            return cost[end];
        }

        int GetMapCost((int, int) coord)
        {
            return Map[coord.Item1, coord.Item2];
        }

        int GetBigMapCost((int, int) coord)
        {
            var x = coord.Item1 % Map.GetLength(0);
            var y = coord.Item2 % Map.GetLength(1);
            var warp = coord.Item1 / Map.GetLength(0) + coord.Item2 / Map.GetLength(1);
            return (Map[x, y] + warp - 1) % 9 + 1;
        }

        IEnumerable<(int, int)> GetNeighbors((int, int) coord, int sizeMult = 1)
        {
            (int i, int j) = coord;
            if (i - 1 >= 0) yield return (i - 1, j);
            if (i + 1 < sizeMult * Map.GetLength(0)) yield return (i + 1, j);
            if (j - 1 >= 0) yield return (i, j - 1);
            if (j + 1 < sizeMult * Map.GetLength(1)) yield return (i, j + 1);
        }

        long Heuristic ((int, int) p1, (int, int) p2)
        {
            return Math.Abs(p1.Item1 - p2.Item1) + Math.Abs(p1.Item2 - p2.Item2);
        }

        protected override void LoadData(List<string> input)
        {
            Map = new int[input.Count, input[0].Length];
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    Map[i, j] = int.Parse(input[i][j].ToString());
                }
            }
        }
    }
}
