using System.Runtime.Serialization;

namespace AdventOfCode.Solutions.Year2023.Day09;

class Solution : SolutionBase
{
    public Solution() : base(09, 2023, "") { }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();

        long result = 0;

        foreach (string line in lines)
        {
            Oasis oasis = new Oasis();
            //Console.WriteLine("Processing: " + line);
            oasis.StartingRow(line);
            long step = oasis.Extrapolate();
            result += step;
            //Console.WriteLine("Return " + step);
            //Console.WriteLine("-------------------------------");
        }


        return result.ToString();
    }

    protected override string SolvePartTwo()
    {

        var lines = Input.SplitByNewline();

        long result = 0;

        foreach (string line in lines)
        {
            Oasis oasis = new Oasis();
            //Console.WriteLine("Processing: " + line);
            oasis.StartingRow(line);
            long step = oasis.ExtrapolateBackwards();
            result += step;
            //Console.WriteLine("Return " + step);
            //Console.WriteLine("-------------------------------");
        }


        return result.ToString();
    }

    public class Oasis()
    {
        List<List<int>> History = new();


        public void StartingRow(string line)
        {
            List<int> vals = new List<int>();
            foreach (string val in line.Split(" "))
            {
                vals.Add(Int32.Parse(val));
            }
            History.Add(vals);
            //Console.WriteLine("Read in starting row");
        }

        public long Extrapolate()
        {
            LoadRows();

            long currTotal = 0;

            for (int i = History.Count - 2; i >= 0; i--)
            {
                //Console.WriteLine("Adding " + History[i].Last());
                currTotal += History[i].Last();
            }


            return currTotal;
        }
        public long ExtrapolateBackwards()
        {
            LoadRows();

            long currTotal = 0;

            for (int i = History.Count - 2; i >= 0; i--)
            {
                //Console.WriteLine("Adding " + History[i].Last());
                currTotal = History[i][0] - currTotal;
            }


            return currTotal;
        }


        private void LoadRows()
        {
            while (!FoundEnd())
            {
                List<int> currRow = History.Last();

                //Console.WriteLine("Looking for end " + String.Join("; ", currRow));

                List<int> newRow = [];

                for (int i = 0; i < (currRow.Count - 1); i++)
                {
                    newRow.Add(currRow[i + 1] - currRow[i]);
                }
                History.Add(newRow);

            }
        }

        public Boolean FoundEnd()
        {
            foreach (int val in History.Last())
            {
                if (val != 0)
                {
                    return false;
                }
            }

            return true; ;
        }


    }


}
