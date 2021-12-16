using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day13 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day13;

        HashSet<(int, int)> Dots = new HashSet<(int, int)> ();
        List<(char, int)> FoldInstructions = new List<(char, int)> ();

        public override long Part1()
        {
            var dots = Dots.ToHashSet();
            Fold(dots, FoldInstructions[0]);
            return dots.Count();
        }

        public override long Part2()
        {
            var dots = Dots.ToHashSet();
            foreach (var instruction in FoldInstructions)
            {
                Fold(dots, instruction);
            }
            var maxX = 0;
            var maxY = 0;
            foreach (var dot in dots)
            {
                maxX = Math.Max(maxX, dot.Item1);
                maxY = Math.Max(maxY, dot.Item2);
            }
            bool[,] result = new bool[maxX+1, maxY+1];
            foreach (var dot in dots)
            {
                result[dot.Item1, dot.Item2] = true;
            }
            for (int j = 0; j <= maxY; j++)
            {
                for (int i = 0; i <= maxX; i++)
                {
                    System.Diagnostics.Debug.Write(result[i, j] ? '#' : '.');
                }
                System.Diagnostics.Debug.WriteLine("");
            }
            return 0;
        }

        static void Fold(HashSet<(int, int)> dots, (char, int) foldInstruction)
        {
            var folded = new List<(int, int)>();
            switch (foldInstruction.Item1)
            {
                case 'x':
                    folded = dots.Where(coord => coord.Item1 > foldInstruction.Item2).ToList();
                    dots.RemoveWhere(coord => coord.Item1 > foldInstruction.Item2);
                    folded.ForEach(coord => dots.Add((2 * foldInstruction.Item2 - coord.Item1, coord.Item2)));
                    break;
                case 'y':
                    folded = dots.Where(coord => coord.Item2 > foldInstruction.Item2).ToList();
                    dots.RemoveWhere(coord => coord.Item2 > foldInstruction.Item2);
                    folded.ForEach(coord => dots.Add((coord.Item1, 2 * foldInstruction.Item2 - coord.Item2)));
                    break;
            }
        }

        protected override void LoadData(List<string> input)
        {
            var foldMessage = "fold along ";
            foreach(var line in input)
            {
                if (line.StartsWith(foldMessage))
                {
                    var axis = line[foldMessage.Length];
                    var coord = int.Parse(line.Substring(foldMessage.Length + 2));
                    FoldInstructions.Add((axis, coord));
                }
                else if (!string.IsNullOrEmpty(line))
                {
                    var coords = line.Split(',');
                    Dots.Add((int.Parse(coords[0]), int.Parse(coords[1])));
                }
            }
        }
    }
}
