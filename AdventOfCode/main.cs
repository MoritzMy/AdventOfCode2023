﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class main
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 1 Solutions:");
            Day1 day1 = new Day1();
            day1.DayOneSolutions();

            Console.WriteLine("\nDay 2 Solutions:");
            Day2 day2 = new Day2();
            day2.Day2Solutions();

            Console.WriteLine("\nDay 3 Solutions:");
            Day3 day3 = new Day3();
            day3.Day3Solutions();
            
        }

        
    }
}