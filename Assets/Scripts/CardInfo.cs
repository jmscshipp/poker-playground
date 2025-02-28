using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardInfo
{
    public static string cardDataDirectory = "Cards"; // Assets/Resources/Cards

    public enum Suits
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Ranks
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public enum Hands
    {
        StraightFlush,
        Quads,
        FullHouse,
        Flush,
        Straight,
        Threes,
        TwoPair,
        Pair,
        HighCard
    }

    // helper function to get rid of duplicate ranks in hand
    private static List<Card> GetDistinctRanks(List<Card> cards)
    {
        List<Card> distinctRanks = new List<Card>();
        foreach (Card card in cards)
        {
            if (distinctRanks.Find(x => x.Rank == card.Rank) == null)
                distinctRanks.Add(card);
        }
        return distinctRanks;
    }

    public static Hands FindBestHand(List<Card> cards) // for now assuming only 5 card hands are passed...
    {
        // create histogram (list of # of each rank in hand)

        List<int> histogram = new List<int>();
        foreach(Card uniqueCard in GetDistinctRanks(cards))
        {
            int cardCount = 0;
            foreach (Card card in cards)
            {
                if (card.Rank == uniqueCard.Rank)
                    cardCount++;
            }
            histogram.Add(cardCount);
        }
        histogram.Sort();
        histogram.Reverse();

        // at this point we have the histogram sorted from highest to lowest
        // check for rank-based hands:

        foreach(int num in histogram)
            Debug.Log(num);

        if (histogram.SequenceEqual(new List<int> { 4, 1 }))
        {
            return Hands.Quads;
        }
        else if (histogram.SequenceEqual(new List<int> { 3, 2 }))
        {
            return Hands.FullHouse;
        }
        else if (histogram.SequenceEqual(new List<int> { 3, 1, 1 }))
        {
            return Hands.Threes;
        }
        else if (histogram.SequenceEqual(new List<int> { 2, 2, 1 }))
        {
            return Hands.TwoPair;
        }
        else if (histogram.Count == 4)
        {
            return Hands.Pair;
        }
        //else // if (histogram == new List<int> { 1, 1, 1, 1, 1 })
        //{
        //    bestHand = Hands.HighCard;
        //}

        Hands bestHand;
        bool flush = true;
        bool straight = false;

        // check if hand is a flush
        for (int i = 0; i < cards.Count - 1; i++)
       {
            if (cards[i].Suit != cards[i + 1].Suit)
            {
                flush = false;
                break;
            }
       }

        // check if hand is a straight
        List<Card> sortedCards = cards.OrderBy(x => x.Rank).Reverse().ToList();
        int difference = sortedCards[0].Rank - sortedCards[sortedCards.Count - 1].Rank;
        if (difference == 4 || sortedCards[0].Rank == Ranks.Ace && sortedCards[1].Rank == Ranks.Five)
            straight = true;


        Debug.Log("straight: " + straight);
        // NEXT STEPS:
        // test straight code for edge cases- especially with aces and 'wheels'.... using debug setup

        return Hands.TwoPair;
    }
}
