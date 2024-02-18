using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2023
{
    enum State {
        Empty,
        Galaxy
    }
    class Galaxies
    {
        static int counter = 1;
        State state;
        int count;
        public Galaxies()
        {
            this.state = State.Empty;
            this.count = 0;
        }
        public Galaxies(State state)
        {
            this.state = state;
            this.count = counter++;
        }
        public State GetSetState { get { return this.state; } set { this.state = value; } }
        public int GetCount { get { return this.count; } }
    }
    internal class Day11
    {
        
        public void Day11Solutions()
        {
            StreamReader sr = new StreamReader("Day11Puzzle.txt");
            string[] input = sr.ReadToEnd().Split("\n");
            List<List<Galaxies>> universe = new List<List<Galaxies>>();
            for (int i = 0; i < input.Length; i++)
            {
                List<Galaxies> currentRow = new List<Galaxies>();
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '#')
                    {
                        Galaxies galaxy = new Galaxies(State.Galaxy);
                        currentRow.Add(galaxy);
                    }
                    else if (input[i][j] == '.')
                    {
                        Galaxies galaxy = new Galaxies();
                        currentRow.Add(galaxy);
                    }
                }
                universe.Add(currentRow);
            }
            ExpandSpace(universe);
            foreach(List<Galaxies> space in universe)
            {
                foreach(Galaxies g in  space)
                {
                    if (g.GetSetState == State.Galaxy)
                    {
                        Console.Write('#');
                    }
                    else if (g.GetSetState == State.Empty)
                    {
                        Console.Write('.');
                    }                  
                }
                Console.WriteLine();
            }

        }
        public void ExpandSpace(List<List<Galaxies>> universe)
        {
            for(int i = 0; i < universe.Count; i++)
            {
                bool emptyRow = true;
                for(int j = 0; j < universe[0].Count; j++)
                {
                    if (universe[i][j].GetSetState == State.Galaxy)
                    {
                        emptyRow = false;
                    }
                }
                if (emptyRow)
                {
                    List<Galaxies> newRow = new List<Galaxies>();
                    foreach(Galaxies galaxy in universe[i])
                    {
                        newRow.Add(galaxy);
                    }
                    universe.Insert(i, newRow);
                    i++;
                }
            }
            for (int i = 0; i < universe[0].Count; i++)
            {
                bool emptyColumn = true;
                for (int j = 0; j < universe.Count; j++)
                {
                    if (universe[j][i].GetSetState == State.Galaxy)
                    {
                        emptyColumn = false;
                    }
                }
                if (emptyColumn)
                {
                    for( int j = 0;j < universe.Count; j++)
                    {
                        universe[j].Insert(i, universe[j][i]);

                    }
                    i++;
                }
            }
        }
    }
}
