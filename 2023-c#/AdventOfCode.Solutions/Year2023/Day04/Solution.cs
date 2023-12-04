namespace AdventOfCode.Solutions.Year2023.Day04;

class Solution : SolutionBase
{
    public Solution() : base(04, 2023, "") { }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        int total = 0;

        foreach (string line in lines)
        {
            // debug
            //Console.WriteLine(line);

            string cleanInput = line.Split(":")[1].Replace("  ", " ").Trim();
            total += score(parseLine(cleanInput));
        }

        return total.ToString();
    }


    private int parseLine(string line)
    {
        //Console.WriteLine("[" + line + "]");
        int[] winning = cleanInputs(line.Split("|")[0].Trim());
        int[] input = cleanInputs(line.Split("|")[1].Trim());
        int numHits = 0;

        numHits = input.Count(winning.Contains);
        //Console.WriteLine("numHits: " + numHits);
        return numHits;
    }

    private int[] cleanInputs(String line)
    {
        String[] stringVals = line.Split(" ");
        int[] input = new int[stringVals.Length];

        for (int index = 0; index < stringVals.Length; index++)
        {
            input[index] = Int32.Parse(stringVals[index].Trim());
        }

        //Console.WriteLine("[{0}]", string.Join(", ", input));
        return input;

    }

    private int score(int numHits)
    {
        int score = 0;

        if (numHits >= 1)
        {
            score = 1;
        }

        numHits--;

        while (numHits > 0)
        {
            score *= 2;
            numHits--;
        }

        return score;
    }

    private int recurse(int index, int[] inputs)
    {
        int score = 1;

        //Console.WriteLine("Card: " + (index + 1) + " - hits: " + inputs[index]);
        //Console.WriteLine("[{0}]", string.Join(", ", inputs));

        for (int i = 1; i <= inputs[index]; i++)
        {
            //Console.WriteLine("From Card: " + (index + 1) + " checking card " + (index + 1 + i));
            int val = recurse((index + i), inputs);
            score += val;
            //Console.WriteLine("From Card: " + (index + 1) + " checking card " + (index + 1 + i) + " score: " + val + " total: " + score);
        }

        //Console.WriteLine("Card: " + (index + 1) + " Returning Score: " + score);
        return score;

    }
    /*

     1  2  3  4  5  6
    [4, 2, 2, 1, 0, 0] - 30

    1 = 1 + [2, 3, 4, 5]    - 1 - 1
    2 = 1 + [3, 4]          - 11 - 2
    3 = 1 + [4, 5]          - 1111 - 4
    4 = 1 + [5]             - 11111111 - 8
    5 = 1                   - 11111111111111 - 14
    6 = 1                   - 1 - 1


    1 = 1 + [2, 3, 4, 5]    - 15
             7  4  2  1
    2 = 1 + [3, 4]          - 7
             4  2
    3 = 1 + [4, 5]          - 4
             2  1
    4 = 1 + [5]             - 2
             1
    5 = 1                   - 1

    6 = 1                   - 1


    */

    protected override string SolvePartTwo()
    {

        var lines = Input.SplitByNewline();
        int total = 0;
        int[] inputs = new int[lines.Length];

        // generate the array
        for (int index = 0; index < lines.Length; index++)
        {
            ;
            string cleanInput = lines[index].Split(":")[1].Replace("  ", " ").Trim();
            inputs[index] = parseLine(cleanInput);
        }

        // score the values
        for (int index = 0; index < lines.Length; index++)
        {

            //Console.WriteLine("--------------- Checking " + (index + 1) + " - " + inputs[index] + " hits. --------------------");
            int score = recurse(index, inputs);
            //Console.WriteLine();

            total += score;
        }

        //Console.WriteLine("[{0}]", string.Join(", ", inputs));

        return total.ToString();
    }
}
