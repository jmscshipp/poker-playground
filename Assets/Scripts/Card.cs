using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Sprite sprite;
    private CardInfo.Suits suit;
    private CardInfo.Ranks rank;

    public CardInfo.Suits Suit
    {
        get { return suit; }
        set { suit = value; }
    }

    public CardInfo.Ranks Rank
    {
        get { return rank; }
        set { rank = value; }
    }

    public Sprite Sprite
    {
        get { return sprite; }
        set { sprite = value; }
    }
}
