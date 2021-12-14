using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day08 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day08;

        public List<Entry> Entries = new List<Entry>();
        public struct Entry
        {
            public string[] Pattern;
            public string[] Output;
        }
        static readonly Dictionary<int, int> DigitRenderer = new Dictionary<int, int>() {
            { 0, 0b1110111 },
            { 1, 0b0010010 },
            { 2, 0b1011101 },
            { 3, 0b0111011 },
            { 4, 0b0111010 },
            { 5, 0b1101011 },
            { 6, 0b1101111 },
            { 7, 0b1010010 },
            { 8, 0b1111111 },
            { 9, 0b1111011 }
        };

        static int CharToSignal(char c)
        {
            switch (c)
            {
                case 'a':
                    return 1 << 6;
                case 'b':
                    return 1 << 5;
                case 'c':
                    return 1 << 4;
                case 'd':
                    return 1 << 3;
                case 'e':
                    return 1 << 2;
                case 'f':
                    return 1 << 1;
                case 'g':
                    return 1;
                default:
                    return 0;
            }
        }

        static int StringToSignal(string s)
        {
            var signal = 0;
            foreach(char c in s)
            {
                signal |= CharToSignal(c);
            }
            return signal;
        }

        int SignalCount(int signal)
        {
            var count = 0;
            while (signal > 0)
            {
                if (signal%2 == 1)
                {
                    count++;
                }
                signal >>= 1;
            }
            return count;
        }

        int SignalDistance(int signal1, int signal2)
        {
            return SignalCount(SignalDifference(signal1, signal2));
        }

        bool SignalContains(int parent, int child)
        {
            return (parent | child) == parent;
        }

        int SignalDifference(int signal1, int signal2)
        {
            return signal1 ^ signal2;
        }

        public override long Part1()
        {
            Dictionary<int, int> DigitSignalCount = new Dictionary<int, int>();
            foreach (var d in DigitRenderer)
            {
                var signalCount = SignalCount(d.Value);
                if (!DigitSignalCount.ContainsKey(signalCount))
                {
                    DigitSignalCount.Add(signalCount, 0);
                }
                DigitSignalCount[signalCount]++;
            }
            var UniqueSignalCount = DigitSignalCount.Where(x => x.Value == 1).Select(x => x.Key).ToHashSet();
            long EasyDigitCount = Entries.Sum(entry => entry.Output.Count(o => UniqueSignalCount.Contains(o.Length)));
            return EasyDigitCount;
        }

        public override long Part2()
        {
            long sum = 0;
            foreach (var entry in Entries)
            {
                Dictionary<int, int> SignalEncoder = new Dictionary<int, int>();

                SignalEncoder.Add(8, StringToSignal(entry.Pattern.First(p => p.Length == 7)));
                SignalEncoder.Add(1, StringToSignal(entry.Pattern.First(p => p.Length == 2)));
                SignalEncoder.Add(7, StringToSignal(entry.Pattern.First(p => p.Length == 3)));
                SignalEncoder.Add(4, StringToSignal(entry.Pattern.First(p => p.Length == 4)));
                int ad = SignalDifference(SignalEncoder[1], SignalEncoder[4]);
                SignalEncoder.Add(5, StringToSignal(entry.Pattern.First(p => p.Length == 5 && SignalContains(StringToSignal(p), ad))));
                SignalEncoder.Add(9, StringToSignal(entry.Pattern.First(p => p.Length == 6 && SignalContains(StringToSignal(p), SignalEncoder[4] | SignalEncoder[7]))));
                SignalEncoder.Add(3, StringToSignal(entry.Pattern.First(p => p.Length == 5 && SignalContains(StringToSignal(p), SignalEncoder[7]))));
                SignalEncoder.Add(6, StringToSignal(entry.Pattern.First(p => p.Length == 6 && SignalContains(StringToSignal(p), ad) && StringToSignal(p) != SignalEncoder[9])));
                SignalEncoder.Add(0, StringToSignal(entry.Pattern.First(p => p.Length == 6 && !SignalEncoder.Values.Contains(StringToSignal(p)))));
                SignalEncoder.Add(2, StringToSignal(entry.Pattern.First(p => !SignalEncoder.Values.Contains(StringToSignal(p)))));

                var SignalDecoder = SignalEncoder.ToDictionary((encoder) => encoder.Value, (encoder) => encoder.Key);

                var total = 0;
                for (int i = 0; i < entry.Output.Length; i++)
                {
                    total *= 10;
                    total += SignalDecoder[StringToSignal(entry.Output[i])];
                }
                sum += total;
            }
            return sum;
        }

        protected override void LoadData(List<string> input)
        {
            Entries = input.Select(x =>
            {
                var entryInput = x.Split('|');
                var entryPattern = entryInput[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                var entryOutput = entryInput[1].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                return new Entry() { Pattern = entryPattern, Output = entryOutput };
            }).ToList();

        }
    }
}
