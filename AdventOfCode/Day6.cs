using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace adventOfCode_2023
{
    internal class Day6
    {
        public void Day6Solutions()
        {
            StreamReader sr = new StreamReader("Day6Puzzle.txt");
            string timeString = RemoveUnnecessaryWhiteSpace(sr.ReadLine().Split(": ")[1]);
            string distanceString = RemoveUnnecessaryWhiteSpace(sr.ReadLine().Split(": ")[1]);

            List<long> time = new List<long>();
            List<long> distance = new List<long>();

            string[] timeStringArray = timeString.Split(" ");
            string[] distanceStringArray = distanceString.Split(" ");

            Stopwatch watch = Stopwatch.StartNew();

            for(long i = 0; i < distanceStringArray.Length; i++)
            {
                time.Add(long.Parse(timeStringArray[i]));
                distance.Add(long.Parse(distanceStringArray[i]));
            }
            List<long> amountOfWinningPossibilities = new List<long>();
            long counter = 0;
            for(int i = 0; i < time.Count; i++)                        
            {
                long[] currentTimes = determineDistances(time[i]);
                for (int j = 0;  j < currentTimes.Length; j++)
                {
                    if (distance[i] < currentTimes[j])
                    {
                        counter++;
                    }
                }
                amountOfWinningPossibilities.Add(counter);
                counter = 0;
            }
            long sum = 1;
            foreach (long winningTime in  amountOfWinningPossibilities)
            {
                sum *= winningTime;
            }

            watch.Stop();
            Console.WriteLine($"Part 1: {sum} in {watch.ElapsedMilliseconds} ms");

            // Part 2
            watch = Stopwatch.StartNew();
            string ConcatinatedTime = "";
            string ConcatinatedDistance = "";

            for(int i = 0; i < timeStringArray.Length; i++)
            {
                ConcatinatedTime += timeStringArray[i];
                ConcatinatedDistance += distanceStringArray[i];
            }
            long bigTime = Convert.ToInt64(ConcatinatedTime); // Finding variable names is hard ok
            long bigDistance = Convert.ToInt64(ConcatinatedDistance);

            long[] currentTimes2 = determineDistances(bigTime);
            counter = 0;
            for (int j = 0; j < currentTimes2.Length; j++)
            {
                if (bigDistance < currentTimes2[j])
                {
                    counter++;
                }
            }
            int finalWinningTime = determineWinningTimes(bigTime, bigDistance);
            watch.Stop();
            Console.WriteLine($"Part 2: {finalWinningTime} in {watch.ElapsedMilliseconds} ms");


        }
        public string RemoveUnnecessaryWhiteSpace(string input)
        {
            bool spaceSeen = false;
            bool firstCharSeen = false;
            string normalizedString = "";
            foreach (char c in input)
            {
                if (c == ' ' && spaceSeen == true)
                {
                    continue;
                }
                else if (c == ' ' && firstCharSeen == true)
                {
                    spaceSeen = true;
                    normalizedString += c;
                }
                else if (c != ' ')
                {
                    firstCharSeen = true;
                    normalizedString += c;
                    spaceSeen = false;
                    
                }
            }
            return normalizedString;
        }
        public long[] determineDistances(long time)
        {
            long[] distance = new long[time];
            for(long i = 0; i < time; i++)
            {
                distance[i] = i * (time - i);
            }
            return distance;
        }
        public int determineWinningTimes(long time, long distance)
        {
            long[] currentTimeArray = determineDistances(time);
            int counter = 0;
            for (int j = 0; j < currentTimeArray.Length; j++)
            {
                if (currentTimeArray[j] < distance)
                {
                    counter++;
                }
                else
                {
                    if ((time % 2 == 0));
                    return (int)(time - 2 * counter) + 1;
                }
            }
            return 0;
        }
    }
}
