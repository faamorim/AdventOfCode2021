using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day12 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day12;

        Dictionary<string, List<string>> Caves = new Dictionary<string, List<string>>();
        static string Start = "start";
        static string End = "end";

        public override long Part1()
        {
            return Traverse(Start);
        }

        public override long Part2()
        {
            return Traverse(Start, 1);
        }

        long Traverse(string current, int allowRevisit = 0, HashSet<string>? smallVisited = null)
        {
            if (smallVisited == null)
            {
                smallVisited = new HashSet<string>();
            }
            if (smallVisited.Contains(current))
            {
                if (current == Start || allowRevisit == 0)
                {
                    return 0;
                }
                allowRevisit--;
            }
            if (current == End)
            {
                return 1;
            }
            if (current.All(c => char.IsLower(c)))
            {
                smallVisited = smallVisited.ToHashSet();
                smallVisited.Add(current);
            }
            return Caves[current].Sum(cave => Traverse(cave, allowRevisit, smallVisited));
        }

        protected override void LoadData(List<string> input)
        {
            foreach (var line in input)
            {
                var connection = line.Split('-');
                if (!Caves.TryGetValue(connection[0], out var to))
                {
                    Caves.Add(connection[0], to = new List<string>());
                }
                to.Add(connection[1]);
                if (!Caves.TryGetValue(connection[1], out var from))
                {
                    Caves.Add(connection[1], from = new List<string>());
                }
                from.Add(connection[0]);
            }
        }
    }
}
