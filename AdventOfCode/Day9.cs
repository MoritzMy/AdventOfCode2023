using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2023
{
    class Row
    {
        List<int> rowInput;
        List<Row> DerivativeRow; 
        public Row(List<int> rowInput)
        {
            this.rowInput = rowInput;
            this.DerivativeRow = new List<Row>();
        }
        public List<int> GetRowInput { get { return this.rowInput; } }
        public List<Row> GetDerivativeRow { get { return this.DerivativeRow; } }
        public void DeriveRow(List<int> row)
        {
            
            List<int> diffrenceRow = new List<int>();
            for(int i = 0; i < row.Count - 1; i++)
            {
                diffrenceRow.Add(row[i + 1] - row[i]);
            }
            Row derivativeRow = new Row(diffrenceRow);
            this.DerivativeRow.Add(derivativeRow);
        }
        static public bool CheckIfZeroRow(List<int> list)
        {
            bool isZeroRow = true;
            foreach (int i in list )
            {
                if (i != 0)
                {
                    isZeroRow = false;
                }
            }
            return isZeroRow;
        }

        public override string ToString()
        {
            string spaceCounter = " ";
            string output = "";
            foreach (int row in this.rowInput)
            {
                output += row.ToString() + " ";
            }

            if (this.DerivativeRow.Count > 0)
            {
                foreach (Row row in this.DerivativeRow)
                {
                    output += "\n" + spaceCounter + row.ToString() + " ";
                    spaceCounter += " ";
                }

            }
            return output;
        }

    }
    internal class Day9
    {
        public void Day9Solution()
        {
            StreamReader sr = new StreamReader("Day9Puzzle.txt");
            string[] input = sr.ReadToEnd().Split("\n");
            List<Row> rows = new List<Row>();
            foreach(string line in input)
            {
                List<int> rowNumbers = new List<int>(); 
                string[] lineNumbers = line.Split(' ');
                foreach (string num in lineNumbers)
                {
                    rowNumbers.Add(int.Parse(num));
                }
                Row row = new Row(rowNumbers);
                row.DeriveRow(row.GetRowInput);
                rows.Add(row);
            }
            FindLowestRows(rows);
            int RowExtensionSum = 0;
            foreach (Row row in rows)
            {
                RowExtensionSum += GetNextPartOfSequence(row);
            }
            Console.WriteLine(RowExtensionSum);
            RowExtensionSum = 0;
            foreach (Row row in rows)
            {
                RowExtensionSum += GetPreviousPartOfSequence(row);
            }
            Console.WriteLine(RowExtensionSum);
        }
        public void FindLowestRows(List<Row> rows)
        {
            bool foundLowestRow = false;
            foreach (Row row in rows)
            {
                foundLowestRow = false;
                while (!foundLowestRow)
                {
                    row.DeriveRow(row.GetDerivativeRow[row.GetDerivativeRow.Count - 1].GetRowInput);
                    if (Row.CheckIfZeroRow(row.GetDerivativeRow[row.GetDerivativeRow.Count - 1].GetRowInput))
                    {
                        foundLowestRow = true;
                    }
                }
            }
        }
        // Build extend Array Funktion
        public int GetNextPartOfSequence(Row row)
        {
            row.GetDerivativeRow[row.GetDerivativeRow.Count - 1].GetRowInput.Add(0);
            int lastNumAdded = 0;
            for (int i = row.GetDerivativeRow.Count - 2; i >= 0; i--)
            {                  
                    row.GetDerivativeRow[i].GetRowInput.Add(row.GetDerivativeRow[i + 1].GetRowInput[row.GetDerivativeRow[i + 1].GetRowInput.Count - 1] + row.GetDerivativeRow[i].GetRowInput[row.GetDerivativeRow[i].GetRowInput.Count - 1]);   
            }
            lastNumAdded = row.GetRowInput[row.GetRowInput.Count - 1] + row.GetDerivativeRow[0].GetRowInput[row.GetDerivativeRow[0].GetRowInput.Count - 1];
            row.GetRowInput.Add(lastNumAdded);
            return lastNumAdded;
        }
        public int GetPreviousPartOfSequence(Row row)
        {
            row.GetDerivativeRow[row.GetDerivativeRow.Count - 1].GetRowInput.Insert(0, 0);
            int lastNumAdded = 0;
            for (int i = row.GetDerivativeRow.Count - 2; i >= 0; i--)
            {
                row.GetDerivativeRow[i].GetRowInput.Insert(0, row.GetDerivativeRow[i].GetRowInput[0] - row.GetDerivativeRow[i + 1].GetRowInput[0]);
            }
            lastNumAdded = row.GetRowInput[0] - row.GetDerivativeRow[0].GetRowInput[0];
            return lastNumAdded;
        }

        public void PrintRows(List<Row> rows)
        {
            foreach (Row row in rows)
            {
                Console.WriteLine(row);
            }
        }
        
    }
}
