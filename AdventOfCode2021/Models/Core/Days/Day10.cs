using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day10 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day10;

        List<string> Lines = new List<string>();

        static Dictionary<char, char> ChunkOpenClosePair = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        static Dictionary<char, long> SyntaxCheckeScore = new Dictionary<char, long>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        static Dictionary<char, long> AutocompleteScore = new Dictionary<char, long>() { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };

        public override long Part1()
        {
            long score = 0;
            foreach (var line in Lines)
            {
                Stack<char> chunkCheck = new Stack<char>();
                foreach (char c in line)
                {
                    var dif = 1;
                    switch (c)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            chunkCheck.Push(c);
                            continue;
                        case ']':
                        case '}':
                        case '>':
                            dif = 2;
                            break;
                    }
                    if (!chunkCheck.TryPeek(out var open) || open + dif != c)
                    {
                        score += SyntaxCheckeScore[c];
                        break;
                    }
                    else
                    {
                        chunkCheck.Pop();
                    }
                }
            }
            return score;
        }

        public override long Part2()
        {
            List<long> scores = new List<long>();
            foreach (var line in Lines)
            {
                Stack<char> chunkCheck = new Stack<char>();
                var corrupt = false;
                long localScore = 0;
                foreach (char c in line)
                {
                    var dif = 1;
                    switch (c)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            chunkCheck.Push(c);
                            continue;
                        case ']':
                        case '}':
                        case '>':
                            dif = 2;
                            break;
                    }
                    if (!chunkCheck.TryPeek(out var open) || open + dif != c)
                    {
                        corrupt = true;
                        break;
                    }
                    else
                    {
                        chunkCheck.Pop();
                    }
                }
                if (corrupt)
                {
                    continue;
                }
                while (chunkCheck.TryPop(out var c))
                {
                    localScore = localScore * 5 + AutocompleteScore[ChunkOpenClosePair[c]];
                }
                scores.Add(localScore);
            }
            scores.Sort();
            return scores[scores.Count / 2];
        }

        protected override void LoadData(List<string> input)
        {
            Lines = input;
        }
    }
}
