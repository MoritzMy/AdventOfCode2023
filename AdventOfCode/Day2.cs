using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Day2
    {
        const int redMax = 12;
        const int greenMax = 13;
        const int blueMax = 14;
        public void Day2Solutions()
        {
            int GameCounter = 1;
            int sum = 0;
            StreamReader sr = new StreamReader("Day2Puzzle.txt");
            int minDiceSum = 0;

            Stopwatch watch = Stopwatch.StartNew();

            while (!sr.EndOfStream)
            {
                string currentLine = sr.ReadLine();
                string currentGame = currentLine.Split(": ")[1];
                string[] Rounds = currentGame.Split("; ");

                bool Valid = true;
                for (int i = 0; i < Rounds.Length; i++)
                {
                    string currentRound = Rounds[i];
                    if (isNotValid(currentRound))
                    {
                        Valid = false;
                        break;
                    }
                }
                if (Valid)
                {
                    sum += GameCounter;
                }

                GameCounter++;
                minDiceSum += GetPower(currentGame);

            }
            watch.Stop();
            Console.WriteLine($"Part 1: {sum} in {watch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Part 2: {minDiceSum} in {watch.ElapsedMilliseconds} ms");

        }
        static int GetPower(string currentGame)
        {
            int maxGreen = 1;
            int maxBlue = 1;
            int maxRed = 1;

            foreach (string round in currentGame.Split("; "))
            {
                foreach (string entry in round.Split(", "))
                {
                    string[] ColorValues = entry.Split(" ");
                    int currentAmount = Convert.ToInt32(ColorValues[0]);
                    switch (ColorValues[1])
                    {
                        case "green":
                            if (currentAmount > maxGreen) { maxGreen = currentAmount; }
                            break;
                        case "blue":
                            if (currentAmount > maxBlue) { maxBlue = currentAmount; }
                            break;
                        case "red":
                            if (currentAmount > maxRed) { maxRed = currentAmount; }
                            break;
                        default: { break; }
                    }
                }
            }
            return maxGreen * maxBlue * maxRed;
        }
        static bool isNotValid(string Round)
        {
            Dictionary<string, int> ColorMap = new Dictionary<string, int> { { "green", greenMax }, { "red", redMax }, { "blue", blueMax } };
            string[] entries = Round.Split(", ");
            foreach (string entry in entries)
            {
                string[] ColorValues = entry.Split(" ");
                int amount = Convert.ToInt32(ColorValues[0]);
                ColorMap.TryGetValue(ColorValues[1], out int MaxValue);
                if (amount > MaxValue) return true;
            }
            return false;
        }
    }
}
