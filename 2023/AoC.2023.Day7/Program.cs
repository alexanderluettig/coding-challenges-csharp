using System.Collections;
using System.Text;

namespace AoC._2023.Day7;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        var hands = new List<Hand>();

        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            var lineParts = line.Split(' ');
            var cards = lineParts[0].Select(x => x.ToString()).ToArray();
            var bet = int.Parse(lineParts[1]);
            var hand = new Hand(cards, bet);

            hands.Add(hand);
        }

        hands.Sort();

        long result = hands
            .Select((x, i) => (long)x.Bet * (i + 1))
            .Sum();

        Console.WriteLine(result);
    }
}

class Hand : IComparable<Hand>
{
    public Card[] Cards { get; internal set; } = [];
    public int Bet { get; internal set; }
    public HandType Type { get; internal set; }
    private int NumberOfJacks => Cards.Count(x => x == Card.Jack);

    public Hand(string[] cards, int bet)
    {
        Cards = cards.Select(ParseCard).ToArray();
        Bet = bet;

        DetermineHandType();
    }

    private void DetermineHandType()
    {
        var cardCounts = new Dictionary<Card, int>();
        foreach (var card in Cards)
        {
            if (cardCounts.ContainsKey(card))
            {
                cardCounts[card]++;
            }
            else
            {
                cardCounts[card] = 1;
            }
        }

        var dictWithOutJacks = cardCounts.Where(x => x.Key != Card.Jack).ToDictionary(x => x.Key, x => x.Value);

        if (cardCounts.ContainsValue(5) || dictWithOutJacks.ContainsValue(5 - NumberOfJacks))
        {
            Type = HandType.FiveOfAKind;
        }
        else if (cardCounts.ContainsValue(4) || dictWithOutJacks.ContainsValue(4 - NumberOfJacks))
        {
            Type = HandType.FourOfAKind;
        }
        else if ((cardCounts.ContainsValue(3) && cardCounts.ContainsValue(2)) || (dictWithOutJacks.Count(x => x.Value == 2) == 2 && NumberOfJacks == 1))
        {
            Type = HandType.FullHouse;
        }
        else if (cardCounts.ContainsValue(3) || dictWithOutJacks.ContainsValue(3 - NumberOfJacks))
        {
            Type = HandType.ThreeOfAKind;
        }
        else if (cardCounts.Count(x => x.Value == 2) == 2 || (dictWithOutJacks.Count(x => x.Value == 2) == 1 && NumberOfJacks == 1))
        {
            Type = HandType.TwoPair;
        }
        else if (cardCounts.ContainsValue(2) || dictWithOutJacks.ContainsValue(2 - NumberOfJacks))
        {
            Type = HandType.Pair;
        }
        else
        {
            Type = HandType.HighCard;
        }
    }

    private static Card ParseCard(string card)
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
            "J" => Card.Jack,
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
    Jack,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Queen,
    King,
    Ace
}