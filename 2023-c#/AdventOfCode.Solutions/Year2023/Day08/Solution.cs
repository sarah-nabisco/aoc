using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace AdventOfCode.Solutions.Year2023.Day08;

class Solution : SolutionBase
{
    public Solution() : base(08, 2023, "") { }

    protected override string SolvePartOne()
    {
        ElfMap map = new ElfMap();
        var lines = Input.SplitByNewline();
        string currStep = "AAA";
        string lastStep = "ZZZ";

        foreach (string line in lines)
        {
            if (line.Contains("("))
            {
                //Console.WriteLine(line);
                string node = line.Split("=")[0].Trim();
                string next = line.Split("=")[1].Trim().Replace("(", "").Replace(")", "").Replace(",", "");
                string left = next.Split(" ")[0].Trim();
                string right = next.Split(" ")[1].Trim();

                map.AddNode(node, left, right);
                //Console.WriteLine(node + " = (" + left + ", " + right + ")");

            }
            else
            {
                //Console.WriteLine("Directions - " + line);
                map.SetDirections(line.Trim());
            }
        }

        // this is the part that processes ------------------------------------

        //Console.WriteLine("--------------------------------------------------------------------------------------");
        //Console.WriteLine (new string(map.Directions));

        Boolean complete = false;
        int steps = 0;
        //Console.WriteLine("Starting at " + currStep);

        while (!complete)
        {
            foreach (char turn in map.Directions)
            {
                if (!complete)
                {
                    currStep = map.Step(turn, currStep);
                    //Console.WriteLine("Turn " + turn + " to " + currStep);

                    steps++;
                    //Console.WriteLine(steps + "-------------");

                    complete = (currStep.Equals(lastStep));
                }

            }
        }


        return steps.ToString();
    }

    protected override string SolvePartTwo()
    {

        ElfMap map = new ElfMap();
        var lines = Input.SplitByNewline();
        List<string> currStep = new List<string>();

        foreach (string line in lines)
        {
            if (line.Contains("("))
            {
                string node = line.Split("=")[0].Trim();
                string next = line.Split("=")[1].Trim().Replace("(", "").Replace(")", "").Replace(",", "");
                string left = next.Split(" ")[0].Trim();
                string right = next.Split(" ")[1].Trim();

                map.AddNode(node, left, right);

                if (node.Substring(2, 1).Equals("A"))
                {
                    currStep.Add(node);
                }

            }
            else
            {
                map.SetDirections(line.Trim());
            }
        }

        return CalculateSteps(currStep, map).ToString();
    }

    public long CalculateSteps(List<string> startVals, ElfMap map)
    {

        Console.WriteLine("Checking for loops");
        Loops loops = new Loops(startVals.Count);

        for (int i = 0; i < startVals.Count; i++)
        {
            int[] currLoop = CalculateLoop(startVals[i], map);
            loops.starts[i] = currLoop[0];
            loops.loopSteps[i] = currLoop[1];
        }

        loops.Start();
        Boolean found = false;
        long iteration = 100000000;
        long count = 0;
        long passes = 0;

        while (!found)
        {
            found = loops.Step();
            if (count % iteration == 0)
            {
                count = 0;
                Console.WriteLine(passes + " - 100M passes: " + " - " + loops.currStep.Max());
                passes++;
                if (loops.currStep.Max() > 20131447079522)
                {
                    Console.WriteLine("WRONG");
                    found = true;
                }
            }
            count++;
        }

        return 0;
    }
    // too high 38585277158834
    //          30197172967610
    //          20131447079522
    //
    // -----------------------
    // low       3355237900838



    private int[] CalculateLoop(string start, ElfMap map)
    {

        Boolean complete = false;
        int index = -1;
        int steps = -1;
        int loopStart = -1;
        int loopSteps = -1;

        //Console.WriteLine("Inside loop check - " + start);
        //Console.WriteLine("------------------");

        string currStep = start;
        Boolean loopFound = false;

        while (!complete)
        {
            foreach (char turn in map.Directions)
            {
                if (!complete)
                {
                    complete = currStep.Substring(2, 1).Equals("Z");

                    currStep = map.Step(turn, currStep);
                    //Console.WriteLine(currStep);
                    steps++;
                    index++;

                    // populate loop start
                    if (loopStart == -1)
                    {
                        loopStart = index;
                    }

                    else if (complete && !loopFound)
                    {
                        loopFound = true;
                        loopSteps = steps;
                        steps = -1;
                        //Console.WriteLine("Found a end at " + loopStart + " with " + loopSteps + " steps");
                        complete = false;

                    }
                    else if (complete && loopFound)
                    {
                        if (!(steps == loopSteps))
                        {
                            //Console.WriteLine("Loop Thrash old steps - " + loopSteps + " new - " + steps);
                            loopStart = index;
                            complete = false;
                            loopSteps = steps;
                            steps = -1;

                        }
                        else
                        {
                            //Console.WriteLine("SUCCESS");
                        }
                    }
                }

            }
        }

        //Console.WriteLine("------------------\n");
        return [loopStart, loopSteps];
    }


    public class ElfMap
    {
        public char[] Directions;
        public Dictionary<string, string[]> Nodes;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ElfMap()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

            Nodes = new Dictionary<string, string[]>();
        }

        public void SetDirections(string directions)
        {
            Directions = directions.ToCharArray();
        }
        public void AddNode(string node, string left, string right)
        {
            Nodes.Add(node, [left, right]);
        }

        public string Step(char turn, String node)
        {
            //Console.WriteLine(node + " = (" + Nodes[node][0] + ", " + Nodes[node][1] + ")");

            if (turn == 'L')
            {
                return Nodes[node][0];

            }
            else if (turn == 'R')
            {
                return Nodes[node][1];

            }
            return "";

        }

    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Loops(int size)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        public long[] starts = new long[size];
        public long[] loopSteps = new long[size];
        public long[] currStep = new long[size];

        public void Start()
        {
            currStep = starts;
        }

        public Boolean Step()
        {


            for (int i = 0; i < starts.Length; i++)
            {
                while (currStep[i] < currStep[(i + 1) % starts.Length])
                {
                    currStep[i] += loopSteps[i];
                }
            }

            for (int i = 1; i < starts.Length; i++)
            {
                if (currStep[i] != currStep[i - 1])
                {
                    return false;
                }

            }


            return true;

        }

    }

}
