using System.Runtime.Serialization;

namespace AdventOfCode.Solutions.Year2023.Day06;

class Solution : SolutionBase
{
    public Solution() : base(06, 2023, "") { }

    protected override string SolvePartOne()
    {

        var lines = Input.SplitByNewline();
        long[] time = [];
        long[] distance = [];
        long result = -1;

        foreach (string line in lines)
        {
            if (line.StartsWith("Time"))
            {
                time = loadValues(line);

            }
            else if (line.StartsWith("Distance"))
            {
                distance = loadValues(line);

            }

        }

        for (int index = 0; index < time.Length; index++)
        {
            long possibleWins = parseGamePt2(time[index], distance[index]);

            //Console.WriteLine("Game: " + (index + 1) + " - " + possibleWins);
            if (possibleWins > 0)
            {
                if (result > 0)
                {
                    result *= possibleWins;
                }
                else
                {
                    result = possibleWins;
                }
            }
        }

        return result.ToString();
    }

    private long parseGame(long time, long distance)
    {
        long winningGames = 0;
        for (long buttonHold = 1; buttonHold < distance; buttonHold++)
        {
            long currDistance = buttonHold * (time - buttonHold);
            if (currDistance > distance)
            {
                winningGames++;
            }
        }
        return winningGames;
    }

    private long[] loadValues(string line)
    {

        line = line.Split(":")[1].TrimStart();

        while (line.Contains("  "))
        {
            line = line.Replace("  ", " ");
        }

        string[] vals = line.Split(" ");
        long[] result = new long[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {

            result[i] = Int32.Parse(vals[i].Trim());
        }
        return result;
    }


    private long loadValuesPt2(string line)
    {
        line = line.Split(":")[1].TrimStart();

        line = line.Replace(" ", "");

        return Int64.Parse(line.Trim());
    }
    private long parseGamePt2(long time, long distance)
    {

        Console.WriteLine("Distance: " + distance + " Time: " + time);

        long start = (long)((time - (long)Math.Sqrt((time * time) - (long)4.0 * (distance))) / (long)2.0);
        long end = (long)((time + (long)Math.Sqrt((time * time) - (long)4.0 * (distance))) / (long)2.0);

        //Console.WriteLine("start: " + start);
        //Console.WriteLine("end: " + end);
        //Console.WriteLine((end - (start)));

        return end - start;

    }


    protected override string SolvePartTwo()
    {
        // TODO didn't finish this - plugged in to quadratic equation calculator
        // answer - 30077773
        var lines = Input.SplitByNewline();
        long time = 0;
        long distance = 0;
        long result = -1;

        foreach (string line in lines)
        {
            if (line.StartsWith("Time"))
            {
                time = loadValuesPt2(line);
            }
            else if (line.StartsWith("Distance"))
            {
                distance = loadValuesPt2(line);
            }
        }

        result = parseGamePt2(time, distance);

        return result.ToString();
    }
}
