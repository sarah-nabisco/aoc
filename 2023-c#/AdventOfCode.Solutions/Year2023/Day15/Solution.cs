namespace AdventOfCode.Solutions.Year2023.Day15;

class Solution : SolutionBase
{
    public Solution() : base(15, 2023, "") { }

    protected override string SolvePartOne()
    {
        List<string> inputs = [.. Input.SplitByNewline()[0].Split(",")];
        long results = 0;

        foreach (string input in inputs)
        {
            results += ParseHash(input);
        }

        return results.ToString();
    }

    private int ParseHash(string input)
    {
        int result = 0;

        foreach (char curr in input)
        {
            //Increase the current value by the ASCII code for the current character of the string
            result += ((int)curr);
            //Set the current value to itself multiplied by 17.
            result *= 17;
            //Set the current value to the remainder of dividing itself by 256.
            result %= 256;
        }


        return result;
    }

    protected override string SolvePartTwo()
    {

        List<string> inputs = [.. Input.SplitByNewline()[0].Split(",")];
        Boxes boxes = new Boxes();

        foreach (string input in inputs)
        {
            Lens lens = new Lens(input);
            Console.WriteLine(input + " = " + lens.label + "|" + lens.focalLength + "|" + lens.hasDash + " -> " + lens.boxIndex);
            boxes.AddLens(lens);
        }
        //boxes.Print();




        return boxes.TotalFocus().ToString();
    }
    public class Lens
    {
        public string input;
        public string label;
        public int focalLength;
        public bool hasDash;
        public int boxIndex;

        public Lens(string a)
        {
            input = a;
            hasDash = true;

            int index = input.IndexOf('-');

            if (index == -1)
            {
                index = input.IndexOf('=');
                hasDash = false;
            }
            if (index == -1)
            {
                Console.WriteLine("WE HAVE A PROBELM WITH INPUT: " + input + "----------------\n------------------\n---------------------");
            }

            label = input.Substring(0, index);

            if (!hasDash)
            {
                //Console.WriteLine("Parse " + input[(index + 1)..] + " -> " + index + " | " + hasDash + " | " + input);
                focalLength = int.Parse(input[(index + 1)..]);
            }

            boxIndex = 0;
            foreach (char curr in label)
            {
                //Increase the current value by the ASCII code for the current character of the string
                boxIndex += ((int)curr);
                //Set the current value to itself multiplied by 17.
                boxIndex *= 17;
                //Set the current value to the remainder of dividing itself by 256.
                boxIndex %= 256;
            }

        }

    }
    public class Boxes
    {
        public List<Lens>[] boxes;

        public Boxes()
        {
            boxes = new List<Lens>[256];
            for (int i = 0; i < 256; i++)
            {
                boxes[i] = [];
            }
        }

        public void AddLens(Lens lens)
        {

            List<Lens> currBox = boxes[lens.boxIndex];
            int index = ContainsLable(lens, currBox);

            if (index != -1)
            {
                currBox.RemoveAt(index);

            }
            // DASH
            // ----check if box includes lens with label
            // ----if it does remove it

            // EQUALS
            // ----check if box includes a lens with label
            // ----     yes - swap out with new lens
            // ----     no -  add to back of box


            if (!lens.hasDash)
            {
                if (index != -1)
                {
                    currBox.Insert(index, lens);
                }
                else
                {
                    currBox.Add(lens);
                }

            }

            boxes[lens.boxIndex] = currBox;


        }

        private int ContainsLable(Lens lens, List<Lens> box)
        {
            int index = 0;
            bool found = false;

            foreach (Lens boxLens in box)
            {
                if (!found)
                {
                    if (boxLens.label.Equals(lens.label))
                    {
                        found = true;
                    }
                    else
                    {
                        index++;
                    }

                }

            }

            return found ? index : -1;
        }

        public void Print()
        {
            Console.WriteLine("--------------------------------------");
            int index = 0;
            foreach (List<Lens> box in boxes)
            {
                if (box.Count > 0)
                {
                    Console.Write("Box " + index + ": ");

                    foreach (Lens lens in box)
                    {
                        Console.Write("[" + lens.label + " " + lens.focalLength + "] ");
                    }
                    Console.Write("\n");
                }

                index++;
            }
            Console.WriteLine("--------------------------------------");
        }

        public long TotalFocus()
        {
            int boxIndex = 0;
            long totalFocus = 0;

            foreach (List<Lens> box in boxes)
            {
                if (box.Count > 0)
                {
                    int lensIndex = 1;

                    foreach (Lens lens in box)
                    {
                        long lensFocus = 1 + boxIndex;
                        lensFocus *= lensIndex;
                        lensFocus *= lens.focalLength;

                        totalFocus += lensFocus;
                        //Console.WriteLine("Box " + boxIndex + " - " + lens.label + " " + lens.focalLength + " | [boxindex " + (boxIndex + 1) + "] * [slot " + lensIndex + "] * [focal " + lens.focalLength + "] -> " + lensFocus);


                        lensIndex++;
                    }

                }

                boxIndex++;
            }

            return totalFocus;
        }
    }


}
