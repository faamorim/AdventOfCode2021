using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day17 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day17;

        public class Target
        {
            public class Range
            {
                public int Min;
                public int Max;
            }
            public Range X;
            public Range Y;
        }
        public Target TargetArea;

        public override long Part1()
        {
            long initialSpeed = Math.Max(TargetArea.Y.Max, TargetArea.Y.Min < 0 ? TargetArea.Y.Min * -1 - 1 : 0);
            return (initialSpeed * (initialSpeed + 1)) / 2;
        }

        public override long Part2()
        {
            var PositionStepsSpeedDictionary = new Dictionary<(int, int), Dictionary<int, (int, int)>>();
            for (var x = TargetArea.X.Min; x <= TargetArea.X.Max; x++)
            {
                var absX = Math.Abs(x);
                var signX = Math.Sign(x);
                var minInitialHSpeed = (int)Math.Round((Math.Sqrt(8 * absX + 1) - 1) / 2);
                var convergeToTargetX = (minInitialHSpeed * (minInitialHSpeed + 1)) / 2 == absX;
                var maxXSteps = convergeToTargetX ? int.MaxValue : minInitialHSpeed;
                for (var y = TargetArea.Y.Min; y <= TargetArea.Y.Max; y++)
                {
                    long maxInitialVSpeed = y < 0 ? y * -1 - 1 : y;
                    var maxYSteps = maxInitialVSpeed * 2 + (y < 0 ? 2 : 0);
                    var maxSteps = Math.Min(maxXSteps, maxYSteps);
                    for (int s = 1; s <= maxSteps; s++)
                    {
                        var validXSteps = Math.Min(s, minInitialHSpeed);
                        var hSpeed = signX;
                        if (convergeToTargetX && s >= minInitialHSpeed)
                        {
                            hSpeed *= minInitialHSpeed;
                        }
                        else
                        {
                            var approxHSpeed = (int)Math.Round((validXSteps * validXSteps - validXSteps + 2 * absX) / (2.0 * s));
                            if (approxHSpeed * (approxHSpeed + 1) - (approxHSpeed - validXSteps) * (approxHSpeed - validXSteps + 1) != 2 * absX)
                            {
                                continue;
                            }
                            hSpeed *= approxHSpeed;
                        }

                        var vSpeed = (int)Math.Round((y + (s*(s-1))/2.0)/s);
                        if (vSpeed * s - (s*(s-1))/2 != y)
                        {
                            continue;
                        }

                        if (!PositionStepsSpeedDictionary.TryGetValue((x, y), out var StepsSpeedDictionary))
                        {
                            StepsSpeedDictionary = new Dictionary<int, (int, int)>();
                            PositionStepsSpeedDictionary.Add((x, y), StepsSpeedDictionary);
                        }
                        StepsSpeedDictionary.Add(s, (hSpeed, vSpeed));
                    }
                }
            }
            return PositionStepsSpeedDictionary.SelectMany(pssd => pssd.Value.Values).Distinct().Count();
        }

        protected override void LoadData(List<string> input)
        {
            int x1, x2, y1, y2;
            bool signx1, signx2, signy1, signy2;
            var regex = Regex.Match(input[0], $@"^target area: x=(?<{nameof(signx1)}>-)?(?<{nameof(x1)}>\d+)..(?<{nameof(signx2)}>-)?(?<{nameof(x2)}>\d+), y=(?<{nameof(signy1)}>-)?(?<{nameof(y1)}>\d+)..(?<{nameof(signy2)}>-)?(?<{nameof(y2)}>\d+)$");
            x1 = int.Parse(regex.Groups[nameof(x1)].Value);
            x2 = int.Parse(regex.Groups[nameof(x2)].Value);
            y1 = int.Parse(regex.Groups[nameof(y1)].Value);
            y2 = int.Parse(regex.Groups[nameof(y2)].Value);
            signx1 = regex.Groups[nameof(signx1)].Success;
            signx2 = regex.Groups[nameof(signx2)].Success;
            signy1 = regex.Groups[nameof(signy1)].Success;
            signy2 = regex.Groups[nameof(signy2)].Success;

            x1 *= signx1 ? -1 : 1;
            x2 *= signx2 ? -1 : 1;
            y1 *= signy1 ? -1 : 1;
            y2 *= signy2 ? -1 : 1;

            TargetArea = new Target() { X = new Target.Range() { Min = Math.Min(x1, x2), Max = Math.Max(x1, x2) }, Y = new Target.Range() { Min = Math.Min(y1, y2), Max = Math.Max(y1, y2) } };
        }
    }
}
