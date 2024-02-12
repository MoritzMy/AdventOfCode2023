using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2023
{
    enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }
    class CardHand
    {
        string cards;
        int bid;
        int rank;
        HandType type;

        public CardHand(string cards, int bid)
        {
            this.cards = cards;
            this.bid = bid;
            this.type = HandType.HighCard;
            this.rank = 0;
        }
        public string GetCards()
        {
            return this.cards;
        }
        public int GetBid()
        {
            return this.bid;
        }
        public void SetRank(int rank)
        {
            this.rank = rank;
        }
        public int GetRank() 
        { 
            return this.rank;
        }
        public void SetType(HandType type)
        {
            this.type = type;
        }
        new public HandType GetType()
        {
            return this.type;
        }
        public override string ToString()
        {
            return $"{cards}: {type}, {rank}";
        }

    }
    internal class Day7
    {
        static int counter = 1;
        public void Day7Solutions()
        {
            List<CardHand> PairOf5 = new List<CardHand>();
            List<CardHand> PairOf4 = new List<CardHand>();
            List<CardHand> FullHouse = new List<CardHand>();
            List<CardHand> PairOf3 = new List<CardHand>();
            List<CardHand> TwoPairsOf2 = new List<CardHand>();
            List<CardHand> PairsOf2 = new List<CardHand>();
            List<CardHand> HighestCard = new List<CardHand>();
            List<List<CardHand>> AllCards = new List<List<CardHand>>() { PairOf5, PairOf4, FullHouse, PairOf3, TwoPairsOf2, PairsOf2, HighestCard};
           
            StreamReader sr = new StreamReader("C:\\Users\\morit\\source\\repos\\AdventOfCode2023\\Day7Puzzle.txt");
            while (!sr.EndOfStream)
            {
                string[] hand = sr.ReadLine().Split(" ");
                CardHand cards = new CardHand(hand[0], Convert.ToInt32(hand[1]));
                DetermineCardType(cards);
                Console.WriteLine(cards);
                switch(cards.GetType())
                {
                    case HandType.FiveOfAKind:
                        PairOf5.Add(cards);
                        break;
                    case HandType.FourOfAKind:
                        PairOf4.Add(cards);
                        break;
                    case HandType.FullHouse:
                        FullHouse.Add(cards);
                        break;
                    case HandType.ThreeOfAKind:
                        PairOf3.Add(cards);
                        break;
                    case HandType.TwoPair:
                        TwoPairsOf2.Add(cards);
                        break;
                    case HandType.OnePair:
                        PairsOf2.Add(cards);
                        break;
                    case HandType.HighCard:
                        HighestCard.Add(cards);
                        break;                        
                }

            }
            sr.Close();
            int sum = 0;
            Dictionary<char, int> map = new Dictionary<char, int>() { { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 },
                { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 }, { 'T', 10 }, { 'J', 11 }, { 'Q', 12 }, { 'K', 13 }, { 'A', 14 } };

            for (int i = AllCards.Count - 1; i >= 0; i--)
            {
                SortList(AllCards[i], map);
                foreach (CardHand hand in AllCards[i])
                {
                    sum += hand.GetRank() * hand.GetBid();
                }
                
            }
            
            counter = 1;
            PairOf5 = new List<CardHand>();
            PairOf4 = new List<CardHand>();
            FullHouse = new List<CardHand>();
            PairOf3 = new List<CardHand>();
            TwoPairsOf2 = new List<CardHand>();
            PairsOf2 = new List<CardHand>();
            HighestCard = new List<CardHand>();
            AllCards = new List<List<CardHand>>() { PairOf5, PairOf4, FullHouse, PairOf3, TwoPairsOf2, PairsOf2, HighestCard };

            sr = new StreamReader("C:\\Users\\morit\\source\\repos\\AdventOfCode2023\\Day7Puzzle.txt");
            while (!sr.EndOfStream)
            {
                string[] hand = sr.ReadLine().Split(" ");
                CardHand cards = new CardHand(hand[0], Convert.ToInt32(hand[1]));
                DetermineCardTypeWithJoker(cards);
                Console.WriteLine(cards);
                switch (cards.GetType())
                {
                    case HandType.FiveOfAKind:
                        PairOf5.Add(cards);
                        break;
                    case HandType.FourOfAKind:
                        PairOf4.Add(cards);
                        break;
                    case HandType.FullHouse:
                        FullHouse.Add(cards);
                        break;
                    case HandType.ThreeOfAKind:
                        PairOf3.Add(cards);
                        break;
                    case HandType.TwoPair:
                        TwoPairsOf2.Add(cards);
                        break;
                    case HandType.OnePair:
                        PairsOf2.Add(cards);
                        break;
                    case HandType.HighCard:
                        HighestCard.Add(cards);
                        break;
                }

            }
            sr.Close();
            Console.WriteLine(sum);
            map.Remove('J');
            map.Add('J', 1);
            sum = 0;
            int previousRank = 0;
            for (int i = AllCards.Count - 1; i >= 0; i--)
            {
                SortList(AllCards[i], map);
                foreach (CardHand hand in AllCards[i])
                {
                    sum += hand.GetRank() * hand.GetBid();
                }
                for (int p = 0; p < AllCards[i].Count; p++)
                {
                    for (int j = 0; j < AllCards[i].Count; j++)
                    {
                        if (AllCards[i][j].GetRank() == previousRank + 1) { Console.WriteLine(AllCards[i][j]); previousRank++; }
                    }
                }
            }
            Console.WriteLine(sum);


        }
        public void DetermineCardType(CardHand hand)
        {
            int counter = 0;
            char[] cards = hand.GetCards().ToArray();
            for(int i = 0; i < cards.Length; i++)
            {
                
                if (cards[i] == 'A')
                {
                    cards[i] = 'H';
                }
            }
            for (int i = 0; i < cards.Length; i++)
            {
                char temp = cards[i];
                if (cards[i] - 'A' > 4 || char.IsDigit(cards[i]))
                {
                    cards[i] = (char)('A' + counter);
                    counter++;
                }
                else
                {
                    continue;
                }

                for (int j = i + 1; j < cards.Length; j++)
                {
                    if (temp == cards[j])
                    {
                        cards[j] = cards[i];
                    }
                }
            }
            string cardsString = "";
            foreach (char c in cards)
            {
                cardsString += c;
            }
            int[] charNums = GetAmountOfChars(cardsString);
            if (!cardsString.Contains('B'))
            {
                hand.SetType(HandType.FiveOfAKind);
            }
            else if(!cardsString.Contains('C'))
            {
                if (charNums[0] == 4 || charNums[1] == 4 || charNums[2] == 4)
                {
                    hand.SetType(HandType.FourOfAKind);
                    return;
                }
                hand.SetType(HandType.FullHouse);
                return;
            }
            else if(!cardsString.Contains('D'))
            {
                if (charNums[0] == 2 || charNums[1] == 2 || charNums[2] == 2)
                {
                    hand.SetType (HandType.TwoPair);
                    return;
                } 
                hand.SetType(HandType.ThreeOfAKind);
                return;
            }
            else if (cardsString.Contains('E'))
            {
                hand.SetType(HandType.HighCard);
                return;
            }
            else if (cardsString.Contains('D'))
            {
                hand.SetType(HandType.OnePair);
                return;
            }
            

        }
        public int[] GetAmountOfChars(string cards)
        {
            int[] charNums = new int[cards.Length + 1];
            foreach (char c in cards)
            {
                if (c == 'J') charNums[charNums.Length - 1]++;
                else charNums[(int)(c - 'A')]++;
            }
            return charNums;
        }
        public void SortList(List<CardHand> CardList, Dictionary<char, int> map)
        {
            CardHand[] Cards = CardList.ToArray();
            for (int i = 0; i < Cards.Length; i++)
            {
                CardHand lowestCard = Cards[i];
                CardHand temp = Cards[i];
                for (int j = i + 1; j < Cards.Length; j++)
                {
                    map.TryGetValue(Cards[j].GetCards()[0], out int CurrentCardValue);
                    map.TryGetValue(lowestCard.GetCards()[0], out int lowestCardValue);
                    if (CurrentCardValue < lowestCardValue)
                    {
                        lowestCard = Cards[j];
                        Cards[i] = lowestCard;
                        Cards[j] = temp;
                        temp = Cards[i];
                    }
                    else if (CurrentCardValue == lowestCardValue)
                    {
                        for (int p = 1; p < Cards[j].GetCards().Length; p++)
                        {
                            map.TryGetValue(lowestCard.GetCards()[p], out lowestCardValue);
                            map.TryGetValue(Cards[j].GetCards()[p], out CurrentCardValue);
                            if (lowestCardValue > CurrentCardValue)
                            {
                                lowestCard = Cards[j];
                                Cards[i] = lowestCard;
                                Cards[j] = temp;
                                temp = Cards[i];
                                break;
                            }
                            else if (lowestCardValue < CurrentCardValue)
                            {
                                break;
                            }
                        }
                    }
                }
                lowestCard.SetRank(counter);

                counter++;

            }
        }
        public void DetermineCardTypeWithJoker(CardHand hand)
        {
            int counter = 0;
            char[] cards = hand.GetCards().ToArray();
            for (int i = 0; i < cards.Length; i++)
            {

                if (cards[i] == 'A')
                {
                    cards[i] = 'H';
                }
            }
            for (int i = 0; i < cards.Length; i++)
            {
                char temp = cards[i];
                if ((cards[i] - 'A' > 4 || char.IsDigit(cards[i])) && cards[i] != 'J')
                {
                    cards[i] = (char)('A' + counter);
                    counter++;
                }
                else
                {
                    continue;
                }

                for (int j = i + 1; j < cards.Length; j++)
                {
                    if (temp == cards[j])
                    {
                        cards[j] = cards[i];
                    }
                }
            }
            string cardsString = "";
            foreach (char c in cards)
            {
                cardsString += c;
            }
            int[] charNums = GetAmountOfChars(cardsString);
            int JokerValue = MaxFinder(cardsString);
            charNums[JokerValue] += charNums[charNums.Length - 1];
            if (!cardsString.Contains('B') || charNums[charNums.Length - 1] == 5)
            {
                hand.SetType(HandType.FiveOfAKind);
            }
            else if (!cardsString.Contains('C'))
            {
                if (charNums[0] == 4 || charNums[1] == 4)
                {
                    hand.SetType(HandType.FourOfAKind);
                    return;
                }
                hand.SetType(HandType.FullHouse);
                return;
            }
            else if (!cardsString.Contains('D'))
            {
                if (charNums[0] == 2 || charNums[1] == 2 || charNums[2] == 2)
                {
                    hand.SetType(HandType.TwoPair);
                    return;
                }
                hand.SetType(HandType.ThreeOfAKind);
                return;
            }
            else if (cardsString.Contains('E'))
            {
                hand.SetType(HandType.HighCard);
                return;
            }
            else if (cardsString.Contains('D'))
            {
                hand.SetType(HandType.OnePair);
                return;
            }


        }
        static int MaxFinder(string input)
        {
            Day7 day7 = new Day7();
            int[] inputInt = day7.GetAmountOfChars(input);
            int highestIndex = 0;
            for (int i = 0; i < inputInt.Length - 1; i++)
            {
                if (inputInt[i] > inputInt[highestIndex]) { highestIndex = i; }
            }
            return highestIndex;
        }
    }
}
