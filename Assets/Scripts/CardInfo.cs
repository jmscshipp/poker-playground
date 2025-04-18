using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardInfo
{
    public static string cardDataDirectory = "Cards"; // Assets/Resources/Cards

    public struct PlayerHandData
    {
        public List<Card> hand;
        public Hands handType;
    }

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

    public static string GetFormattedHandName(Hands hand)
    {
        switch (hand)
        {
            case Hands.StraightFlush:
                return "straight flush";
            case Hands.Quads:
                return "four of a kind";
            case Hands.FullHouse:
                return "full house";
            case Hands.Flush:
                return "flush";
            case Hands.Straight:
                return "straight";
            case Hands.Threes:
                return "three of a kind";
            case Hands.TwoPair:
                return "two pair";
            case Hands.Pair:
                return "pair";
            default:
                return "high card";
        }
    }

    // histogram helper function to get rid of duplicate ranks in hand
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

    // returns the best possible hand from the collection of cards given
    public static Hands FindHandType(List<Card> cards) // for now assuming only 5 card hands are passed...
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

        // final evaluation
        if (flush && straight)
            return Hands.StraightFlush;
        else if (flush)
            return Hands.Flush;
        else if (straight)
            return Hands.Straight;
        else
            return Hands.HighCard;
    }

    public static List<Player> FindBestHand(List<Player> currentWinners, Player contender)
    {
        PlayerHandData currentWinnerData = currentWinners[0].GetHandData();
        PlayerHandData contenderData = contender.GetHandData();

        // same hand type, need to determine winner more specifically
        if (currentWinnerData.handType == contenderData.handType)
        {
            // compare hands card by card
            for (int i = 0; i < currentWinnerData.hand.Count; i++)
            {
                if (currentWinnerData.hand[i].Rank > contenderData.hand[i].Rank)
                    return currentWinners;
                else if (currentWinnerData.hand[i].Rank < contenderData.hand[i].Rank)
                    return new List<Player> { contender };
            }
            // tie, add contender to winner list
            List<Player> combinedWinners = new List<Player>(currentWinners);
            combinedWinners.Add(contender);
            return combinedWinners;
        }
        // player 1 hand type is better
        else if (currentWinnerData.handType < contenderData.handType)
            return currentWinners;
        // player 2 hand type is better
        else
            return new List<Player> { contender };
    }
}
