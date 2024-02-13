using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day1
    {
        public void DayOneSolutions()
        {
            int sum = 0;
            StreamReader sr = new StreamReader("Day1Puzzle.txt");
            Stopwatch watch = Stopwatch.StartNew();
            while (!sr.EndOfStream)
            {
                int firstDigit = -1;
                int lastDigit = -1;
                string currentLine = sr.ReadLine();
                currentLine = ReplaceAllSpelledNums(currentLine);

                foreach (char c in currentLine)
                {
                    if (char.IsDigit(c) && firstDigit == -1)
                    {
                        firstDigit = c - '0';
                    }
                    if (char.IsDigit(c))
                    {
                        lastDigit = c - '0';
                    }
                }
                sum += firstDigit * 10 + lastDigit;
            }
            sr.Close();
            watch.Stop();
            Console.WriteLine($"Part 2: {sum} in {watch.ElapsedMilliseconds} ms");
        }

        static string ReplaceAllSpelledNums(string line)
        {
            List<string> list = new List<string>() { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            List<string> output = new List<string>() { "z0o", "o1e", "t2o", "th3ee", "fo4r", "fi5e", "s6x", "se7en", "ei8th", "n9e" };

            for (int i = 0; i < list.Count; i++)
            {
                line = Regex.Replace(line, list[i], output[i]);
            }

            return line;
        }
    }
}