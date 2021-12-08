using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day04 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day04;

        public List<int> Numbers { get; private set; } = new List<int>();
        public List<Board> Boards { get; private set; } = new List<Board>();

        public class Board
        {
            public static int Size => 5;
            public int[,] Numbers = new int[Size,Size];
        }

        public class PlayableBoard
        {
            Board NumberBoard { get; set; }
            public int[,] Numbers => NumberBoard.Numbers;
            public bool[,] Marked = new bool[Board.Size, Board.Size];

            public PlayableBoard(Board board)
            {
                NumberBoard = board;
            }

            public bool Mark(int number)
            {
                var pos = GetPosition(number);
                if (pos != null)
                {
                    Marked[pos.Value.Item1, pos.Value.Item2] = true;
                    var rows = Numbers.GetLength(0);
                    var cols = Numbers.GetLength(1);
                    int i = 0, j = 0;
                    while (i < rows && Marked[i, pos.Value.Item2])
                    {
                        i++;
                    }
                    while (j < cols && Marked[pos.Value.Item1, j])
                    {
                        j++;
                    }
                    return i == rows || j == cols;
                }
                return false;
            }

            public (int, int)? GetPosition(int number)
            {
                var rows = Numbers.GetLength(0);
                var cols = Numbers.GetLength(1);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (Numbers[i, j] == number)
                        {
                            return (i, j);
                        }
                    }
                }
                return null;
            }

            public bool Check()
            {
                var rows = Numbers.GetLength(0);
                var cols = Numbers.GetLength(1);
                for (int i = 0; i < rows; i++)
                {
                    var rowComplete = true;
                    for (int j = 0; j < cols; j++)
                    {
                        if (!Marked[i, j])
                        {
                            rowComplete = false;
                            break;
                        }
                    }
                    if (rowComplete)
                        return true;
                }

                for (int j = 0; j < cols; j++)
                {
                    var colComplete = true;
                    for (int i = 0; i < rows; i++)
                    {
                        if (!Marked[i, j])
                        {
                            colComplete = false;
                            break;
                        }
                    }
                    if (colComplete)
                        return true;
                }
                return false;
            }
        }

        public override long Part1()
        {
            var playableBoards = Boards.Select(b => new PlayableBoard(b)).ToList();
            foreach (var number in Numbers)
            {
                var board = playableBoards.FirstOrDefault(b => b.Mark(number));
                if (board is not null)
                {
                    var sum = 0;
                    for (int r = 0; r < Board.Size; r++)
                    {
                        for (int c = 0; c < Board.Size; c++)
                        {
                            if (!board.Marked[r,c])
                            {
                                sum += board.Numbers[r, c];
                            }
                        }
                    }
                    return (long)sum * number;
                }
            }
            return 0;
        }

        public override long Part2()
        {
            var playableBoards = Boards.Select(b => new PlayableBoard(b)).ToList();
            var initialPlayableBoards = playableBoards.ToList();
            foreach (var number in Numbers)
            {
                var lastBoard = playableBoards.Count() == 1 ? playableBoards.First() : null;
                playableBoards = playableBoards.Where(b => !b.Mark(number)).ToList();
                if (playableBoards.Count() == 0)
                {
                    var check = initialPlayableBoards.Count(b => !b.Check());
                    var sum = 0;
                    for (int r = 0; r < Board.Size; r++)
                    {
                        for (int c = 0; c < Board.Size; c++)
                        {
                            if (!lastBoard.Marked[r, c])
                            {
                                sum += lastBoard.Numbers[r, c];
                            }
                        }
                    }
                    return (long)sum * number;
                }
            }
            return 0;
        }

        protected override void LoadData(List<string> input)
        {
            Numbers = input[0].Split(',').Select(x => int.Parse(x)).ToList();
            for (int i = 2; i < input.Count; i += Board.Size + 1)
            {
                var board = new Board();
                for (int r = 0; r < Board.Size; r++)
                {
                    var row = input[i+r].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    for (int c = 0; c < Board.Size; c++)
                    {
                        board.Numbers[r, c] = int.Parse(row[c]);
                    }
                }
                Boards.Add(board);
            }
        }
    }
}
