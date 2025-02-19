using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CardData", order = 1)]
public class CardData : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private CardInfo.Suits suit;
    [SerializeField]
    private CardInfo.Ranks rank;

    public Sprite Sprite
    {
        get { return sprite; }
    }

    public CardInfo.Suits Suit
    {
        get { return suit; }
    }

    public CardInfo.Ranks Rank
    {
        get { return rank; }
    }
}
