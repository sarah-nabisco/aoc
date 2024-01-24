namespace AdventOfCode.Solutions.Year2023.Day15;

class Solution : SolutionBase
{
    public Solution() : base(15, 2023, "") { }

    protected override string SolvePartOne()
    {
        List<string> inputs = [.. Input.SplitByNewline()[0].Split(",")];
        long results = 0;

        foreach (string input in inputs)
        {
            git results += ParseHash(input);
        }

        return results.ToString();
    }

    private int ParseHash(string input)
    {
        int result = 0;

        foreach (char curr in input)
        {
            //Increase the current value by the ASCII code for the current character of the string
            result += ((int)curr);
            //Set the current value to itself multiplied by 17.
            result *= 17;
            //Set the current value to the remainder of dividing itself by 256.
            result %= 256;
        }


        return result;
    }

    protected override string SolvePartTwo()
    {

        var lines = Input.SplitByNewline();

        foreach (string line in lines)
        {

        }

        return "";
    }
}
