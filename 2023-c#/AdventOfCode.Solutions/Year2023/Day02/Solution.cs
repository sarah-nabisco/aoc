using System.Security;
using System.Xml.XPath;

namespace AdventOfCode.Solutions.Year2023.Day02;

class Solution : SolutionBase
{
    public Solution() : base(02, 2023, "") { }

    private int parseLine(string line, string game, string startdelim, string pullDelim, string colorDelim, Dictionary<string, int> colors)
    {
        Boolean validGame = true;

        // remove any casing issues
        line = line.ToLower();

        // debug
        //Console.WriteLine(line);

        // remove game prefix
        line = line.Replace(game, "").TrimStart();

        // split game number from pulls
        string[] currGame = line.Split(startdelim);

        // get game number
        string number = currGame[0];
        int num = Int32.Parse(number);

        // debug
        //Console.WriteLine(num);

        // get remainder of game stats
        line = currGame[1].TrimStart();

        // debug
        //Console.WriteLine(line);

        // split the string by pull delimeter
        string[] pulls = line.Split(pullDelim);

        foreach (string pull in pulls)
        {
            //Console.WriteLine(pull.TrimStart());

            // split out each color
            string[] color = pull.Split(colorDelim);

            foreach (string test in color)
            {

                string[] count = test.TrimStart().Split(" ");

                string recordedColor = count[1];
                if (colors.ContainsKey(recordedColor))
                {
                    int recordedCount = Int32.Parse(count[0]);
                    //Console.WriteLine(recordedColor + " - " + recordedCount + " - " + colors[recordedColor]);
                    if (colors[recordedColor] < recordedCount)
                    {
                        validGame = false;
                    }
                }
                else
                {
                    validGame = false;
                    //probably an error unknown color
                }
            }
        }
        return validGame ? num : 0;
    }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        int total = 0;
        Dictionary<string, int> colors = new Dictionary<string, int>() { { "red", 12 }, { "green", 13 }, { "blue", 14 } };

        foreach (string line in lines)
        {
            int result = parseLine(line, "game", ":", ";", ",", colors);
            //Console.WriteLine("Result -> " + result);
            total += result;
            // debug
            //Console.WriteLine("--------------------");
        }
        return total.ToString();
    }


    private int parseLine2(string line, string game, string startdelim, char[] delimChars)
    {

        // remove any casing issues
        line = line.ToLower();

        // debug
        Console.WriteLine(line);

        // remove game prefix
        line = line.Replace(game, "").TrimStart();

        // split game number from pulls
        string[] currGame = line.Split(startdelim);

        // get remainder of game stats
        line = currGame[1].TrimStart();

        // debug
        //Console.WriteLine(line);

        // split the string by pull delimeter

        string[] pulls = line.Split(delimChars);
        Dictionary<string, int> colors = new Dictionary<string, int>() { { "red", 0 }, { "green", 0 }, { "blue", 0 } };

        foreach (string pull in pulls)
        {
            //Console.WriteLine( pull.TrimStart());

            string[] count = pull.TrimStart().Split(" ");

            string recordedColor = count[1];
            if (colors.ContainsKey(recordedColor))
            {
                // replace as needed
                int recordedCount = Int32.Parse(count[0]);

                if (colors[recordedColor] < recordedCount)
                {
                    colors[recordedColor] = recordedCount;
                }
            }
            else
            {
                //probably an error unknown color
            }

        }

        Boolean firstValue = true;
        int result = -1;
        foreach (KeyValuePair<string, int> entry in colors)
        {
            Console.WriteLine(entry.Key + " - " + entry.Value);

            if (firstValue)
            {
                firstValue = false;
                result = entry.Value;
            }
            else
            {
                result *= entry.Value;
            }

        }
        return result;
    }

    protected override string SolvePartTwo()
    {
        var lines = Input.SplitByNewline();
        char[] delimChars = { ';', ',' };

        int total = 0;

        foreach (string line in lines)
        {
            int result = parseLine2(line, "game", ":", delimChars);
            Console.WriteLine("Result -> " + result);
            total += result;
            // debug
            Console.WriteLine("--------------------");
        }
        return total.ToString();
    }
}
