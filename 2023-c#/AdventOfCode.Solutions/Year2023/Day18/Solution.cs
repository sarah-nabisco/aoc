using System.Diagnostics.Tracing;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2023.Day18;

class Solution : SolutionBase
{
    public Solution() : base(18, 2023, "") { }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        Trench trench = new Trench();
        int x = 0;
        int y = 0;

        trench.AddPoint(x, y);

        foreach (string line in lines)
        {
            string[] input = line.Split(" ");
            string direction = input[0];
            int length = int.Parse(input[1]);

            switch (direction)
            {
                case "U":
                    x -= length;
                    break;
                case "D":
                    x += length;
                    break;
                case "L":
                    y -= length;
                    break;
                case "R":
                    y += length;
                    break;
            }
            trench.AddPoint(x, y);


        }
        trench.NormalizePoints();

        return trench.getVolume().ToString();
    }

    protected override string SolvePartTwo()
    {

        var lines = Input.SplitByNewline();

        Trench trench = new Trench();
        long x = 0;
        long y = 0;

        trench.AddPoint(x, y);

        foreach (string line in lines)
        {
            string[] input = line.Split(" ");
            string hex = input[2].Substring(2, 6);

            string direction = hex.Substring(5, 1);
            int length = int.Parse(hex.Substring(0, 5), System.Globalization.NumberStyles.HexNumber);

            switch (direction)
            {
                case "3":
                    x -= length;
                    direction = "U";
                    break;
                case "1":
                    x += length;
                    direction = "D";
                    break;
                case "2":
                    y -= length;
                    direction = "L";
                    break;
                case "0":
                    y += length;
                    direction = "R";
                    break;
            }

            trench.AddPoint(x, y);


            //Console.WriteLine(input[2] + " = " + direction + " " + length);

        }
        //trench.NormalizePoints();

        //952408144115
        //5375968157

        return trench.getVolume().ToString();
        //return "";
    }

    public class Trench
    {
        public List<Point> points;

        public Trench()
        {
            points = new List<Point>();
        }

        public void AddPoint(long x, long y)
        {
            points.Add(new Point(x, y));
        }

        public void NormalizePoints()
        {
            // go through and fix indexes;
            long minX = 0;
            long minY = 0;

            foreach (Point point in points)
            {
                if (point.x < minX)
                {
                    minX = point.x;
                }
                if (point.y < minY)
                {
                    minY = point.y;
                }
            }

            if (minX < 0 || minY < 0)
            {
                List<Point> normalizedPoints = new List<Point>();

                foreach (Point point in points)
                {
                    normalizedPoints.Add(new Point(point.x - minX, point.y - minY));
                }
                points = normalizedPoints;
            }
        }
        public void PrintTrench()
        {
            // get max x & y
            long maxX = 0;
            long maxY = 0;

            foreach (Point point in points)
            {
                if (point.x > maxX)
                {
                    maxX = point.x;
                }
                if (point.y > maxY)
                {
                    maxY = point.y;
                }
            }

            List<char[]> image = new List<char[]>();

            for (int x = 0; x < maxX + 1; x++)
            {
                char[] row = new char[maxY + 1];
                for (int y = 0; y < maxY + 1; y++)
                {
                    row[y] = '.';

                }
                image.Add(row);
            }

            foreach (Point point in points)
            {
                image[(int)point.x][point.y] = '#';
            }


            foreach (char[] row in image)
            {
                foreach (char val in row)
                {
                    Console.Write(val + " ");
                }
                Console.WriteLine();
            }
        }

        // utility function to find GCD of
        // two numbers a and b
        private long gcd(long a, long b)
        {
            if (b == 0)
                return a;
            return gcd(b, a % b);
        }

        // Finds the no. of Integral points between
        // two given points.
        private long getBoundaryCount(Point p, Point q)
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
        public long getVolume()
        {

            // use the pick's algorithm
            long boundaryCount = 0;
            for (int index = 0; index < points.Count - 1; index++)
            {
                boundaryCount += (getBoundaryCount(points[index], points[index + 1]));
            }
            boundaryCount += getBoundaryCount(points[^1], points[0]);
            boundaryCount += points.Count;

            // boundary should be 38
            Console.WriteLine(boundaryCount);

            long sum1 = 0;
            long sum2 = 0;

            // use the pick's algorithm
            for (int index = 0; index < points.Count - 1; index++)
            {
                sum1 += (points[index].x * points[index + 1].y);
                sum2 += (points[index].y * points[index + 1].x);
            }

            sum1 += (points[^1].x * points[0].y);
            sum2 += (points[0].x * points[^1].y);

            long area = Math.Abs(sum1 - sum2);


            // Use Pick's theorem to calculate
            // the no. of Interior points
            return ((long)((area - boundaryCount + 2.0) / 2.0)) + boundaryCount;
        }

        public class Point
        {
            public long x, y;

            public Point(long a, long b)
            {
                x = a;
                y = b;
            }
        }
    }
}
