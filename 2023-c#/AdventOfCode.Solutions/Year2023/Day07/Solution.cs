using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace AdventOfCode.Solutions.Year2023.Day07;

class Solution : SolutionBase
{
    public Solution() : base(07, 2023, "") { }


    private Dictionary<char, int> createCounts(string hand)
    {
        Dictionary<char, int> counts = new Dictionary<char, int>();
        foreach (char card in hand)
        {
            if (counts.ContainsKey(card))
            {
                counts[card] = (counts[card] + 1);
            }
            else
            {
                counts.Add(card, 1);
            }
        }
        return counts;

    }
    private int getType(string hand)
    {
        Dictionary<char, int> counts = createCounts(hand);

        switch (counts.Keys.Count)
        {
            case 1:
                // 5 of a kind
                return 7;
            case 2:
                if (counts.ContainsValue(4))
                {
                    // 4 of a kind
                    return 6;

                }
                else
                {
                    // full house
                    return 5;
                }


            case 3:
                if (counts.ContainsValue(3))
                {
                    // 3 of a kind
                    return 4;

                }
                else
                {
                    // 2 pair
                    return 3;
                }
            case 4:
                // 1 pair
                return 2;
            default:
                // high card
                return 1;
        }

    }


    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        Hands hands = new Hands();

        foreach (string line in lines)
        {
            string cards = line.Split(" ")[0];
            int bet = Int32.Parse(line.Split(" ")[1]);
            int type = getType(cards);

            hands.AddHand(new Hand(cards, type, bet, true));
        }

        //hands.PrintHands();
        hands.BubbleSort();
        Console.WriteLine("-------------------------");
        //hands.PrintHands();


        return hands.Winnings().ToString();
    }

    private String OptimizeHand(String hand)
    {
        String result = hand;

        /*
7 - Five of a kind, where all five cards have the same label: AAAAA
6 - Four of a kind, where four cards have the same label and one card has a different label: AA8AA
5 - Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
4 - Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
3 - Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
2 - One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
1 - High card, where all cards' labels are distinct: 23456
*/

        if (hand.Contains("J"))
        {
            // clean up result
            Dictionary<char, int> counts = createCounts(hand);

            if (counts.ContainsKey('J') && counts.Keys.Count > 1)
            {
                int numJokers = counts['J'];
                counts.Remove('J');

                // get thing with highest count
                char maxKey = counts.FirstOrDefault(x => x.Value == counts.Values.Max()).Key;

                // add numJokers to it
                counts[maxKey] += numJokers;

                // rebuild string
                result = "";
                foreach (char ch in counts.Keys)
                {
                    for (int i = 0; i < counts[ch]; i++)
                    {
                        result += ch;
                    }
                }

                //Console.WriteLine("CHANGE - " + hand + " to " + result);



            }


        }

        return result;
    }

    protected override string SolvePartTwo()
    {

        var lines = Input.SplitByNewline();
        Hands hands = new Hands();

        foreach (string line in lines)
        {
            string cards = line.Split(" ")[0];
            int bet = Int32.Parse(line.Split(" ")[1]);
            string optimizedHand = OptimizeHand(cards);
            int type = getType(optimizedHand);

            hands.AddHand(new Hand(cards, type, bet, false));
        }

        //hands.PrintHands();
        hands.BubbleSort();
        //Console.WriteLine("-------------------------");
        //hands.PrintHands();


        return hands.Winnings().ToString();
    }

    public class Hand(string cards, int type, int bet, Boolean type1)
    {
        private char[] ValuesPt1 = new char[] { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
        private char[] ValuesPt2 = new char[] { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };
        public string Cards { get; set; } = cards;
        public int Type { get; set; } = type;
        public int Bet { get; set; } = bet;
        public Boolean Type1 { get; set; } = type1;

        public Boolean IsLarger(Hand newHand)
        {
            // return true if hand > newHand
            if (Type > newHand.Type)
            {
                return true;
            }
            else if (Type < newHand.Type)
            {
                return false;
            }
            else
            {
                // deep compare
                for (int index = 0; index < 5; index++)
                {
                    if (newHand.Cards[index] != Cards[index])
                    {
                        return CardVal(Cards[index]) > CardVal(newHand.Cards[index]);
                    }
                }

            }
            return false;
        }

        private int CardVal(char test)
        {
            char[] values = Type1 ? ValuesPt1 : ValuesPt2;

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].Equals(test))
                {
                    return i;
                }
            }
            return 0;
        }

    }

    public class Hands()
    {
        List<Hand> hands = new();

        public void AddHand(Hand newHand)
        {
            hands.Add(newHand);
        }

        public void BubbleSort()
        {
            var n = hands.Count;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)

                    // TODO replace with a deep compare
                    if (!hands[j + 1].IsLarger(hands[j]))
                    {
                        var tempVar = hands[j];
                        hands[j] = hands[j + 1];
                        hands[j + 1] = tempVar;
                    }
        }


        public void PrintHands()
        {

            foreach (Hand hand in hands)
            {
                Console.WriteLine(hand.Cards + " - " + hand.Type);
            }
        }

        public long Winnings()
        {
            long winnings = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                winnings += ((i + 1) * hands[i].Bet);
            }
            return winnings;
        }



    }
}
