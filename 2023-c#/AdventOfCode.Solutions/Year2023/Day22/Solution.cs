using System.ComponentModel;

namespace AdventOfCode.Solutions.Year2023.Day22;

class Solution : SolutionBase
{
    public Solution() : base(22, 2023, "") { }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        SandBlock sandBlock = new SandBlock();

        foreach (string line in lines)
        {
            sandBlock.AddBrick(line);
        }

        return "";
    }

    protected override string SolvePartTwo()
    {

        var lines = Input.SplitByNewline();

        foreach (string line in lines)
        {

        }

        return "";
    }

    public class SandBlock()
    {
        List<Brick> Bricks = [];

        int MaxX = 0;
        int MaxY = 0;
        int MaxZ = 0;



        public void AddBrick(string line)
        {
            string[] start = line.Split("~")[0].Split(",");
            string[] end = line.Split("~")[1].Split(",");

            int startX = Int32.Parse(start[0]);
            int startY = Int32.Parse(start[1]);
            int startZ = Int32.Parse(start[2]);
            int endX = Int32.Parse(end[0]);
            int endY = Int32.Parse(end[1]);
            int endZ = Int32.Parse(end[2]);

            CheckX(startX, endX);
            CheckY(startY, endY);
            CheckZ(startZ, endZ);

            Bricks.Add(new Brick(startX, startY, startZ, endX, endY, endZ));
        }

        public void CheckX(int startX, int endX)
        {
            if (MaxX < startX)
            {
                MaxX = startX;
            }
            if (MaxX < endX)
            {
                MaxX = endX;
            }

        }
        public void CheckY(int startY, int endY)
        {
            if (MaxY < startY)
            {
                MaxY = startY;
            }
            if (MaxY < endY)
            {
                MaxY = endY;
            }

        }
        public void CheckZ(int startZ, int endZ)
        {
            if (MaxZ < startZ)
            {
                MaxZ = startZ;
            }
            if (MaxZ < endZ)
            {
                MaxZ = endZ;
            }

        }

        public void GenerateGrid()
        {

            // create a 3D boolean array

        }
    }

    public class Brick(int startX, int startY, int startZ, int endX, int endY, int endZ)
    {
        int StartX { get; set; } = startX;
        int StartY { get; set; } = startY;
        int StartZ { get; set; } = startZ;
        int EndX { get; set; } = endX;
        int EndY { get; set; } = endY;
        int EndZ { get; set; } = endZ;
    }

    public class Layer(){
        
    }

}
