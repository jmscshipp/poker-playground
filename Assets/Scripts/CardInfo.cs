using System.Collections;
using System.Collections.Generic;
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
}
