using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2023
{
    enum Direction
    {
        north,
        west,
        south,
        east
    }
    class LoopExplorer
    {
        int counter;
        Direction direction;
        (int y, int x) coordinates;
        public LoopExplorer(Direction direction, (int y, int x) coordinates)
        {
            this.direction = direction;
            this.counter = 0;
            this.coordinates = coordinates;
        }
        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.north:
                    this.coordinates.y--;
                    break;
                case Direction.west:
                    this.coordinates.x--; 
                    break;
                case Direction.east:
                    this.coordinates.x++;
                    break;
                case Direction.south:
                    this.coordinates.y++;
                    break;
                default:
                    break;
            }
            this.counter++;
        }
        public int GetCounter { get { return counter; } }
        public Direction GetSetDirection { get { return direction; } set { this.direction = value; } }
        public (int y, int x) GetCoordinates { get { return coordinates; } }

    }
    internal class Day10
    {
        public void Day10Solution()
        {
            StreamReader sr = new StreamReader("Day10Puzzle.txt");
            string[] maze = sr.ReadToEnd().Split('\n');
            
            (int y, int x) startingCoordinates = FindStartingPosition(maze);
            FindStartingDirections(startingCoordinates, maze, out Direction firstDirection, out Direction secondDirection);
            Console.WriteLine(firstDirection + " " + secondDirection);
            LoopExplorer Loopin = new LoopExplorer(firstDirection, startingCoordinates);
            LoopExplorer Loopus = new LoopExplorer(secondDirection, startingCoordinates);
            LoopExplorer[] Loopers = new LoopExplorer[] { Loopin, Loopus };
            Loopin.Move(Loopin.GetSetDirection);
            Loopus.Move(Loopus.GetSetDirection);
            
            while (!(Loopin.GetCoordinates.y == Loopus.GetCoordinates.y) && !(Loopin.GetCoordinates.x == Loopus.GetCoordinates.x))
            {
                foreach (LoopExplorer Looper in Loopers)
                {
                    Console.WriteLine("(" + Looper.GetCoordinates.y + ", " + Looper.GetCoordinates.x + ")");
                    SetNewDirection(Looper, maze);
                    Looper.Move(Looper.GetSetDirection);
                }
            }
            Console.WriteLine(Loopin.GetCounter);
        }

        public (int y, int x) FindStartingPosition(string[] maze)
        {
            (int y, int x) startingPositionCoordinates = (0, 0);
            for(int i = 0; i < maze.Length; i++)
            {
                for(int j = 0; j < maze[i].Length; j++)
                {
                    if (maze[i][j] == 'S')
                    {
                        startingPositionCoordinates = (i, j);
                    }
                }
            }
            return startingPositionCoordinates;
        }
        public void FindStartingDirections((int x, int y) startingCoordinates, string[] maze, out Direction firstDirection, out Direction secondDirection)
        {
            List<Direction> ValidStartingDirections = new List<Direction>();
            for(int i = 0; i < 4; i++)
            {
                Direction currentDirection = (Direction)i;
                if (CanMoveinThisDirection(maze, startingCoordinates, currentDirection))
                {
                    ValidStartingDirections.Add(currentDirection);
                }
            }
            firstDirection = ValidStartingDirections[0];
            secondDirection = ValidStartingDirections[1];
        }
        public bool CanMoveinThisDirection(string[] maze, (int y, int x) coordinates, Direction direction)
        {
            List<char> canMoveNorthTo = new List<char>() { '7', '|', 'F'};
            List<char> canMoveEastTo = new List<char>() { '7', '-', 'J' };
            List<char> canMoveSouthTo = new List<char>() { 'J', '|', 'L'};
            List<char> canMoveWestTo = new List<char>() { 'F', '-', 'L' };
            switch (direction)
            {
                case Direction.north:
                    if (canMoveNorthTo.Contains((maze[coordinates.y - 1][coordinates.x])) && coordinates.y - 1 >= 0) return true;
                    return false;
                case Direction.east:
                    if (canMoveEastTo.Contains((maze[coordinates.y][coordinates.x + 1])) && coordinates.x + 1 < maze[coordinates.y].Length) return true;
                    return false;
                case Direction.south:   
                    if (canMoveSouthTo.Contains((maze[coordinates.y + 1][coordinates.x])) && coordinates.y + 1 < maze.Length) return true;
                    return false;
                case Direction.west:
                    if (canMoveWestTo.Contains((maze[coordinates.y][coordinates.x - 1])) && coordinates.x - 1 >= 0) return true;
                    return false;
            }
            return false;
        }
        public void SetNewDirection(LoopExplorer Looper, string[] maze)
        {
            Direction currentDirection = Looper.GetSetDirection;

            switch (maze[Looper.GetCoordinates.y][Looper.GetCoordinates.x])
            {
                case '7':
                    if (currentDirection == Direction.east) Looper.GetSetDirection = Direction.south;
                    else if (currentDirection == Direction.north) Looper.GetSetDirection = Direction.west;
                    break;
                case 'F':
                    if (currentDirection == Direction.west) Looper.GetSetDirection = Direction.south;
                    else if (currentDirection == Direction.north) Looper.GetSetDirection= Direction.east;
                    break;
                case 'J':
                    if (currentDirection == Direction.west) Looper.GetSetDirection = Direction.north;
                    else if (currentDirection == Direction.south) Looper.GetSetDirection = Direction.west;
                    break;
                case 'L':
                    if (currentDirection == Direction.east) Looper.GetSetDirection = Direction.north;
                    else if (currentDirection == Direction.south) Looper.GetSetDirection = Direction.east;
                    break;
            }
        }

    }
}
