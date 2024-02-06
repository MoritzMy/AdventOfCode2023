using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Day3
    {
        public void Day3Solutions()
        {
            string placeHolder = "..............................................................................................................................................\r\n";
            string currentLine = placeHolder;
            string nextLine = placeHolder;
            string aboveLine;
            StreamReader sr = new StreamReader("C:\\Users\\PC\\source\\repos\\AdventOfCode\\Day3Puzzle.txt");
            int sum = 0;
            int lineCounter = 0;
            int sumOfGearRatio = 0;
            int GearRatioSum = 0;
            while (!sr.EndOfStream)
            {
                aboveLine = currentLine;
                currentLine = nextLine;
                nextLine = "." + sr.ReadLine() + ".";
                sum += GetNumsFromLine(aboveLine, currentLine, nextLine, lineCounter, out sumOfGearRatio);
                lineCounter++;
                GearRatioSum += sumOfGearRatio;

            }
            sr.Close();
            aboveLine = currentLine;
            currentLine = nextLine;
            nextLine = placeHolder;
            sum += GetNumsFromLine(aboveLine, currentLine, nextLine, lineCounter, out int sumOfGearRatio2);
            GearRatioSum += sumOfGearRatio2;

            Console.WriteLine(sum);
            Console.WriteLine(GearRatioSum);

        } 
        public int GetNumsFromLine(string aboveLine, string currentLine, string nextLine, int lineCounter, out int sumOfGearRatio)
        {
            string currentNum = "";
            int linesum = 0;
            List<(int num, int starPostion)> starPosition = new List<(int, int)>();

            for (int i = 0; i < currentLine.Length; i++)
            {
                if (char.IsDigit(currentLine[i]))
                {
                    currentNum += currentLine[i];
                }
                else if ((!char.IsDigit(currentLine[i]) && currentNum != "") || (currentNum != "" && i == currentLine.Length - 1))
                {
                    int currentIndex = i - currentNum.Length;
                    for (int j = currentIndex - 1; j <= currentIndex + currentNum.Length; j++)
                    {
                        switch (j)
                        {
                            case int when currentLine[j] == '*':
                                starPosition.Add((Convert.ToInt32(currentNum), 1000 * lineCounter + j));
                                break;
                            case int when aboveLine[j] == '*':
                                starPosition.Add((Convert.ToInt32(currentNum), 1000 * (lineCounter - 1) + j));
                                break;
                            case int when nextLine[j] == '*':
                                starPosition.Add((Convert.ToInt32(currentNum), 1000 * (lineCounter + 1) + j));
                                break;
                            default:
                                break;
                        }

                        if ((!char.IsDigit(currentLine[j]) && currentLine[j] != '.') || (!char.IsDigit(aboveLine[j]) && aboveLine[j] != '.') || (!char.IsDigit(nextLine[j]) && nextLine[j] != '.'))
                        {
                            linesum += Convert.ToInt32(currentNum);
                        }
                    }
                    currentNum = "";
                }
                else currentNum = "";
            }
            sumOfGearRatio = 0;
            for (int i = 0; i < starPosition.Count - 1; i++)
            {
                Console.WriteLine(starPosition[i].num + ", " + starPosition[i].starPostion);
                for (int j = i + 1; j < starPosition.Count; j++)
                {
                    if (starPosition[i].starPostion == starPosition[j].starPostion)
                    {
                        Console.WriteLine("happens uwu");
                        sumOfGearRatio += starPosition[j].num * starPosition[i].num;
                    }
                }
            }
            return linesum;
        }
    }
}
