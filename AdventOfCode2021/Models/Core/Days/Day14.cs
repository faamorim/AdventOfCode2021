using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day14 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day14;

        List<char> PolymerTemplate = new List<char>();
        Dictionary<(char, char), char> PairInsertionRules = new Dictionary<(char, char), char>();

        public override long Part1()
        {
            var occurrences = PolymerTemplate.GroupBy(c => c).ToDictionary(g => g.Key, g => (long)g.Count());
            Polymerize(PolymerTemplate, 10, occurrences);

            var max = occurrences.Max(o => o.Value);
            var min = occurrences.Min(o => o.Value);
            return max - min;
        }

        public override long Part2()
        {
            var occurrences = PolymerTemplate.GroupBy(c => c).ToDictionary(g => g.Key, g => (long)g.Count());
            Polymerize(PolymerTemplate, 40, occurrences);

            var max = occurrences.Max(o => o.Value);
            var min = occurrences.Min(o => o.Value);
            return max - min;
        }

        void Polymerize(List<char> polymer, int steps, Dictionary<char, long> occurrences)
        {
            Dictionary<(char,char),((char,char)[],char)> PairPolymerization = PairInsertionRules.ToDictionary(rule => rule.Key, rule => (new (char, char)[] { (rule.Key.Item1, rule.Value), (rule.Value, rule.Key.Item2) }, rule.Value));
            Dictionary<(char, char), long> PairOccurrences = new Dictionary<(char, char), long>();
            for (int i = 1; i < polymer.Count; i++)
            {
                IncreasePairCount(PairOccurrences, (polymer[i - 1], polymer[i]));
            }
            for (var step = 0; step < steps; step++)
            {
                var newPairs = new Dictionary<(char, char), long>();
                foreach (var pairCount in PairOccurrences)
                {
                    var polymerization = PairPolymerization[pairCount.Key];
                    var pairs = polymerization.Item1;
                    var newElement = polymerization.Item2;

                    IncreasePairCount(newPairs, pairs[0], pairCount.Value);
                    IncreasePairCount(newPairs, pairs[1], pairCount.Value);
                    Increase<char>(occurrences, newElement, pairCount.Value);
                }
                PairOccurrences = newPairs;
            }
        }

        static void IncreasePairCount(Dictionary<(char, char), long> dictionary, (char, char) key, long value = 1)
        {
            Increase<(char, char)>(dictionary, key, value);
        }

        static void Increase<T1>(Dictionary<T1, long> dictionary, T1 key, long value = 1)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
            else
            {
                dictionary[key] += value;
            }
        }

        protected override void LoadData(List<string> input)
        {
            PolymerTemplate = input[0].ToList();

            for (int i = 1; i < input.Count; i++)
            {
                var line = input[i];
                if (!string.IsNullOrEmpty(line))
                {
                    PairInsertionRules.Add((line[0], line[1]), line[6]);
                }
            }
        }
    }
}
