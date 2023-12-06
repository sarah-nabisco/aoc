using System.ComponentModel.Design;
using System.Configuration.Assemblies;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace AdventOfCode.Solutions.Year2023.Day05;

class Solution : SolutionBase
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Solution() : base(05, 2023, "") { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    long[] seedList;// pt1
    long[] resultList;//pt1
    List<long[]> seedRanges = [];//pt2
    List<long[]> resultRanges = [];//pt2

    string[] stages = [
            "seed-to-soil map:",
            "soil-to-fertilizer map:",
            "fertilizer-to-water map:",
            "water-to-light map:",
            "light-to-temperature map:",
            "temperature-to-humidity map:",
            "humidity-to-location map:"
        ];

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        int currStage = -1;

        foreach (string line in lines)
        {
            //Console.WriteLine("STAGE: " + currStage);
            //Console.WriteLine(line);

            // first stage - seeds
            if (line.StartsWith("seeds:"))
            {
                // create seed array
                seedList = ingestNumbers(line.Replace("seeds: ", "").Trim());
                resultList = new long[seedList.Length];

                for (int i = 0; i < resultList.Length; i++)
                {
                    resultList[i] = -1;
                }

                //Console.WriteLine("[{0}]", string.Join(", ", seedList));
                continue;
            }

            if (currStage == -1 || (currStage != (stages.Length - 1) && line.Trim().Equals(stages[currStage + 1])))
            {
                // whitespace close category
                // TODO process values from previous stage
                //Console.WriteLine("new category");
                currStage++;
                resetLists();


                continue;
            }

            //Console.WriteLine("PROCESSING - " + line);
            // TODO read values into stage
            updateResults(line);



        }
        resetLists();

        long minVal = seedList[0];
        foreach (long val in seedList)
        {
            if (minVal > val)
            {
                minVal = val;
            }
        }

        // TODO return the smallest value in result list

        return minVal.ToString();
    }

    private long[] ingestNumbers(string line)
    {
        string[] strings = line.Split(" ");
        long[] seeds = new long[strings.Length];

        for (int i = 0; i < strings.Length; i++)
        {
            seeds[i] = Int64.Parse(strings[i]);
        }

        return seeds;
    }

    private void updateResults(string line)
    {
        long[] ingest = ingestNumbers(line);
        //0 - dest start
        //1 - source start
        //2 - range

        for (int seedIndex = 0; seedIndex < seedList.Length; seedIndex++)
        {
            if (seedList[seedIndex] >= ingest[1] && seedList[seedIndex] <= (ingest[1] + ingest[2]))
            {
                long newVal = (ingest[0] - ingest[1]) + seedList[seedIndex];

                //Console.WriteLine("Setting " + seedList[seedIndex] + " to " + newVal);
                resultList[seedIndex] = newVal;

            }
            // do nothing if not found.

        }
    }

    private void resetLists()
    {
        //Console.WriteLine("[{0}]", string.Join(", ", resultList));
        for (int seedIndex = 0; seedIndex < seedList.Length; seedIndex++)
        {
            if (resultList[seedIndex] != -1)
            {
                seedList[seedIndex] = resultList[seedIndex];
            }
            resultList[seedIndex] = -1;
        }

    }


    private void SetSeedRanges(string line)
    {
        string[] strings = line.Split(" ");
        long[] seeds = new long[strings.Length];

        for (int i = 0; i < strings.Length; i += 2)
        {
            long start = Int64.Parse(strings[i]);
            long range = Int64.Parse(strings[i + 1]);
            seedRanges.Add([start, (start + range)]);
        }

    }

    private void resetRanges()
    {

        Console.WriteLine("Reset Ranges");

        // look for any ranges in seed range not included in result Range
        seedRanges.AddRange(resultRanges);
        resultRanges = new List<long[]>();

    }

    private String getMin()
    {
        long min = -1;
        foreach (long[] seedRange in seedRanges)
        {
            if (min == -1 || min > seedRange[0])
            {
                min = seedRange[0];
            }
        }
        //TODO this is off by one, not sure why
        return (min - 1).ToString();
    }


    private void processLine(string line)
    {
        long[] ingest = ingestNumbers(line);
        //0 - dest start
        //1 - source start
        //2 - range

        // go through each range
        long sourceStart = ingest[1];
        long sourceEnd = ingest[1] + ingest[2];
        long destStart = ingest[0];
        long destEnd = ingest[0] + ingest[2];
        long offset = ingest[0] - ingest[1];
        List<long[]> newSeedRange = [];

        String status = "";
        Boolean changed = false;


        status += "------------------------------\n";
        //status += "Processing Line: " + line + "\n";
        status += "SourceRange: " + sourceStart + " - " + sourceEnd + "\n";
        status += "Offest " + offset + "\n";
        //status += "DestRange: " + destStart + " - " + destEnd + "\n";

        foreach (long[] seedRange in seedRanges)
        {


            long seedStart = seedRange[0];
            long seedEnd = seedRange[1];

            //Console.WriteLine("\nSeedRange: " + seedStart + " - " + seedEnd);

            // source range outside of seed range
            if (seedStart > sourceEnd || seedEnd < sourceStart)
            {
                //Console.WriteLine("\tSource Range outside of seed range");
                // do nothing
                newSeedRange.Add(new long[] { seedStart, seedEnd });


            } // seed wraps source
            else if (seedStart <= sourceStart && sourceEnd <= seedEnd)
            {
                changed = true;
                status += "\nSeedRange: " + seedStart + " - " + seedEnd + "\n";
                status += "\tSeed Wraps source, splitting off ends" + "\n";
                // split off the ends
                newSeedRange.Add(new long[] { seedStart, (sourceStart - 1) });
                newSeedRange.Add(new long[] { (sourceEnd + 1), seedEnd });

                // add the new result
                resultRanges.Add(new long[] { (sourceStart + offset), (sourceEnd + offset) });

            }// source wraps seed
            else if (sourceStart <= seedStart && seedEnd <= sourceEnd)
            {
                changed = true;
                status += "\nSeedRange: " + seedStart + " - " + seedEnd + "\n";
                status += "\tSource wraps seed" + "\n";
                // add the new result
                resultRanges.Add(new long[] { (seedStart + offset), (seedEnd + offset) });

            }// source hangs before seed
            else if (sourceStart <= seedStart && sourceEnd <= seedEnd)
            {
                changed = true;
                status += "\nSeedRange: " + seedStart + " - " + seedEnd + "\n";
                status += "\tSource before seed range" + "\n";
                // trim off unused range
                newSeedRange.Add(new long[] { (sourceEnd + 1), seedEnd });

                // add to result
                resultRanges.Add(new long[] { (seedStart + offset), (sourceEnd + offset) });

            }// seed hangs before source
            else if (seedStart <= sourceStart && seedEnd <= sourceEnd)
            {
                changed = true;
                status += "\nSeedRange: " + seedStart + " - " + seedEnd + "\n";
                status += "\tSource after seed" + "\n";

                // trim off unused range
                newSeedRange.Add(new long[] { seedStart, (sourceStart - 1) });

                // add the new result
                resultRanges.Add(new long[] { (sourceStart + offset), (seedEnd + offset) });
            }
            else
            {
                Console.WriteLine("NOOOOOOOOOO");
            }


        }
        //Console.WriteLine("Processed");
        seedRanges = newSeedRange;

        List<long[]> printSeedRange = [];
        printSeedRange.AddRange(seedRanges);
        printSeedRange.AddRange(resultRanges);


        changed = true;
        if (changed)
        {
            Console.WriteLine(status);
            Console.WriteLine("\nResult - ");
            foreach (long[] seedRange in seedRanges)
            {
                Console.WriteLine("\t\t" + seedRange[0] + " - " + seedRange[1]);
            }

            foreach (long[] seedRange in resultRanges)
            {
                Console.WriteLine("\t\t" + seedRange[0] + " - " + seedRange[1] + " *");
            }
            Console.WriteLine("----------------------------------\n");
        }



    }




    protected override string SolvePartTwo()
    {

        //Console.WriteLine("");
        var lines = Input.SplitByNewline();
        int currStage = -1;

        foreach (string line in lines)
        {
            Console.WriteLine("STAGE: " + currStage);
            Console.WriteLine("LINE: " + line);

            // first stage - seeds
            if (line.StartsWith("seeds:"))
            {

                SetSeedRanges(line.Replace("seeds: ", "").Trim());
                continue;
            }

            if (currStage == -1 || (currStage != (stages.Length - 1) && line.Trim().Equals(stages[currStage + 1])))
            {
                currStage++;
                resetRanges();


                continue;
            }

            processLine(line);

        }
        resetRanges();

        return getMin();

    }
    //6472061 too high

    // new sample output should be 59370572




}
