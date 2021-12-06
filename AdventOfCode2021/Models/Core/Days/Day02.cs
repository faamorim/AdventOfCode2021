using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day02 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day02;
        public enum Direction { Forward, Down, Up };
        public class Command
        {
            public Direction Direction;
            public int Value;
        }

        public List<Command> Commands;

        public override long Part1()
        {
            long x = 0, y = 0;
            foreach (var command in Commands)
            {
                switch(command.Direction)
                {
                    case Direction.Forward:
                        x += command.Value;
                        break;
                    case Direction.Down:
                        y += command.Value;
                        break;
                    case Direction.Up:
                        y -= command.Value;
                        break;
                }
            }
            return x * y;
        }

        public override long Part2()
        {
            long x = 0, y = 0;
            int aim  = 0;
            foreach (var command in Commands)
            {
                switch (command.Direction)
                {
                    case Direction.Forward:
                        x += command.Value;
                        y += command.Value * aim;
                        break;
                    case Direction.Down:
                        aim += command.Value;
                        break;
                    case Direction.Up:
                        aim -= command.Value;
                        break;
                }
            }
            return x * y;
        }

        protected override void LoadData(List<string> input)
        {
            Commands = input.Select(s => {
                var c = s.Split(' ');
                var val = int.Parse(c[1]);
                Direction? dir = null;
                switch (c[0])
                {
                    case "forward":
                        dir = Direction.Forward;
                        break;
                    case "down":
                        dir = Direction.Down;
                        break;
                    case "up":
                        dir = Direction.Up;
                        break;
                }
                return new Command { Direction = dir.Value, Value = val };
            }).ToList();
        }
    }
}
