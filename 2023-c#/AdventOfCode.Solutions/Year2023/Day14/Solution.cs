using System.ComponentModel;
using System.Net.NetworkInformation;

namespace AdventOfCode.Solutions.Year2023.Day14;

class Solution : SolutionBase
{
    public Solution() : base(14, 2023, "") { }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        Platform platform = new Platform();

        foreach (string line in lines)
        {
            Console.WriteLine("Reading [" + line + ']');
            platform.Add(line);
        }
        platform.PrintPlatform();

        return platform.CalculateLoad().ToString();
    }

    protected override string SolvePartTwo()
    {

        var lines = Input.SplitByNewline();

        foreach (string line in lines)
        {

        }

        return "";
    }

    public class Platform
    {
        private List<char[]> platform;
        public Platform()
        {
            platform = [];

        }

        public void Add(string row)
        {
            row = row.Replace('.', ' ');
            if (platform.Count == 0)
            {
                platform.Add(row.ToCharArray());
            }
            else
            {

                char[] currRow = new char[row.Length];
                for (int i = 0; i < currRow.Length; i++)
                {
                    currRow[i] = ' ';
                }


                List<int> ball = [];

                for (int index = 0; index < row.Length; index++)
                {
                    char curr = row[index];
                    if (curr.Equals('O'))
                    {
                        ball.Add(index);
                    }
                    else if (curr.Equals('#'))
                    {
                        currRow[index] = '#';
                    }
                }
                platform.Add(currRow);
                AddRound(ball);
            }



        }

        private void AddRound(List<int> balls)
        {
            foreach (int ball in balls)
            {
                bool dropped = false;

                // starting at the top
                int rowIndex = platform.Count - 1;

                while (!dropped)
                {
                    char[] curr = platform[rowIndex];

                    if (rowIndex == 0)
                    {
                        curr[ball] = 'O';
                        dropped = true;
                        platform[rowIndex] = curr;
                    }
                    else
                    {
                        char nextVal = platform[rowIndex - 1][ball];

                        if (nextVal.Equals('#') || nextVal.Equals('O'))
                        {
                            curr[ball] = 'O';
                            dropped = true;
                            platform[rowIndex] = curr;
                        }
                    }
                    rowIndex--;

                }
            }

        }
        public void PrintPlatform()
        {
            foreach (char[] row in platform)
            {
                Console.WriteLine(new string(row));
            }
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------\n\n");
        }

        public long CalculateLoad()
        {
            long total = 0;

            for (int index = 0; index < platform.Count; index++)
            {
                foreach (char val in platform[index])
                {

                    if (val.Equals('O'))
                    {
                        total += (long)(platform.Count - index);
                    }
                }

            }

            return total;

        }
    }


}
