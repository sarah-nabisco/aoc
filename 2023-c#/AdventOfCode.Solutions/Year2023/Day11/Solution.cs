using System.Reflection.Emit;
using System.Runtime.ExceptionServices;

namespace AdventOfCode.Solutions.Year2023.Day11;

class Solution : SolutionBase
{
    public Solution() : base(11, 2023, "") { }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();

        // list of empty columns
        // list of empty rows
        List<long> emptyRows = new List<long>();
        List<long> emptyCols = new List<long>();
        List<Galaxy> universe = new List<Galaxy>();
        long rowIndex = 0;


        foreach (string line in lines)
        {
            if (!line.Contains("#"))
            {
                emptyRows.Add(rowIndex);
            }
            long colIndex = 0;
            if (emptyCols.Count() == 0)
            {
                foreach (char letter in line)
                {
                    emptyCols.Add(colIndex);
                    colIndex++;
                }
            }

            colIndex = 0;
            foreach (char letter in line)
            {
                if (!letter.Equals('.'))
                {
                    universe.Add(new Galaxy(rowIndex, colIndex));
                    emptyCols.Remove(colIndex);
                }

                colIndex++;
            }

            rowIndex++;
        }


        // expand the universe
        foreach (Galaxy galaxy in universe)
        {
            galaxy.Expand(emptyRows, emptyCols, 1);
        }

        // calculate paths
        long length = 0;
        for (int index = 0; index < universe.Count; index++)
        {
            for (int compare = (index + 1); compare < universe.Count(); compare++)
            {
                length += universe[index].Navigate(universe[compare]);
            }
        }

        return length.ToString();
    }

    protected override string SolvePartTwo()
    {

        var lines = Input.SplitByNewline();

        // list of empty columns
        // list of empty rows
        List<long> emptyRows = new List<long>();
        List<long> emptyCols = new List<long>();
        List<Galaxy> universe = new List<Galaxy>();
        long rowIndex = 0;


        foreach (string line in lines)
        {
            if (!line.Contains("#"))
            {
                emptyRows.Add(rowIndex);
            }


            // load emptycols

            long colIndex = 0;


            if (emptyCols.Count() == 0)
            {
                foreach (char letter in line)
                {
                    emptyCols.Add(colIndex);
                    colIndex++;
                }
            }

            colIndex = 0;
            foreach (char letter in line)
            {
                if (!letter.Equals('.'))
                {
                    universe.Add(new Galaxy(rowIndex, colIndex));
                    emptyCols.Remove(colIndex);
                }

                colIndex++;
            }

            rowIndex++;
        }


        /*
        // print empty
        Console.Write("Empty rows: ");

        foreach (int emptyRow in emptyRows)
        {
            Console.Write(emptyRow + ", ");

        }

        Console.WriteLine();
        Console.Write("Empty cols: ");

        foreach (int emptyCol in emptyCols)
        {
            Console.Write(emptyCol + ", ");
        }
        Console.WriteLine();
        */

        // expand the universe
        foreach (Galaxy galaxy in universe)
        {
            //Console.Write("Moving [" + galaxy.X + ", " + galaxy.Y + "] -> [");
            galaxy.Expand(emptyRows, emptyCols, (1000000 - 1));
            //Console.WriteLine(galaxy.X + ", " + galaxy.Y + "]");
        }

        // calculate paths
        long length = 0;
        for (int index = 0; index < universe.Count; index++)
        {
            for (int compare = index + 1; compare < universe.Count; compare++)
            {
                length += universe[index].Navigate(universe[compare]);
            }
        }

        return length.ToString();
    }

    //827010736819 <- too high

    private class Galaxy(long x, long y)
    {
        public long X = x;
        public long Y = y;

        public void Expand(List<long> rows, List<long> cols, long expanse)
        {
            long rowOffset = 0;
            long colOffset = 0;
            foreach (long row in rows)
            {
                if (row < X)
                {
                    rowOffset++;
                }
            }
            foreach (long col in cols)
            {
                if (col < Y)
                {
                    colOffset++;
                }
            }

            rowOffset *= expanse;
            colOffset *= expanse;

            X += rowOffset;
            Y += colOffset;

        }

        public long Navigate(Galaxy galaxy)
        {
            long distance = Math.Abs((X - galaxy.X)) + Math.Abs((Y - galaxy.Y));
            //Console.WriteLine("[" + X + ", " + Y + "] -> [" + galaxy.X + ", " + galaxy.Y + "] = " + distance);
            return distance;
        }

    }
}
