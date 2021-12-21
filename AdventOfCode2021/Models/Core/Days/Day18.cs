using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day18 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day18;

        public List<SnailfishNumber> SnailfishNumbers = new List<SnailfishNumber>();

        public override long Part1()
        {
            var number = SnailfishNumbers[0];
            for (int i = 1; i < SnailfishNumbers.Count; i++)
            {
                number = number + SnailfishNumbers[i];
            }
            return number.Magnitude;
        }

        public override long Part2()
        {
            long maxMag = 0;
            for (int i = 0; i < SnailfishNumbers.Count; i++)
            {
                for (int j = i+1; j < SnailfishNumbers.Count; j++)
                {
                    var sumij = SnailfishNumbers[i] + SnailfishNumbers[j];
                    var sumji = SnailfishNumbers[j] + SnailfishNumbers[i];
                    var magij = sumij.Magnitude;
                    var magji = sumji.Magnitude;
                    maxMag = Math.Max(maxMag, Math.Max(magji, magij));
                }
            }
            return maxMag;
        }

        public abstract class SnailfishNumber
        {
            public NumberPair? Parent;
            public abstract long Magnitude { get; }
            public abstract SnailfishNumber Clone();
            public abstract override string ToString();

            static public SnailfishNumber operator + (SnailfishNumber lhs, SnailfishNumber rhs)
            {
                var newNumber = new NumberPair(lhs.Clone(), rhs.Clone());
                return newNumber;
            }

            static public SnailfishNumber FromString(string s)
            {
                var i = 0;
                return FromString(s, ref i);
            }
            static public SnailfishNumber FromString(string s, ref int i)
            {
                var isPair = s[i] == '[';
                if (isPair)
                {
                    i++;
                    var left = FromString(s,ref i);
                    i++;
                    var right = FromString(s,ref i);
                    i++;
                    var newNumber = new NumberPair(left, right);
                    return newNumber;
                }
                else
                {
                    var sign = s[i] == '-';
                    var number = int.Parse(s.Substring(i, sign ? 2 : 1));
                    i += sign ? 2 : 1;
                    return new RegularNumber(number);
                }
            }

            int Depth()
            {
                var depth = 0;
                var parent = Parent;
                while (parent != null)
                {
                    parent = parent.Parent;
                    depth++;
                }
                return depth;
            }

            protected void Reduce()
            {
                var inOrderTraverseList = new List<SnailfishNumber>();
                var toExplode = new SortedSet<int>();
                var toSplit = new SortedSet<int>();
                InOrderTraverse(this);
                while (toExplode.Count > 0 || toSplit.Count > 0)
                {
                    if (toExplode.Count > 0)
                    {
                        var i = toExplode.First();
                        toExplode.Remove(i);
                        var explode = (NumberPair)inOrderTraverseList[i];
                        for (int j = i - 2; j >= 0; j--)
                        {
                            if (inOrderTraverseList[j] is RegularNumber reg)
                            {
                                reg.Value += ((RegularNumber)explode.LeftNumber).Value;
                                if (reg.Value > 9)
                                {
                                    toSplit.Add(j);
                                }
                                break;
                            }
                        }
                        for (int j = i + 2; j < inOrderTraverseList.Count; j++)
                        {
                            if (inOrderTraverseList[j] is RegularNumber reg)
                            {
                                reg.Value += ((RegularNumber)explode.RightNumber).Value;
                                if (reg.Value > 9)
                                {
                                    toSplit.Add(j);
                                }
                                break;
                            }
                        }
                        var newNumber = new RegularNumber(0);
                        newNumber.Parent = explode.Parent;
                        if (explode.Parent.LeftNumber == explode)
                        {
                            explode.Parent.LeftNumber = newNumber;
                        }
                        else
                        {
                            explode.Parent.RightNumber = newNumber;
                        }
                        toSplit.Remove(i - 1);
                        toSplit.Remove(i + 1);
                        inOrderTraverseList[i] = newNumber;
                        inOrderTraverseList.RemoveAt(i + 1);
                        inOrderTraverseList.RemoveAt(i - 1);
                        toExplode = new SortedSet<int>(toExplode.Select(e => e > i - 1 ? e > i + 1 ? e - 2 : e - 1 : e));
                        toSplit = new SortedSet<int>(toSplit.Select(e => e > i - 1 ? e > i + 1 ? e - 2 : e - 1 : e));
                        continue;
                    }
                    if (toSplit.Count > 0)
                    {
                        var i = toSplit.First();
                        toSplit.Remove(i);
                        var split = (RegularNumber)inOrderTraverseList[i];
                        var value = split.Value;
                        if (split.Depth() == 4)
                        {
                            toExplode.Add(i);
                        }
                        var left = new RegularNumber(value / 2);
                        var right = new RegularNumber(value / 2 + value % 2);
                        var newNumber = new NumberPair(left, right, false);
                        newNumber.Parent = split.Parent;
                        if (split.Parent.LeftNumber == split)
                        {
                            split.Parent.LeftNumber = newNumber;
                        }
                        else
                        {
                            split.Parent.RightNumber = newNumber;
                        }
                        inOrderTraverseList[i] = newNumber;
                        toExplode = new SortedSet<int>(toExplode.Select(e => e > i - 1 ? e > i + 1 ? e + 2 : e + 1 : e));
                        toSplit = new SortedSet<int>(toSplit.Select(e => e > i - 1 ? e > i + 1 ? e + 2 : e + 1 : e));
                        inOrderTraverseList.Insert(i, left);
                        inOrderTraverseList.Insert(i + 2, right);
                        if (left.Value > 9)
                        {
                            toSplit.Add(i);
                        }
                        if (right.Value > 9)
                        {
                            toSplit.Add(i + 2);
                        }
                        continue;
                    }
                }

                void InOrderTraverse(SnailfishNumber number, int depth = 0)
                {
                    if (number is NumberPair pair)
                    {
                        InOrderTraverse(pair.LeftNumber, depth + 1);
                        if (depth >= 4)
                        {
                            toExplode.Add(inOrderTraverseList.Count);
                        }
                        inOrderTraverseList.Add(number);
                        InOrderTraverse(pair.RightNumber, depth + 1);
                    }
                    else if (number is RegularNumber reg)
                    {
                        if (reg.Value > 9)
                        {
                            toSplit.Add(inOrderTraverseList.Count);
                        }
                        inOrderTraverseList.Add(number);
                    }
                }
            }

            public RegularNumber? FindLeft()
            {
                var cur = this;
                while (cur != null)
                {
                    if (cur.Parent == null)
                    {
                        return null;
                    }
                    if (cur.Parent.RightNumber == cur)
                    {
                        cur = cur.Parent;
                        break;
                    }
                    cur = cur.Parent;
                }
                while (cur is NumberPair pair)
                {
                    cur = pair.RightNumber;
                }
                return (RegularNumber)cur;
            }

            public RegularNumber FindRight()
            {
                var cur = this;
                while (cur != null)
                {
                    if (cur.Parent == null)
                    {
                        return null;
                    }
                    if (cur.Parent.LeftNumber == cur)
                    {
                        cur = cur.Parent;
                        break;
                    }
                    cur = cur.Parent;
                }
                while (cur is NumberPair pair)
                {
                    cur = pair.LeftNumber;
                }
                return (RegularNumber)cur;
            }
        }

        public class NumberPair : SnailfishNumber
        {
            public SnailfishNumber LeftNumber;
            public SnailfishNumber RightNumber;
            public override long Magnitude => 3 * LeftNumber.Magnitude + 2 * RightNumber.Magnitude;

            public NumberPair(SnailfishNumber left, SnailfishNumber right, bool reduce = true)
            {
                LeftNumber = left;
                RightNumber = right;
                left.Parent = this;
                right.Parent = this;
                if (reduce)
                {
                    Reduce();
                }
            }

            public override SnailfishNumber Clone()
            {
                return new NumberPair(LeftNumber.Clone(),RightNumber.Clone(), false);
            }

            public override string ToString()
            {
                return $"[{LeftNumber},{RightNumber}]";
            }
        }

        public class RegularNumber : SnailfishNumber
        {
            public int Value;
            public override long Magnitude => Value;

            public RegularNumber(int value)
            {
                Value = value;
            }

            public override SnailfishNumber Clone()
            {
                return new RegularNumber(Value);
            }

            public override string ToString()
            {
                return $"{Value}";
            }
        }

        protected override void LoadData(List<string> input)
        {

            foreach(var line in input)
            {
                SnailfishNumbers.Add(SnailfishNumber.FromString(line));
            }
        }
    }
}
