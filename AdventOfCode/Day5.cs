//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Design;
//using System.Diagnostics.Metrics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace adventofcode_2023
{
    internal class Day5
    {
        public void Day5Solutions()
        {
            StreamReader sr = new StreamReader("C:\\Users\\PC\\source\\repos\\AdventOfCode\\Day5Puzzle.txt");
            string text = sr.ReadToEnd();
            sr.Close();
            string[] lines = text.Split("\n");
            string[] seedsString = lines[0].Split(": ")[1].Split(" ");
            uint[] seeds = new uint[seedsString.Length];
            for (int i = 0; i < seedsString.Length; i++)
            {
                seeds[i] = Convert.ToUInt32(seedsString[i]);
            }

            List<List<((uint destination, uint source) start, uint range)>> MapsList = ReturnTupleList(lines);
            text = "";
            for (int j = 0; j < seeds.Length; j++)
            {
                for (int i = 0; i < MapsList.Count; i++)
                {
                    Console.Write(seeds[j] + " ");
                    for (int p = 0; p < MapsList[i].Count; p++)
                    {
                        if (seeds[j] >= MapsList[i][p].start.source  && seeds[j] < MapsList[i][p].start.source + MapsList[i][p].range)
                        {
                            seeds[j] = MapsList[i][p].start.destination + (seeds[j] - MapsList[i][p].start.source);
                            break;
                        }
                    }

                }
                Console.Write("\n");
            }
            Console.WriteLine(FindLowestSeed(seeds));
        }
        public List<((uint, uint), uint)> GetMap(List<string> list)
        {
            List<((uint destination, uint source) start, uint range)> Map = new List<((uint, uint), uint)>();
            foreach (string item in list)
            {
                string[] values = item.Split(" ");
                uint destinationStart = Convert.ToUInt32(values[0]);
                uint sourceStart = Convert.ToUInt32(values[1]);
                uint range = Convert.ToUInt32(values[2]);
                Map.Add(((destinationStart, sourceStart), range));

            }
            return Map;

        }
        public List<List<((uint, uint), uint)>> ReturnTupleList(string[] lines)
        {
            List<string> lineList = new List<string>();
            List<List<((uint destination, uint source) start, uint range)>> MapsList = new List<List<((uint, uint), uint)>>();

            foreach (string line in lines)
            {
                if (line.Contains("map:") || line.Contains("seeds:"))
                {
                    continue;
                }
                else if (line.Length <= 1)
                {
                    if (lineList.Count != 0)
                    {
                        MapsList.Add(GetMap(lineList));
                    }
                    lineList = new List<string>();
                    continue;
                }
                else
                {
                    lineList.Add(line);
                }
            }
            return MapsList;
        }
        public uint FindLowestSeed(uint[] seeds)
        {
            uint lowestseed = seeds[0];
            foreach (uint seed in seeds)
            {
                if (seed < lowestseed)
                {
                    lowestseed = seed;
                }
            }
            return lowestseed;
        }
    }
}
//        The Idea behind this code was kinda cool, but used 14 GBs of RAM. :,)
//        public void Day5Solutions()
//        {
//            StreamReader sr = new StreamReader("C:\\Users\\PC\\source\\repos\\AdventOfCode\\Day5Puzzle.txt");
//            string text = sr.ReadToEnd();
//            sr.Close();

//            string[] lines = text.Split('\n');
//            string[] seedsString = (lines[0].Split(": ")[1]).Split(" ");
//            uint[] seeds = new uint[seedsString.Length];

//            for(int i = 0; i < seedsString.Length; i++)
//            {
//                seeds[i] = Convert.ToUInt32(seedsString[i]);
//            }

//            List<Dictionary<uint, uint>> MapsList = ReturnMapList(lines);
//            text = "";

//            for(int j = 0; j < seeds.Length; j++) 
//            {
//                for(int i = 0; i < MapsList.Count; i++)
//                {
//                    Console.Write(seeds[j] + " ");
//                    if (MapsList[i].ContainsKey(seeds[j])) 
//                    {

//                        MapsList[i].TryGetValue(seeds[j], out uint mappedValue);
//                        seeds[j] = mappedValue;
//                    }

//                }
//                Console.Write("\n");
//            }
//            foreach (uint seed in seeds)
//            {
//                Console.Write(seed + " ");
//            }
//            Console.WriteLine(FindLowestSeed(seeds));

//        }
//        public List<Dictionary<uint, uint>> ReturnMapList(string[] lines)
//        {
//            List<string> lineList = new List<string>();
//            List<Dictionary<uint, uint>> MapsList = new List<Dictionary<uint, uint>>();

//            foreach (string line in lines)
//            {
//                if (line.Contains("map:") || line.Contains("seeds:"))
//                {
//                    continue;
//                }
//                else if (line.Length <= 1)
//                {
//                    if (lineList.Count != 0)
//                    {
//                        MapsList.Add(GetMap(lineList));
//                    }
//                    lineList = new List<string>();
//                    continue;
//                }
//                else
//                {
//                    lineList.Add(line);
//                }
//            }
//            return MapsList;
//        }
//        public Dictionary<uint, uint> GetMap(List<string> list)
//        {
//            Dictionary<uint, uint> Map = new Dictionary<uint, uint>();
//            foreach (string item in list)
//            {
//                string[] values = item.Split(" ");
//                uint destinationStart = Convert.ToUInt32(values[0]);
//                uint sourceStart = Convert.ToUInt32(values[1]);
//                uint range = Convert.ToUInt32(values[2]);

//                for (uint i = 0; i < range; i++)
//                {
//                    Map.Add(sourceStart + i, destinationStart + i);
//                }
//            }
//            return Map;
//        }
//        public uint FindLowestSeed(uint[] seeds)
//        {
//            uint lowestSeed = seeds[0];
//            foreach (uint seed in seeds)
//            {
//                if (seed < lowestSeed)
//                {
//                    lowestSeed = seed;
//                }
//            }
//            return lowestSeed;
//        } 
