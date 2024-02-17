
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
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
            string[] input = sr.ReadToEnd().Split('\n');
            char[,] maze = new char[input.Length, input[0].Length];
            for(int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    maze[i, j] = input[i][j];
                }
            }            
            Console.BufferWidth = 400;
            
            maze = DoubleTheField(maze);
            (int y, int x) startingCoordinates = FindStartingPosition(maze);
            FindStartingDirections(startingCoordinates, maze, out Direction firstDirection, out Direction secondDirection);
            LoopExplorer Loopin = new LoopExplorer(firstDirection, startingCoordinates);
            LoopExplorer Loopus = new LoopExplorer(secondDirection, startingCoordinates);
            maze[Loopin.GetCoordinates.y, Loopin.GetCoordinates.x] = '3';
            LoopExplorer[] Loopers = new LoopExplorer[] { Loopin, Loopus };
            Loopin.Move(Loopin.GetSetDirection);
            Loopus.Move(Loopus.GetSetDirection);

            while (!((Loopin.GetCoordinates.y == Loopus.GetCoordinates.y) && (Loopin.GetCoordinates.x == Loopus.GetCoordinates.x)))
            {
                foreach (LoopExplorer Looper in Loopers)
                {
                    SetNewDirection(Looper, maze);
                    maze[Looper.GetCoordinates.y, Looper.GetCoordinates.x] = '3';

                    Looper.Move(Looper.GetSetDirection);

                }
            }
            maze[Loopin.GetCoordinates.y, Loopin.GetCoordinates.x] = '3';
            maze = ReplaceEdgeChars(maze);
            for (int i = 0; i < 200; i++)
            {
                ReplaceAllNonEnclosedChars(maze);
            }
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    Console.Write(maze[i, j]);
                }
                Console.Write('\n');
            }

            Console.WriteLine(Loopin.GetCounter / 2);
            int counter = 0;
            for(int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {

                    if (maze[i, j] == 'L' || maze[i, j] == '-' || maze[i, j] == 'J' || maze[i, j] == 'F' || maze[i, j] == '7' || maze[i, j] == '|' || maze[i, j] == '.')
                    {
                        counter++;
                    }
                }
            }
            Console.WriteLine(counter);
        }
        public char[,] DoubleTheField(char[,] maze)
        {
            List<List<char>> maze_list = new List<List<char>>();
            List<char> mazeRows = new List<char>();
            List<char> extraRow = new List<char>();
            for(int i = 0; i < maze.GetLength(0); i++)
            {
                for(int j = 0; j < maze.GetLength(1); j++)
                {
                    mazeRows.Add(maze[i, j]);
                    mazeRows.Add('*');
                }
                maze_list.Add(mazeRows);
                foreach(char c in mazeRows)
                {
                    extraRow.Add('*');
                }
                maze_list.Add(extraRow);
                mazeRows = new List<char>();
            }
            char[,] doubledMaze = new char[maze.GetLength(0) * 2, maze.GetLength(1) * 2];
            {
                for (int i = 0; i < doubledMaze.GetLength(0); i++)
                {
                    for (int j = 0; j < doubledMaze.GetLength(1); j++)
                    {
                        doubledMaze[i, j] = maze_list[i][j];
                    }
                }
            }
            return doubledMaze;
        }
        public char[,] ReplaceAllNonEnclosedChars(char[,] maze)
        {
            for(int i = 0; i < maze.GetLength(0);i++)
            {
                for(int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] != '2' && maze[i, j] != '3')
                    {
                        if ((i - 1 >= 0 && maze[i - 1, j] == '2') ||
                            (i + 1 < maze.GetLength(0) && maze[i + 1, j] == '2') ||
                            (j - 1 >= 0 && maze[i, j - 1] == '2') ||
                            (j + 1 < maze.GetLength(1) && maze[i, j + 1] == '2'))
                        {
                            maze[i, j] = '2';
                        }
                    }
                }
            }
            return maze;
        }
        public char[,] ReplaceEdgeChars(char[,] input)
        {
            for(int i = 0; i < input.GetLength(1); i++)
            {
                if (input[0 , i] != '3')
                {
                    input[0, i] = '2';
                }
                if (input[input.GetLength(0) - 1 ,i] != '3')
                {
                    input[input.GetLength(0) - 1, i] = '2';

                }
            }
            for (int i = 0; i < input.GetLength(0); i++)
            {
                if (input[i, 0] != '3')
                {
                    input[i, 0] = '2';

                }
                if (input[i, input.GetLength(1) - 1] != '3')
                {
                    input[i, input.GetLength(1) - 1] = '2';

                }
            }
            return input;
        }
        public (int y, int x) FindStartingPosition(char[,] maze)
        {
            (int y, int x) startingPositionCoordinates = (0, 0);
            for(int i = 0; i < maze.GetLength(0); i++)
            {
                for(int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] == 'S')
                    {
                        return startingPositionCoordinates = (i, j);
                    }
                }
            }
            return startingPositionCoordinates;
        }
        public void FindStartingDirections((int y, int x) startingCoordinates, char[,] maze, out Direction firstDirection, out Direction secondDirection)
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
        public bool CanMoveinThisDirection(char[,] maze, (int y, int x) coordinates, Direction direction)
        {
            List<char> canMoveNorthTo = new List<char>() { '7', '|', 'F'};
            List<char> canMoveEastTo = new List<char>() { '7', '-', 'J' };
            List<char> canMoveSouthTo = new List<char>() { 'J', '|', 'L'};
            List<char> canMoveWestTo = new List<char>() { 'F', '-', 'L' };
            switch (direction)
            {
                case Direction.north:
                    if (coordinates.y - 2 < 0) return false;
                    if (canMoveNorthTo.Contains((maze[coordinates.y - 2, coordinates.x]))) return true;
                    return false;
                case Direction.east:
                    if (coordinates.x + 2 >= maze.GetLength(1)) return false;
                    if (canMoveEastTo.Contains((maze[coordinates.y, coordinates.x + 2]))) return true;
                    return false;
                case Direction.south:
                    if (coordinates.y + 2 >= maze.GetLength(1)) return false;
                    if (canMoveSouthTo.Contains((maze[coordinates.y + 2, coordinates.x]))) return true;
                    return false;
                case Direction.west:
                    if (coordinates.x - 2 < 0) return false;
                    if (canMoveWestTo.Contains((maze[coordinates.y, coordinates.x - 2]))) return true;
                    return false;
            }
            return false;
        }
        public void SetNewDirection(LoopExplorer Looper, char[,] maze)
        {
            Direction currentDirection = Looper.GetSetDirection;

            switch (maze[Looper.GetCoordinates.y, Looper.GetCoordinates.x])
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
                    if (currentDirection == Direction.east) Looper.GetSetDirection = Direction.north;
                    else if (currentDirection == Direction.south) Looper.GetSetDirection = Direction.west;
                    break;
                case 'L':
                    if (currentDirection == Direction.west) Looper.GetSetDirection = Direction.north;
                    else if (currentDirection == Direction.south) Looper.GetSetDirection = Direction.east;
                    break;
            }
        }

    }
}
