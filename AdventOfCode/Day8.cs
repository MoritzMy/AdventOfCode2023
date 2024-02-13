using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode_2023
{
    class StateLoops
    {
        string startingState;
        string finishingState;
        string currentState;
        int loopCounter;

        public StateLoops(string startingState)
        {
            this.startingState = startingState;
            this.currentState = String.Empty;
            this.finishingState = String.Empty;
            this.loopCounter = 0;
        }
        public string StartingState { get { return this.startingState; } }
        public string CurrentState { get { return this.currentState; } set { this.currentState = value; } }
        public string FinishingState { get { return this.finishingState; } set { this.finishingState = value; } }
        public int LoopCounter { get { return this.loopCounter; } set { this.loopCounter = value; } }
        public override string ToString()
        {
            return $"{StartingState}: {FinishingState} looped every {LoopCounter} steps";
        }
    }
    internal class Day8
    {
        public void Day8Solutions()
        {
            
            StreamReader sr = new StreamReader("Day8Puzzle.txt");
            string lrInstructions = sr.ReadLine();
            sr.ReadLine();

            Dictionary<string, string> LeftMap = new Dictionary<string, string>();
            Dictionary<string, string> RightMap = new Dictionary<string, string>();
            List<string> StartingStates = new List<string>();
            List<string> FinishingStates = new List<string>();

            Stopwatch watch = Stopwatch.StartNew();
            while (!sr.EndOfStream)
            {
                string[] unprocessedState = sr.ReadLine().Split(" = (");
                string startingState = unprocessedState[0];
                string nextLeftState = unprocessedState[1].Split(", ")[0];
                string nextRightState = unprocessedState[1].Split(", ")[1].Remove(3);
                LeftMap.Add(startingState, nextLeftState);
                RightMap.Add(startingState, nextRightState);

                if (startingState.EndsWith('A')) StartingStates.Add(startingState);
                else if (startingState.EndsWith('Z')) FinishingStates.Add(startingState);
            }
            sr.Close();
            string currentState = "AAA";
            const string finishState = "ZZZ";
            int counter = 0;
            bool reachedGoal = false;
            while (!reachedGoal)
            {
                foreach (char direction in lrInstructions)
                {
                    if (currentState == finishState)
                    {
                        reachedGoal = true;
                        break;
                    }
                    if (direction == 'L')
                    {
                        LeftMap.TryGetValue(currentState, out currentState);
                    }
                    else if (direction == 'R')
                    {
                        RightMap.TryGetValue(currentState, out currentState);
                    }
                    counter++;
                }
            }
            watch.Stop();
            Console.WriteLine($"Part 1: {counter} in {watch.ElapsedMilliseconds} ms");
            watch.Restart();

            bool currentStateFinished = false;
            counter = 1;
            List<StateLoops> loops = new List<StateLoops>();
            foreach (string state in StartingStates)
            {
                StateLoops s1 = new StateLoops(state);
                loops.Add(s1);
            }
            watch = Stopwatch.StartNew();
            foreach (StateLoops state in loops)
            {
                currentStateFinished = false;
                counter = 1;
                state.CurrentState = state.StartingState;
                while (!currentStateFinished)
                {
                    foreach (char direction in lrInstructions)
                    {
                        if (direction == 'L')
                        {
                            state.CurrentState = LeftMap.GetValueOrDefault(state.CurrentState);
                        }
                        else if (direction == 'R')
                        {
                            state.CurrentState = RightMap.GetValueOrDefault(state.CurrentState);
                        }
                        if (FinishingStates.Contains(state.CurrentState) && state.FinishingState == String.Empty)
                        {
                            state.FinishingState = state.CurrentState;
                            state.LoopCounter = counter;
                            currentStateFinished = true;
                            break;
                        }


                        counter++;
                    }
                }
            }
            long lcm = 0;

            foreach (StateLoops state in loops)
            {
                if (lcm == 0)
                {
                    lcm = state.LoopCounter;
                }
                else
                {
                    long num1 = lcm;
                    long num2 = state.LoopCounter;
                    lcm = LeastCommonMultiple(num1, num2);
                }
            }
            watch.Stop();

            Console.WriteLine($"Part 2: {lcm} in {watch.ElapsedMilliseconds} ms");



        }
        public long BiggestCommonDenominator(long num1, long num2)
        {
            long largerNum = Math.Max(num1, num2);
            long smallerNum = Math.Min(num1, num2);

            while (smallerNum != 0)
            {
                long remainder = largerNum % smallerNum;
                largerNum = smallerNum;
                smallerNum = remainder;
            }

            return largerNum;
        }

        public long LeastCommonMultiple(long num1, long num2)
        {
            return (num1 / BiggestCommonDenominator(num1, num2)) * num2;
        }
    }
}

