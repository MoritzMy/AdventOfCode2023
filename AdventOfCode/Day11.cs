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
        bool expandedVertically;
        bool expandedHorizontally;
        (int y, int x) coordiantes;
        public Galaxies()
        {
            this.state = State.Empty;
            this.count = 0;
            this.expandedVertically = false;
            this.expandedHorizontally = false;
        }
        public Galaxies(State state)
        {
            this.state = state;
            this.count = counter++;
            this.coordiantes = coordiantes;
        }
        public State GetSetState { get { return this.state; } set { this.state = value; } }
        public int GetCount { get { return this.count; } }
        public (int y, int x) GetSetCoordinates { get { return this.coordiantes; } set { this.coordiantes = value; } }
        public bool GetSetVerticalExpansion {  get { return this.expandedVertically; } set { this.expandedVertically = value; } }
        public bool GetSetHorizontalExpansion { get { return this.expandedHorizontally; } set { this.expandedHorizontally = value; } }
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
            SetCoordinatesInUniverse(universe);
            
            int expansionAmount = 2;
            Console.WriteLine(CalculateSumOfMinimalDistances(universe, expansionAmount));
            expansionAmount = 1000000;
            Console.WriteLine(CalculateSumOfMinimalDistances(universe, expansionAmount));

        }
        public long CalculateSumOfMinimalDistances(List<List<Galaxies>> universe, int expansionAmount)
        {
            long sum = 0;
            List<Galaxies> allGalaxies = new List<Galaxies> ();
            foreach (List<Galaxies> universeRow in universe)
            {
                foreach (Galaxies Galaxy in universeRow)
                {
                    if (Galaxy.GetCount != 0)
                    {
                        allGalaxies.Add(Galaxy);
                    }
                }
            }
            for (int i = 0; i < allGalaxies.Count; i++)
            {
                for (int j = i + 1;  j < allGalaxies.Count; j++)
                {
                    int verticalExpansionCounter = 0;
                    int horizontalExpansionCounter = 0;
                    Galaxies mostLeftGalaxie = allGalaxies[i];
                    Galaxies mostRightGalaxie = allGalaxies[j];
                    if (allGalaxies[j].GetSetCoordinates.x < allGalaxies[i].GetSetCoordinates.x)
                    {
                        mostLeftGalaxie = allGalaxies[j];
                        mostRightGalaxie = allGalaxies[i];
                    }
                    for(int y = allGalaxies[i].GetSetCoordinates.y; y < allGalaxies[j].GetSetCoordinates.y; y++)
                    {
                        if (universe[y][0].GetSetHorizontalExpansion == true) { horizontalExpansionCounter++; }
                    }
                    for (int x = mostLeftGalaxie.GetSetCoordinates.x; x < mostRightGalaxie.GetSetCoordinates.x; x++)
                    {
                        if (universe[0][x].GetSetVerticalExpansion == true) {  verticalExpansionCounter++; }
                    }

                    sum += Math.Abs(allGalaxies[j].GetSetCoordinates.x - allGalaxies[i].GetSetCoordinates.x) + (expansionAmount * verticalExpansionCounter - verticalExpansionCounter) + Math.Abs(allGalaxies[j].GetSetCoordinates.y - allGalaxies[i].GetSetCoordinates.y) + (expansionAmount * horizontalExpansionCounter - horizontalExpansionCounter);
                }
            }
            return sum;
        }
        public void SetCoordinatesInUniverse(List<List<Galaxies>> universe)
        {
            for (int i = 0; i <  universe.Count; i++)
            {
                for (int j = 0; j < universe[0].Count; j++)
                {
                    if (universe[i][j].GetSetState == State.Galaxy)
                    {
                        universe[i][j].GetSetCoordinates = (i, j);
                    }
                }
            }
        }
        public void ExpandSpace(List<List<Galaxies>> universe)
        {
            for (int i = 0; i < universe.Count; i++)
            {
                bool emptyRow = true;
                for (int j = 0; j < universe[0].Count; j++)
                {
                    if (universe[i][j].GetSetState == State.Galaxy)
                    {
                        emptyRow = false;
                    }
                }
                if (emptyRow)
                {
                    foreach (Galaxies g in universe[i])
                    {
                        g.GetSetHorizontalExpansion = true;
                    }
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
                    for (int j = 0; j < universe.Count; j++)
                    {
                        universe[j][i].GetSetVerticalExpansion = true;
                    }
                }
            }
        }
        //public void ExpandSpace(List<List<Galaxies>> universe)
        //{
        //    for(int i = 0; i < universe.Count; i++)
        //    {
        //        bool emptyRow = true;
        //        for(int j = 0; j < universe[0].Count; j++)
        //        {
        //            if (universe[i][j].GetSetState == State.Galaxy)
        //            {
        //                emptyRow = false;
        //            }
        //        }
        //        if (emptyRow)
        //        {
        //            List<Galaxies> newRow = new List<Galaxies>();
        //            foreach(Galaxies galaxy in universe[i])
        //            {
        //                newRow.Add(galaxy);
        //            }
        //            universe.Insert(i, newRow);
        //            i++;
        //        }
        //    }
        //    for (int i = 0; i < universe[0].Count; i++)
        //    {
        //        bool emptyColumn = true;
        //        for (int j = 0; j < universe.Count; j++)
        //        {
        //            if (universe[j][i].GetSetState == State.Galaxy)
        //            {
        //                emptyColumn = false;
        //            }
        //        }
        //        if (emptyColumn)
        //        {
        //            for( int j = 0;j < universe.Count; j++)
        //            {
        //                universe[j].Insert(i, universe[j][i]);

        //            }
        //            i++;
        //        }
        //    }
        //}
    }
}
