using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Days
{
    internal class Day16 : DayBase
    {
        public override DayPuzzle DayPuzzle => DayPuzzle.Day16;
        string HexString = "";
        string BinaryString = "";

        class Chunk
        {
            public int Version;
            public int TypeID;
            public List<Chunk>? InnerChunks = null;
            public long Value;
        }

        public override long Part1()
        {
            var chunk = ReadChunk(BinaryString);
            var versionSum = GetVersionSum(chunk);
            return versionSum;

            long GetVersionSum(Chunk chunk)
            {
                return chunk.Version + (chunk.InnerChunks?.Sum(c => GetVersionSum(c)) ?? 0);
            }
        }

        public override long Part2()
        {
            var chunk = ReadChunk(BinaryString);
            var value = GetValue(chunk);
            return value;

            long GetValue(Chunk chunk)
            {
                switch (chunk.TypeID)
                {
                    case 0:
                        return chunk.InnerChunks.Sum(c => GetValue(c));
                    case 1:
                        long prod = 1;
                        chunk.InnerChunks.ForEach(c => prod *= GetValue(c));
                        return prod;
                    case 2:
                        return chunk.InnerChunks.Min(c => GetValue(c));
                    case 3:
                        return chunk.InnerChunks.Max(c => GetValue(c));
                    case 4:
                        return chunk.Value;
                    case 5:
                        return GetValue(chunk.InnerChunks[0]) > GetValue(chunk.InnerChunks[1]) ? 1 : 0;
                    case 6:
                        return GetValue(chunk.InnerChunks[0]) < GetValue(chunk.InnerChunks[1]) ? 1 : 0;
                    case 7:
                        return GetValue(chunk.InnerChunks[0]) == GetValue(chunk.InnerChunks[1]) ? 1 : 0;
                    default:
                        return 0;
                }
            }
        }

        static Chunk ReadChunk(string binaryString)
        {
            var curBit = 0;
            return ReadChunk(binaryString, ref curBit);
        }

        static Chunk ReadChunk(string binaryString, ref int parentCurBit)
        {
            var chunk = new Chunk();
            var curBit = parentCurBit;
            chunk.Version = Next(3);
            chunk.TypeID = Next(3);

            switch (chunk.TypeID)
            {
                case 4:
                    var keepReading = true;
                    chunk.Value = 0;
                    while (keepReading)
                    {
                        keepReading = NextBit();
                        chunk.Value <<= 4;
                        chunk.Value += Next(4);
                    }
                    break;

                default:
                    chunk.InnerChunks = new List<Chunk>();
                    var lengthTypeID = NextBit() ? 1 : 0;
                    switch (lengthTypeID)
                    {
                        case 0:
                            var innerChunksBitCount = Next(15);
                            var finalInnerChunkBit = curBit + innerChunksBitCount;
                            while (curBit < finalInnerChunkBit)
                            {
                                chunk.InnerChunks.Add(ReadChunk(binaryString, ref curBit));
                            }
                            break;
                        case 1:
                            var innerChunks = Next(11);
                            while (chunk.InnerChunks.Count < innerChunks)
                            {
                                chunk.InnerChunks.Add(ReadChunk(binaryString, ref curBit));
                            }
                            break;
                    }    
                    break;
            }

            parentCurBit = curBit;
            return chunk;

            bool NextBit()
            {
                return binaryString[curBit++] == '1';
            }

            int Next(int count)
            {
                int val = 0;
                for (int i = 0; i < count; i++)
                {
                    val <<= 1;
                    val += NextBit() ? 1 : 0;
                }
                return val;
            }
        }

        protected override void LoadData(List<string> input)
        {
            HexString = input[0];
            BinaryString = String.Join(String.Empty, HexString.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
        }
    }
}
