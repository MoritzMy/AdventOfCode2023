//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Design;
//using System.Diagnostics.Metrics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Transactions;

namespace adventofcode_2023
{
    internal class Day5
    {
        public void Day5Solutions()
        {
            var watch = new System.Diagnostics.Stopwatch();

            StreamReader sr = new StreamReader("C:\\Users\\PC\\source\\repos\\AdventOfCode\\Day5Puzzle.txt");
            string text = sr.ReadToEnd();
            sr.Close();
            string[] lines = text.Split("\n");
            string[] seedsString = lines[0].Split(": ")[1].Split(" ");
            long[] seeds = new long[seedsString.Length];
            for (int i = 0; i < seedsString.Length; i++)
            {
                seeds[i] = Convert.ToInt64(seedsString[i]);
            }
            long[] seedsCopy = new long[seeds.Length];
            seeds.CopyTo(seedsCopy, 0);

            watch.Start();

            List<List<((long destination, long source) start, long range)>> MapsList = ReturnTupleList(lines);
            text = "";
            for (int j = 0; j < seeds.Length; j++)
            {
                for (int i = 0; i < MapsList.Count; i++)
                {
                    for (int p = 0; p < MapsList[i].Count; p++)
                    {
                        if (seeds[j] >= MapsList[i][p].start.source && seeds[j] < MapsList[i][p].start.source + MapsList[i][p].range)
                        {
                            seeds[j] = MapsList[i][p].start.destination + (seeds[j] - MapsList[i][p].start.source);
                            break;
                        }
                    }

                }
            }
            watch.Stop();
            Console.WriteLine($"{FindLowestSeed(seeds)} Calculated in {watch.ElapsedMilliseconds} ms");

            // part 2
            bool isRange = false;
            long[] seedsValue = new long[seedsCopy.Length / 2];
            long[] seedsRange = new long[seedsCopy.Length / 2];
            for (int i = 0; i < seeds.Length; i++)
            {
                if (isRange)
                {
                    seedsRange[((i - 1) / 2)] = seedsCopy[i];
                    isRange = false;
                }
                else
                {
                    seedsValue[i / 2] = seedsCopy[i];
                    isRange = true;
                }
            }
            bool lowestLocationFound = false;
            long locationCounter = 0;
            long tempLocation = locationCounter;
            int locationCounterIncrease = 1000000;


            watch = System.Diagnostics.Stopwatch.StartNew();

            while (!lowestLocationFound)
            {
                tempLocation = locationCounter;
                for (int i = MapsList.Count - 1; i >= 0; i--)
                {
                    for (int j = 0; j < MapsList[i].Count; j++)
                    {
                        if (tempLocation >= MapsList[i][j].start.destination && tempLocation < MapsList[i][j].start.destination + MapsList[i][j].range)
                        {
                            tempLocation = MapsList[i][j].start.source + (tempLocation - MapsList[i][j].start.destination);
                            break;
                        }
                    }
                }

                for (int k = 0; k < seedsValue.Length; k++)
                {
                    if (tempLocation >= seedsValue[k] && tempLocation < seedsValue[k] + seedsRange[k])
                    {
                        if (locationCounterIncrease == 1)
                        {
                            Console.Write($"Solution to Part 2: {locationCounter} (Location), {tempLocation} (corresponding Seed) ");
                            lowestLocationFound = true;
                            break;
                        }
                        else
                        {
                            locationCounter -= locationCounterIncrease;
                            locationCounterIncrease /= 10;
                        }
                    }
                }
                locationCounter += locationCounterIncrease;
            }
            watch.Stop();
            Console.Write($"Calculated in {watch.ElapsedMilliseconds} ms");
        }

        public List<((long, long), long)> GetMap(List<string> list)
        {
            List<((long destination, long source) start, long range)> Map = new List<((long, long), long)>();
            foreach (string item in list)
            {
                string[] values = item.Split(" ");
                long destinationStart = Convert.ToInt64(values[0]);
                long sourceStart = Convert.ToInt64(values[1]);
                long range = Convert.ToInt64(values[2]);
                Map.Add(((destinationStart, sourceStart), range));

            }
            return Map;

        }
        public List<List<((long, long), long)>> ReturnTupleList(string[] lines)
        {
            List<string> lineList = new List<string>();
            List<List<((long destination, long source) start, long range)>> MapsList = new List<List<((long, long), long)>>();

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
        public long FindLowestSeed(long[] seeds)
        {
            long lowestseed = seeds[0];
            foreach (long seed in seeds)
            {
                if (seed < lowestseed)
                {
                    lowestseed = seed;
                }
            }
            return lowestseed;
        }
        public long FindHighestSeed(long[] seeds)
        {
            long highestseed = seeds[0];
            foreach (long seed in seeds)
            { 
                if (seed < highestseed)
                {
                    highestseed = seed;
                } 
            }
            return highestseed;
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
//            long[] seeds = new long[seedsString.Length];

//            for(int i = 0; i < seedsString.Length; i++)
//            {
//                seeds[i] = Convert.ToInt64(seedsString[i]);
//            }

//            List<Dictionary<long, long>> MapsList = ReturnMapList(lines);
//            text = "";

//            for(int j = 0; j < seeds.Length; j++) 
//            {
//                for(int i = 0; i < MapsList.Count; i++)
//                {
//                    Console.Write(seeds[j] + " ");
//                    if (MapsList[i].ContainsKey(seeds[j])) 
//                    {

//                        MapsList[i].TryGetValue(seeds[j], out long mappedValue);
//                        seeds[j] = mappedValue;
//                    }

//                }
//                Console.Write("\n");
//            }
//            foreach (long seed in seeds)
//            {
//                Console.Write(seed + " ");
//            }
//            Console.WriteLine(FindLowestSeed(seeds));

//        }
//        public List<Dictionary<long, long>> ReturnMapList(string[] lines)
//        {
//            List<string> lineList = new List<string>();
//            List<Dictionary<long, long>> MapsList = new List<Dictionary<long, long>>();

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
//        public Dictionary<long, long> GetMap(List<string> list)
//        {
//            Dictionary<long, long> Map = new Dictionary<long, long>();
//            foreach (string item in list)
//            {
//                string[] values = item.Split(" ");
//                long destinationStart = Convert.ToInt64(values[0]);
//                long sourceStart = Convert.ToInt64(values[1]);
//                long range = Convert.ToInt64(values[2]);

//                for (long i = 0; i < range; i++)
//                {
//                    Map.Add(sourceStart + i, destinationStart + i);
//                }
//            }
//            return Map;
//        }
//        public long FindLowestSeed(long[] seeds)
//        {
//            long lowestSeed = seeds[0];
//            foreach (long seed in seeds)
//            {
//                if (seed < lowestSeed)
//                {
//                    lowestSeed = seed;
//                }
//            }
//            return lowestSeed;
//        } 
