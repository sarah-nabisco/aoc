namespace AdventOfCode.Solutions.Year2023.Day10;

class Solution : SolutionBase
{
    public Solution() : base(10, 2023, "") { }

    protected override string SolvePartOne()
    {

        // find start
        var lines = Input.SplitByNewline();
        List<char[]> matrix = [];

        /*
        | is a vertical pipe connecting north and south.
        - is a horizontal pipe connecting east and west.
        L is a 90-degree bend connecting north and east.
        J is a 90-degree bend connecting north and west.
        7 is a 90-degree bend connecting south and west.
        F is a 90-degree bend connecting south and east.
        . is ground; there is no pipe in this tile.
        S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
        */

        int currRow = -1;
        int currCol = -1;

        foreach (string line in lines)
        {
            // index everything
            matrix.Add(line.ToCharArray());

            // find start
            if (line.Contains('S'))
            {
                currRow = matrix.Count() - 1;
                currCol = line.IndexOf('S');
            }
        }

        // first step, move down
        currRow += 1;

        char currPipe = matrix[currRow][currCol];
        int length = 0;
        char flow = 'S';

        while (!currPipe.Equals('S'))
        {
            flow = Move(currPipe, flow);
            length++;

            switch (flow)
            {
                case 'S':
                    currRow += 1;
                    break;
                case 'N':
                    currRow -= 1;
                    break;
                case 'E':
                    currCol += 1;
                    break;
                case 'W':
                    currCol -= 1;
                    break;
            }

            currPipe = matrix[currRow][currCol];
        }

        length += length % 2;

        length /= 2;
        // when back at start halve the length

        return length.ToString();
    }


    private char Move(char turn, char direction)
    {
        /*
        | is a vertical pipe connecting north and south.
        - is a horizontal pipe connecting east and west.
        L is a 90-degree bend connecting north and east.
        J is a 90-degree bend connecting north and west.
        7 is a 90-degree bend connecting south and west.
        F is a 90-degree bend connecting south and east.
        . is ground; there is no pipe in this tile.
        S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
        */
        char flow = ' ';



        switch (turn)
        {
            case '|':
                flow = direction;
                break;
            case '-':
                flow = direction;

                break;
            case 'L':
                if (direction.Equals('S'))
                {
                    flow = 'E';
                }
                if (direction.Equals('W'))
                {
                    flow = 'N';
                }

                break;
            case 'J':
                if (direction.Equals('S'))
                {
                    flow = 'W';
                }
                if (direction.Equals('E'))
                {
                    flow = 'N';
                }

                break;
            case '7':
                if (direction.Equals('N'))
                {
                    flow = 'W';
                }
                if (direction.Equals('E'))
                {
                    flow = 'S';
                }

                break;
            case 'F':
                if (direction.Equals('N'))
                {
                    flow = 'E';
                }
                if (direction.Equals('W'))
                {
                    flow = 'S';
                }
                break;
        }

        Console.WriteLine(turn + " from " + direction + " going " + flow);

        return flow;
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
