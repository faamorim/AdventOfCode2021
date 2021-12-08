using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Models.Days
{
    internal class Day05 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day05;

        public List<Line> Lines { get; private set; } = new List<Line>();
        public class Line
        {
            public Point P1 { get; private set; }
            public Point P2 { get; private set; }
            public Line(int p1x, int p1y, int p2x, int p2y)
            {
                P1 = new Point(p1x, p1y);
                P2 = new Point(p2x, p2y);
            }
        }

        public override long Part1()
        {
            Dictionary<Point, int> positionCount = new Dictionary<Point, int>();
            long dangerCount = 0;
            foreach(var l in Lines)
            {
                if (l.P1.X == l.P2.X || l.P1.Y == l.P2.Y)
                {
                    var startX = Math.Min(l.P1.X, l.P2.X);
                    var startY = Math.Min(l.P1.Y, l.P2.Y);
                    var endX = Math.Max(l.P1.X, l.P2.X);
                    var endY = Math.Max(l.P1.Y, l.P2.Y);
                    var modX = endX - startX != 0 ? 1 : 0;
                    var modY = endY - startY != 0 ? 1 : 0;
                    for (int x = startX, y = startY; x <= endX && y <= endY;)
                    {
                        var point = new Point(x, y);
                        if(!positionCount.TryGetValue(point, out var count))
                        {
                            count = 0;
                            positionCount.Add(point, count);
                        }
                        count++;
                        positionCount[point] = count;
                        if (count == 2)
                        {
                            dangerCount++;
                        }
                        x += modX;
                        y += modY;
                    }
                }
            }
            return dangerCount;
        }

        public override long Part2()
        {
            Dictionary<Point, int> positionCount = new Dictionary<Point, int>();
            long dangerCount = 0;
            foreach (var l in Lines)
            {
                var diffX = l.P2.X - l.P1.X;
                var diffY = l.P2.Y - l.P1.Y;
                var modX = Math.Sign(diffX);
                var modY = Math.Sign(diffY);
                var dist = Math.Max(Math.Abs(diffX), Math.Abs(diffY));
                for (int d = 0, x = 0, y = 0; d <= dist; d++)
                {
                    var point = new Point(l.P1.X + x, l.P1.Y + y);
                    if (!positionCount.TryGetValue(point, out var count))
                    {
                        count = 0;
                        positionCount.Add(point, count);
                    }
                    count++;
                    positionCount[point] = count;
                    if (count == 2)
                    {
                        dangerCount++;
                    }
                    x += modX;
                    y += modY;
                }
            }
            return dangerCount;
        }

        protected override void LoadData(List<string> input)
        {
            Lines = new List<Line>();
            foreach (string i in input)
            {
                var regex = Regex.Match(i, $@"^(\d+),(\d+) -> (\d+),(\d+)$");
                Lines.Add(new Line(int.Parse(regex.Groups[1].Value), int.Parse(regex.Groups[2].Value), int.Parse(regex.Groups[3].Value), int.Parse(regex.Groups[4].Value)));
            }
        }
    }
}
