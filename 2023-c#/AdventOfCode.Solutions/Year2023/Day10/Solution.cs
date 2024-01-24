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

        //Console.WriteLine(turn + " from " + direction + " going " + flow);

        return flow;
    }

    protected override string SolvePartTwo()
    {

        // find start
        var lines = Input.SplitByNewline();
        List<char[]> matrix = [];
        List<Point> route = [];

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
        route.Add(new Point(currRow, currCol));
        Console.WriteLine(currRow + ", " + currCol);

        // first step, move down
        currRow += 1;

        char currPipe = matrix[currRow][currCol];
        char flow = 'S';

        while (!currPipe.Equals('S'))
        {
            route.Add(new Point(currRow, currCol));
            Console.WriteLine(currRow + ", " + currCol);
            flow = Move(currPipe, flow);

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

        long internalCount = getInternalCount(route);

        // 7142 is too high
        return internalCount.ToString();
    }

    // utility function to find GCD of
    // two numbers a and b
    static int gcd(int a, int b)
    {
        if (b == 0)
            return a;
        return gcd(b, a % b);
    }

    // Finds the no. of Integral points between
    // two given points.
    static int getBoundaryCount(Point p, Point q)
    {
        // Check if line parallel to axes
        if (p.x == q.x)
            return Math.Abs(p.y - q.y) - 1;

        if (p.y == q.y)
            return Math.Abs(p.x - q.x) - 1;

        return gcd(Math.Abs(p.x - q.x),
                Math.Abs(p.y - q.y)) - 1;
    }


    // Returns count of points inside the triangle
    static long getInternalCount(List<Point> route)
    {

        // use the pick's algorithm
        long boundaryCount = 0;
        for (int index = 0; index < route.Count - 1; index++)
        {
            boundaryCount += (getBoundaryCount(route[index], route[index + 1]));
        }
        boundaryCount += getBoundaryCount(route[^1], route[0]);
        boundaryCount += route.Count;



        long sum1 = 0;
        long sum2 = 0;

        // use the pick's algorithm
        for (int index = 0; index < route.Count - 1; index++)
        {
            sum1 += (route[index].x * route[index + 1].y);
            sum2 += (route[index].y * route[index + 1].x);
        }

        sum1 += (route[^1].x * route[0].y);
        sum2 += (route[0].x * route[^1].y);

        long area = Math.Abs(sum1 - sum2);


        // Use Pick's theorem to calculate
        // the no. of Interior points
        return ((long)((area - boundaryCount + 2.0) / 2.0));
    }

    // Class to represent an Integral point
    // on XY plane.
    public class Point
    {
        public int x, y;

        public Point(int a, int b)
        {
            x = a;
            y = b;
        }
    }
}
