using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode.Solutions.Year2023.Day13;

class Solution : SolutionBase
{
    public Solution() : base(13, 2023, "") { }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        Pattern currPattern = new Pattern();
        long total = 0;

        //Console.WriteLine(lines.Length);
        foreach (string line in lines)
        {
            //Console.WriteLine(line);
            if (line.Trim().Equals("---"))
            {
                // process pattern
                //total += currPattern.Process();

                // reset patttern
                currPattern = new Pattern();
            }
            else
            {
                //Console.WriteLine("Adding");
                currPattern.Add(line);
            }

        }

        return total.ToString();
    }

    protected override string SolvePartTwo()
    {

        var lines = Input.SplitByNewline();
        Pattern currPattern = new Pattern();
        long total = 0;

        //Console.WriteLine(lines.Length);
        foreach (string line in lines)
        {
            //Console.WriteLine(line);
            if (line.Trim().Equals("---"))
            {
                // process pattern
                total += currPattern.ProcessFix();

                // reset patttern
                currPattern = new Pattern();
            }
            else
            {
                //Console.WriteLine("Adding");
                currPattern.Add(line);
            }

        }

        //56744 too high
        //37344 too high
        //37343 too high

        return total.ToString();
    }

    public class Pattern
    {
        List<string> pattern;
        List<string> rotatePattern;


        public Pattern()
        {
            pattern = [];
            rotatePattern = [];
        }

        public void Add(string row)
        {
            pattern.Add(row);
        }

        public long Process()
        {
            long score = 0;
            RotatePattern();
            Console.WriteLine("Row Candidates");
            List<int> rowCandidates = GetCandidates(pattern);
            rowCandidates = EvaluateCandidates(pattern, rowCandidates, false);

            foreach (int rowVal in rowCandidates)
            {
                score = (rowVal * 100);
            }

            Console.WriteLine("\nCol Candidates");
            List<int> colCandidates = GetCandidates(rotatePattern);
            colCandidates = EvaluateCandidates(rotatePattern, colCandidates, false);
            foreach (int colVal in colCandidates)
            {
                score = colVal;
            }
            Console.WriteLine("---------------------");
            return score;
        }

        public long ProcessFix()
        {

            Console.WriteLine("\n------Start Process-------------");
            long score = 0;


            Console.WriteLine("Checking Row Candidates");
            List<int> rowCandidates = FixCandidates(true);
            if (rowCandidates.Count == 0)
            {
                Console.WriteLine("Initial check not found, evaluating other candidates");
                rowCandidates = GetCandidates(pattern);
                Console.WriteLine("Possible options: " + rowCandidates.Count);

                rowCandidates = EvaluateCandidates(pattern, rowCandidates, true);
            }

            foreach (int rowVal in rowCandidates)
            {
                Console.WriteLine("Row Candidates to score " + rowVal);

                score = (rowVal * 100);
            }

            if (rowCandidates.Count > 1)
            {
                PrintPattern(pattern);
                throw new Exception("TOO MANY ROW CANDIDATES");
            }

            if (rowCandidates.Count == 0)
            {
                Console.WriteLine("No rows, checking columns \n\nCol Candidates");
                RotatePattern();


                List<int> colCandidates = FixCandidates(false);
                if (colCandidates.Count == 0)
                {
                    Console.WriteLine("Initial check not found, evaluating other candidates\n");
                    colCandidates = GetCandidates(rotatePattern);
                    Console.WriteLine("Possible options: " + colCandidates.Count);

                    colCandidates = EvaluateCandidates(rotatePattern, colCandidates, true);
                }

                foreach (int colVal in colCandidates)
                {
                    Console.WriteLine("Col Candidates to score " + colVal);
                    score = colVal;
                }
                if (colCandidates.Count == 0)
                {
                    PrintPattern(pattern);
                    throw new Exception(" HOLD UP NOTHING FOUND");
                }
                else if (colCandidates.Count > 1)
                {
                    PrintPattern(pattern);
                    throw new Exception("TOO MANY COL CANDIDATES");
                }


            }

            Console.WriteLine("-------------------------------------------------------------\n");
            return score;
        }

        private List<int> FixCandidates(bool isRow)
        {
            List<string> checkPattern = isRow ? pattern : rotatePattern;
            List<int> candidates = [];

            string compare = "";
            int index = 0;

            foreach (string row in checkPattern)
            {
                string newRow = CleanSmudge(row, compare);

                if (!newRow.Equals(row))
                {
                    Console.WriteLine("Smudge Found: " + (isRow ? "Row" : "Col") + " -> " + index + "  [" + row + "] [" + newRow + "]");
                    candidates.Add(index);
                }
                compare = row;
                index++;
            }

            if (candidates.Count > 0)
            {
                candidates = EvaluateCandidates(checkPattern, candidates, false);
            }
            return candidates;
        }

        private string CleanSmudge(string original, string compare)
        {
            int diffs = 0;

            //Console.WriteLine("Compare [" + original + "] [" + compare + "]");

            for (int index = 0; index < compare.Length; index++)
            {
                if (!original[index].Equals(compare[index]))
                {
                    diffs++;
                }
            }

            //Console.WriteLine("diffs: " + diffs);

            if (diffs == 1)
            {
                return compare;
            }

            return original;

        }

        private List<int> GetCandidates(List<string> checkPattern)
        {
            List<int> candidates = [];

            string compare = "";
            int index = 0;

            //PrintPattern(checkPattern);

            foreach (string row in checkPattern)
            {
                if (compare.Equals(row))
                {
                    candidates.Add(index);
                    Console.WriteLine("Candidate: " + index);
                }
                compare = row;
                index++;
            }
            return candidates;
        }

        private void RotatePattern()
        {
            List<string> newPattern = [];
            char[,] newMatrix = new char[pattern[0].Length, pattern.Count];

            // swap x & y
            for (int x = 0; x < pattern.Count; x++)
            {
                for (int y = 0; y < pattern[0].Length; y++)
                {
                    newMatrix[y, x] = pattern[x][y];
                }
            }

            // convert back to list
            for (int x = 0; x < pattern[0].Length; x++)
            {
                string row = "";
                for (int y = 0; y < pattern.Count; y++)
                {
                    row += newMatrix[x, y];
                }
                newPattern.Add(row);
            }

            rotatePattern = newPattern;
        }
        public void PrintPattern(List<string> print)
        {
            foreach (string row in print)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine();

        }

        private List<int> EvaluateCandidates(List<string> checkPattern, List<int> candidates, bool smudge)
        {
            List<int> confirmed = [];

            foreach (int candidate in candidates)
            {
                Console.WriteLine("Evaluating " + candidate);
                // ignore reflection point because we know it matches
                int indexA = candidate + 1;
                int indexB = candidate - 2;

                bool reflection = true;
                bool smudgeFound = false;

                while (indexA < checkPattern.Count && indexB < checkPattern.Count && indexB >= 0 && indexA >= 0 && reflection)
                {
                    if (!checkPattern[indexA].Equals(checkPattern[indexB]))
                    {
                        Console.WriteLine("No match: [" + checkPattern[indexA] + "] [" + checkPattern[indexB] + "] " + smudge + "|" + smudgeFound + "|" + CleanSmudge(checkPattern[indexA], checkPattern[indexB]));
                        if (smudge && !smudgeFound && CleanSmudge(checkPattern[indexA], checkPattern[indexB]).Equals(checkPattern[indexB]))
                        {
                            Console.WriteLine("Fixed Smudge at " + indexA);
                            smudgeFound = true;

                        }
                        else
                        {
                            reflection = false;
                        }

                    }
                    indexB--;
                    indexA++;
                }

                if (reflection && ((!smudge) || (smudge && smudgeFound)))
                {
                    confirmed.Add(candidate);
                    //Console.WriteLine(candidate + " - Confirmed!----------");
                }
            }

            return confirmed;
        }


    }

}
