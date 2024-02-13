using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Day4
    {
        public void Day4Solutions() 
        {
            StreamReader sr = new StreamReader("Day4Puzzle.txt");
            double scoreSum = 0;
            int counter = 0;
            int[] gameNumbers = Enumerable.Repeat(1, 205).ToArray();

            Stopwatch watch = Stopwatch.StartNew();

            while (!sr.EndOfStream)
            {
                string game = sr.ReadLine();
                string cards = game.Split(": ")[1];
                string winningCards = cards.Split("| ")[0];
                string cardsInHand = cards.Split("| ")[1];
                List<int> WinningCards = GetCards(winningCards);
                List<int> CardsInHand = GetCards(cardsInHand);

                // Part 1
                int score = GetScoreForWins(WinningCards, CardsInHand);
                if (score != -1) scoreSum += Math.Pow(2, score);

                // Part 2
                IncreaseBasedOnScore(score, counter, gameNumbers);
                counter++;

            }
            sr.Close();

            watch.Stop();
            Console.WriteLine($"Part 1: {scoreSum} in {watch.ElapsedMilliseconds} ms");
            watch.Restart();
            int sumCards = SumUpAmountOfCards(gameNumbers);
            watch.Stop();
            Console.WriteLine($"Part 2: {sumCards} in {watch.ElapsedMilliseconds} ms");
        }
        public List<int> GetCards(string cardsString)
        {
            List<int> Cards = new List<int>();
            string[] splitCards = cardsString.Split(" ");
            foreach (string card in splitCards)
            {
                if (card != "") Cards.Add(Convert.ToInt32(card));
            }
            return Cards;
        }

        public int GetScoreForWins(List<int> WinningCards, List <int> CardsInHand) 
        {
            int score = -1;
            foreach (int card in WinningCards)
            {
                if (CardsInHand.Contains(card)) score++;
            }
            return score;
        }
        public void IncreaseBasedOnScore(int score, int counter, int[] gameNumbers)
        {
            for(int i = counter + 1; i <= counter + score + 1 && i < 205; i++)
            {
                gameNumbers[i] += gameNumbers[counter];
            }
        }
        public int SumUpAmountOfCards(int[] gameNumbers)
        {
            int sum = 0;
            foreach (int gameNumber in gameNumbers)
            {
                sum += gameNumber;
            }
            return sum;
        }
    }
}
