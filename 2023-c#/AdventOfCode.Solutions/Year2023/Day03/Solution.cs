using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace AdventOfCode.Solutions.Year2023.Day03;

class Solution : SolutionBase
{
    public Solution() : base(03, 2023, "") { }

    private Boolean checkChar(Char test)
    {
        if (!Char.IsNumber(test) && !test.Equals('.'))
        {
            return true;
        }
        return false;
    }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        int solution = 0;

        // each line in the set
        for (int i = 0; i < lines.Length; i++)
        {

            // debug
            //Console.WriteLine(lines[i]);

            Boolean haveNum = false;
            String num = "";
            Boolean isValid = false;



            // each character in the line
            for (int c = 0; c < lines[i].Length; c++)
            {
                char currChar = lines[i][c];

                if (Char.IsNumber(currChar))
                {
                    haveNum = true;

                    // concat number to string
                    num = num + currChar;

                }


                // check if char touches symbol
                //      set isValid to true if it does
                // [1] [2] [3]
                // [4] [ ] [5]
                // [6] [7] [8]

                char check;

                //Console.WriteLine("---------------------------------" + currChar + " - " + haveNum + " - " + isValid);


                if (Char.IsNumber(currChar))
                {

                    // check case 1
                    if (c != 0 && i != 0)
                    {

                        check = lines[i - 1][c - 1];
                        //Console.Write("\t[" + check);

                        if (checkChar(check))
                        {
                            isValid = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }

                        //Console.Write("] ");
                    }
                    else
                    {
                        //Console.Write("\t[X ] ");
                    }

                    // check case 2
                    if (i != 0)
                    {
                        check = lines[i - 1][c];
                        //Console.Write("[" + check);

                        if (checkChar(check))
                        {
                            isValid = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("] ");

                    }
                    else
                    {
                        // Console.Write("[X ] ");
                    }

                    // check case 3
                    if (i != 0 && c != (lines[i].Length - 1))
                    {
                        check = lines[i - 1][c + 1];
                        //Console.Write("[" + check);

                        if (checkChar(check))
                        {
                            isValid = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("]\n");

                    }
                    else
                    {
                        //Console.Write("[X ]\n");
                    }

                    // check case 4
                    if (c != 0)
                    {

                        check = lines[i][c - 1];
                        //Console.Write("\t[" + check);

                        if (checkChar(check))
                        {
                            isValid = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("] ");

                    }
                    else
                    {
                        //Console.Write("\t[X ] ");
                    }

                    //Console.Write("[" + currChar + " ] ");

                    // check case 5
                    if (c != (lines[i].Length - 1))
                    {
                        check = lines[i][c + 1];
                        //Console.Write("[" + check);

                        if (checkChar(check))
                        {
                            isValid = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("]\n");

                    }
                    else
                    {
                        //Console.Write("[X ]\n");
                    }

                    // check case 6
                    if (c != 0 && i != (lines.Length - 1))
                    {
                        check = lines[i + 1][c - 1];
                        //Console.Write("\t[" + check);

                        if (checkChar(check))
                        {
                            isValid = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("] ");

                    }
                    else
                    {
                        //Console.Write("\t[X ] ");
                    }

                    // check case 7
                    if (i != (lines.Length - 1))
                    {

                        check = lines[i + 1][c];
                        //Console.Write("[" + check);

                        if (checkChar(check))
                        {
                            isValid = true;
                            // Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("] ");

                    }
                    else
                    {
                        // Console.Write("[X ] ");
                    }

                    // check case 8
                    if (i != (lines.Length - 1) && c != (lines[i].Length - 1))
                    {
                        check = lines[i + 1][c + 1];
                        //Console.Write("[" + check);

                        if (checkChar(check))
                        {
                            isValid = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            // Console.Write(" ");
                        }
                        // Console.Write("]\n");

                    }
                    else
                    {
                        //Console.Write("[X ]\n");
                    }



                }


                //calculate result if needed
                if (haveNum && (!Char.IsNumber(currChar) || c == (lines[i].Length - 1)))
                {

                    //Console.Write("Testing: " + num + "  - ");

                    if (isValid)
                    {
                        solution += Int32.Parse(num);
                        // Console.Write("YES");
                    }

                    // Console.WriteLine();

                    // reset
                    haveNum = false;
                    num = "";
                    isValid = false;

                }




            }

        }

        return solution.ToString();
    }

    protected override string SolvePartTwo()
    {
        var lines = Input.SplitByNewline();
        int solution = 0;

        // each line in the set
        for (int i = 0; i < lines.Length; i++)
        {

            // debug
            //Console.WriteLine(lines[i]);



            // each character in the line
            for (int c = 0; c < lines[i].Length; c++)
            {
                char currChar = lines[i][c];



                char check;





                // astrix touches 2 numbers, then multiply them
                // [1] [2] [3]
                // [4] [ ] [5]
                // [6] [7] [8]



                // checking for part numbers
                if ((currChar.Equals('*')))
                {
                    //Console.WriteLine("---------------------------------");

                    bool one = false;
                    bool two = false;
                    bool three = false;
                    bool four = false;
                    bool five = false;
                    bool six = false;
                    bool seven = false;
                    bool eight = false;

                    // check case 1
                    if (c != 0 && i != 0)
                    {

                        check = lines[i - 1][c - 1];
                        //Console.Write("\t[" + check);

                        if (Char.IsNumber(check))
                        {
                            one = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }

                        //Console.Write("] ");
                    }
                    else
                    {
                        //Console.Write("\t[X ] ");
                    }

                    // check case 2
                    if (i != 0)
                    {
                        check = lines[i - 1][c];
                        //Console.Write("[" + check);

                        if (Char.IsNumber(check))
                        {
                            two = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("] ");

                    }
                    else
                    {
                        //Console.Write("[X ] ");
                    }

                    // check case 3
                    if (i != 0 && c != (lines[i].Length - 1))
                    {
                        check = lines[i - 1][c + 1];
                        //Console.Write("[" + check);

                        if (Char.IsNumber(check))
                        {
                            three = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("]\n");

                    }
                    else
                    {
                        //Console.Write("[X ]\n");
                    }

                    // check case 4
                    if (c != 0)
                    {

                        check = lines[i][c - 1];
                        //Console.Write("\t[" + check);

                        if (Char.IsNumber(check))
                        {
                            four = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("] ");

                    }
                    else
                    {
                        //Console.Write("\t[X ] ");
                    }

                    //Console.Write("[" + currChar + " ] ");

                    // check case 5
                    if (c != (lines[i].Length - 1))
                    {
                        check = lines[i][c + 1];
                        //Console.Write("[" + check);

                        if (Char.IsNumber(check))
                        {
                            five = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("]\n");

                    }
                    else
                    {
                        //Console.Write("[X ]\n");
                    }

                    // check case 6
                    if (c != 0 && i != (lines.Length - 1))
                    {
                        check = lines[i + 1][c - 1];
                        //Console.Write("\t[" + check);

                        if (Char.IsNumber(check))
                        {
                            six = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("] ");

                    }
                    else
                    {
                        //Console.Write("\t[X ] ");
                    }

                    // check case 7
                    if (i != (lines.Length - 1))
                    {

                        check = lines[i + 1][c];
                        //Console.Write("[" + check);

                        if (Char.IsNumber(check))
                        {
                            seven = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("] ");

                    }
                    else
                    {
                        //Console.Write("[X ] ");
                    }

                    // check case 8
                    if (i != (lines.Length - 1) && c != (lines[i].Length - 1))
                    {
                        check = lines[i + 1][c + 1];
                        //Console.Write("[" + check);

                        if (Char.IsNumber(check))
                        {
                            eight = true;
                            //Console.Write("!");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                        //Console.Write("]\n");

                    }
                    else
                    {
                        //Console.Write("[X ]\n");
                    }


                    // [1] [2] [3]
                    // [4] [ ] [5]
                    // [6] [7] [8]

                    if (
                        (one && ((!two && three) || four || five || six || seven || eight || seven)) ||
                        (two && (four || five || six || seven || eight)) ||
                        (three && ((one && !two) || four || five || six || seven || eight)) ||
                        (four && (one || two || three | five || six || seven || eight)) ||
                        (five && (one || two || three || four || six || seven || eight)) ||
                        (six && ((eight && !seven) || one || two || three || four || five)) ||
                        (seven && (one || two || three || four || five)) ||
                        (eight && ((six && !seven) || one || two || three || four || five))
                        )
                    {
                        //Console.WriteLine("COG - " + lines[i]);

                        // find values
                        int test = -1;
                        int val1 = -1;
                        int cogVal = -1;


                        if (one)
                        {
                            test = getNum((c - 1), lines[i - 1]);
                            val1 = test;

                        }

                        if (two && !one)
                        {
                            test = getNum((c), lines[i - 1]);
                            if (val1 == -1)
                            {
                                val1 = test;
                            }
                            else
                            {
                                cogVal = val1 * test;
                            }

                        }

                        if (three && !two)
                        {
                            test = getNum((c + 1), lines[i - 1]);
                            if (val1 == -1)
                            {
                                val1 = test;
                            }
                            else
                            {
                                cogVal = val1 * test;
                            }


                        }

                        if (four)
                        {
                            test = getNum((c - 1), lines[i]);
                            if (val1 == -1)
                            {
                                val1 = test;
                            }
                            else
                            {
                                cogVal = val1 * test;
                            }


                        }

                        if (five)
                        {
                            test = getNum((c + 1), lines[i]);
                            if (val1 == -1)
                            {
                                val1 = test;
                            }
                            else
                            {
                                cogVal = val1 * test;
                            }


                        }

                        if (six)
                        {
                            test = getNum((c - 1), lines[i + 1]);
                            if (val1 == -1)
                            {
                                val1 = test;
                            }
                            else
                            {
                                cogVal = val1 * test;
                            }


                        }

                        if (seven && !six)
                        {
                            test = getNum((c), lines[i + 1]);
                            if (val1 == -1)
                            {
                                val1 = test;
                            }
                            else
                            {
                                cogVal = val1 * test;
                            }

                        }

                        if (eight && !seven)
                        {
                            test = getNum((c + 1), lines[i + 1]);
                            if (val1 == -1)
                            {
                                val1 = test;
                            }
                            else
                            {
                                cogVal = val1 * test;
                            }

                        }


                        solution += cogVal;

                    }
                    else
                    {
                        //Console.WriteLine("NOT COG");
                    }

                }

            }

        }

        return solution.ToString();
    }


    private int getNum(int index, string source)
    {

        //Console.WriteLine("Checking - " + source);
        //Console.WriteLine(index + " - " + source[index]);
        String value = "";

        // backtrack till non-numeric
        while (index >= 0 && Char.IsNumber(source[index]))
        {
            //Console.WriteLine("Backing up");
            index--;

        };


        index++;
        //Console.WriteLine("setting idex - " + index);



        // forward track till non-numeric
        while (index < source.Length && Char.IsNumber(source[index]))
        {
            value += source[index];
            index++;
        };

        return Int32.Parse(value);

    }
}
