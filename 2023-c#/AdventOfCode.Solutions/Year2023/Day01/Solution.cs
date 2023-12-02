using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using Microsoft.VisualBasic;

namespace AdventOfCode.Solutions.Year2023.Day01;

class Solution : SolutionBase
{
    public Solution() : base(01, 2023, "") { }

    private Dictionary<string, int> stringToInt = new Dictionary<string, int>() { { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 } };

    private int findDigit(string input, Boolean first)
    {
        // could also just step through the string and ignore non-digits

        // peel out just the numeric characters
        string result = string.Concat(input.Where(Char.IsDigit));

        // if there are numeric characters
        if (result != null && result.Length >= 1)
        {
            if (first)
                return Int32.Parse(result[..1]);
            else
                return Int32.Parse(new string(result[result.Length - 1], 1));
        }
        else

            return -1;
    }

    private int findIndex(string input, string number, Boolean min)
    {
        int minIndex = -1;
        int maxIndex = -1;

        for (int i = input.IndexOf(number); i > -1; i = input.IndexOf(number, i + 1))
        {
            if (minIndex == -1)
            {
                minIndex = i;
                maxIndex = i;
            }
            else
            {
                maxIndex = i;
            }
        }

        if (min)
        {
            return minIndex;
        }
        else
        {
            return maxIndex;
        }

    }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        long total = 0;

        foreach (string line in lines)
        {
            var firstDigit = findDigit(line, true);
            var lastDigit = findDigit(line, false);

            var firstDigitIndex = firstDigit == -1 ? -1 : findIndex(line, firstDigit.ToString(), true);
            var lastDigitIndex = lastDigit == -1 ? -1 : findIndex(line, lastDigit.ToString(), false);

            // numbers
            var firstNumber = "";
            var lastNumber = "";
            var firstNumberIndex = -1;
            var lastNumberIndex = -1;


            // compare
            var lineVal = 0;

            if (firstDigitIndex != -1 && (firstDigitIndex < firstNumberIndex || lastNumberIndex == -1))
            {
                lineVal += (firstDigit * 10);
            }
            else
            {
                lineVal += (stringToInt[firstNumber] * 10);
            }

            if (lastDigitIndex != -1 && (lastDigitIndex > lastNumberIndex || lastNumberIndex == -1))
            {
                lineVal += lastDigit;
            }
            else
            {
                lineVal += stringToInt[lastNumber];
            }


            //debug
            //Console.WriteLine("Input: " + line);
            //Console.WriteLine("First Digit - " + firstDigit);
            //Console.WriteLine("First Digit Index - " + firstDigitIndex);
            //Console.WriteLine("Last Digit - " + lastDigit);
            //Console.WriteLine("Last Digit Index - " + lastDigitIndex);
            //Console.WriteLine("First Number - " + firstNumber);
            //Console.WriteLine("First Number Index - " + firstNumberIndex);
            //Console.WriteLine("Last Number - " + lastNumber);
            //Console.WriteLine("Last Number Index - " + lastNumberIndex);
            //Console.WriteLine("VALUE -> " + lineVal);
            //Console.WriteLine("-------------");

            total += lineVal;

        }

        return total.ToString();

    }

    protected override string SolvePartTwo()
    {
        var lines = Input.SplitByNewline();
        long total = 0;

        foreach (string line in lines)
        {
            var firstDigit = findDigit(line, true);
            var lastDigit = findDigit(line, false);
            var firstDigitIndex = firstDigit == -1 ? -1 : findIndex(line, firstDigit.ToString(), true);
            var lastDigitIndex = lastDigit == -1 ? -1 : findIndex(line, lastDigit.ToString(), false);


            // numbers
            var firstNumber = "";
            var lastNumber = "";
            var firstNumberIndex = -1;
            var lastNumberIndex = -1;

            foreach (KeyValuePair<string, int> entry in stringToInt)
            {
                var number = entry.Key;
                var currMinIndex = findIndex(line, number, true);
                var currMaxIndex = findIndex(line, number, false);

                if (currMinIndex != -1 && (firstNumberIndex == -1 || currMinIndex < firstNumberIndex))
                {
                    firstNumber = number;
                    firstNumberIndex = currMinIndex;
                }
                if (currMaxIndex != -1 && (lastNumberIndex == -1 || currMaxIndex > lastNumberIndex))
                {
                    lastNumber = number;
                    lastNumberIndex = currMaxIndex;
                }
            }


            // compare
            var lineVal = 0;

            if (firstDigitIndex != -1 && (firstDigitIndex < firstNumberIndex || lastNumberIndex == -1))
            {
                lineVal += (firstDigit * 10);
            }
            else
            {
                lineVal += (stringToInt[firstNumber] * 10);
            }


            if (lastDigitIndex != -1 && (lastDigitIndex > lastNumberIndex || lastNumberIndex == -1))
            {
                lineVal += lastDigit;
            }
            else
            {
                lineVal += stringToInt[lastNumber];
            }


            //debug
            //Console.WriteLine("Input: " + line);
            //Console.WriteLine("First Digit - " + firstDigit);
            //Console.WriteLine("First Digit Index - " + firstDigitIndex);
            //Console.WriteLine("Last Digit - " + lastDigit);
            //Console.WriteLine("Last Digit Index - " + lastDigitIndex);
            //Console.WriteLine("First Number - " + firstNumber);
            //Console.WriteLine("First Number Index - " + firstNumberIndex);
            //Console.WriteLine("Last Number - " + lastNumber);
            //Console.WriteLine("Last Number Index - " + lastNumberIndex);
            //Console.WriteLine("VALUE -> " + lineVal);
            //Console.WriteLine("-------------");

            total += lineVal;

        }

        return total.ToString();
    }
}
