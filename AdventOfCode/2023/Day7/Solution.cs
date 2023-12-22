using System.Text;

namespace AdventOfCode.Y2023.Day7;

internal class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        var hands = new List<Hand>();

        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var lineParts = line.Split(' ');
            var cards = lineParts[0].Select(x => x.ToString()).ToArray();
            var bet = int.Parse(lineParts[1]);
            var hand = new Hand(cards, bet, Hand.ParseCardPartOne);

            hands.Add(hand);
        }

        hands.Sort();

        return hands
            .Select((x, i) => (long)x.Bet * (i + 1))
            .Sum();
    }

    public override long SolvePartTwo()
    {
        var hands = new List<Hand>();

        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var lineParts = line.Split(' ');
            var cards = lineParts[0].Select(x => x.ToString()).ToArray();
            var bet = int.Parse(lineParts[1]);
            var hand = new Hand(cards, bet, Hand.ParseCardPartTwo);

            hands.Add(hand);
        }

        hands.Sort();

        return hands
            .Select((x, i) => (long)x.Bet * (i + 1))
            .Sum();
    }
}

class Hand : IComparable<Hand>
{
    public Card[] Cards { get; internal set; } = [];
    public int Bet { get; internal set; }
    public HandType Type { get; internal set; }

    public Hand(string[] cards, int bet, Func<string, Card> parseCard)
    {
        Cards = cards.Select(parseCard).ToArray();
        Bet = bet;

        Type = DetermineHandType(Cards);
    }

    private static HandType DetermineHandType(Card[] cards)
    {
        var NumberOfJacks = cards.Count(x => x == Card.JackPartTwo);
        var cardCounts = new Dictionary<Card, int>();
        foreach (var card in cards)
        {
            if (cardCounts.TryGetValue(card, out int value))
            {
                cardCounts[card] = ++value;
            }
            else
            {
                cardCounts[card] = 1;
            }
        }

        var dictWithOutJacks = cardCounts.Where(x => x.Key != Card.JackPartTwo).ToDictionary(x => x.Key, x => x.Value);

        if (cardCounts.ContainsValue(5) || dictWithOutJacks.ContainsValue(5 - NumberOfJacks))
        {
            return HandType.FiveOfAKind;
        }
        else if (cardCounts.ContainsValue(4) || dictWithOutJacks.ContainsValue(4 - NumberOfJacks))
        {
            return HandType.FourOfAKind;
        }
        else if ((cardCounts.ContainsValue(3) && cardCounts.ContainsValue(2)) || (dictWithOutJacks.Count(x => x.Value == 2) == 2 && NumberOfJacks == 1))
        {
            return HandType.FullHouse;
        }
        else if (cardCounts.ContainsValue(3) || dictWithOutJacks.ContainsValue(3 - NumberOfJacks))
        {
            return HandType.ThreeOfAKind;
        }
        else if (cardCounts.Count(x => x.Value == 2) == 2 || (dictWithOutJacks.Count(x => x.Value == 2) == 1 && NumberOfJacks == 1))
        {
            return HandType.TwoPair;
        }
        else if (cardCounts.ContainsValue(2) || dictWithOutJacks.ContainsValue(2 - NumberOfJacks))
        {
            return HandType.Pair;
        }
        else
        {
            return HandType.HighCard;
        }
    }

    public static Card ParseCardPartOne(string card)
    {
        return card switch
        {
            "2" => Card.Two,
            "3" => Card.Three,
            "4" => Card.Four,
            "5" => Card.Five,
            "6" => Card.Six,
            "7" => Card.Seven,
            "8" => Card.Eight,
            "9" => Card.Nine,
            "T" => Card.Ten,
            "J" => Card.JackPartOne,
            "Q" => Card.Queen,
            "K" => Card.King,
            "A" => Card.Ace,
            _ => throw new ArgumentException($"Invalid card: {card}", nameof(card)),
        };
    }

    public static Card ParseCardPartTwo(string card)
    {
        return card switch
        {
            "2" => Card.Two,
            "3" => Card.Three,
            "4" => Card.Four,
            "5" => Card.Five,
            "6" => Card.Six,
            "7" => Card.Seven,
            "8" => Card.Eight,
            "9" => Card.Nine,
            "T" => Card.Ten,
            "J" => Card.JackPartTwo,
            "Q" => Card.Queen,
            "K" => Card.King,
            "A" => Card.Ace,
            _ => throw new ArgumentException($"Invalid card: {card}", nameof(card)),
        };
    }

    public int CompareTo(Hand? otherHand)
    {
        if (otherHand == null)
        {
            return 1;
        }

        if (Type > otherHand.Type)
        {
            return 1;
        }
        else if (Type < otherHand.Type)
        {
            return -1;
        }
        else
        {
            for (var i = 0; i < Cards.Length; i++)
            {
                if (Cards[i] > otherHand.Cards[i])
                {
                    return 1;
                }
                else if (Cards[i] < otherHand.Cards[i])
                {
                    return -1;
                }
            }
        }

        return 0;
    }
}

enum HandType
{
    HighCard,
    Pair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
}

enum Card
{
    JackPartTwo,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    JackPartOne,
    Queen,
    King,
    Ace
}