using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    private List<Card> cards = new List<Card>();

    [SerializeField]
    private Transform cardParent;

    private int cardIndex = 0;

    private static Deck instance;

    private void Awake()
    {
        // setting up singleton
        if (instance != null && instance != this)
            Destroy(this);
        instance = this;

        // copying cards in resources folder to our deck
        CardData[] cardData = Resources.LoadAll<CardData>(CardInfo.cardDataDirectory);

        foreach (CardData cardDatum in cardData)
        {
            Card card = Instantiate(cardPrefab, cardParent).GetComponent<Card>();
            card.Sprite = cardDatum.Sprite;
            card.Suit = cardDatum.Suit;
            card.Rank = cardDatum.Rank;

            card.gameObject.GetComponent<Image>().sprite = card.Sprite;
            cards.Add(card);
        }

        Shuffle();
    }

    public static Deck Instance()
    {
        return instance;
    }

    // fisher - yates shuffle
    // https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
    public void Shuffle()
    {
        cardIndex = 0;

        for (int i = cards.Count -1; i >= 1; i--)
        {
            int indexToSwap = Random.Range(0, i);
            Card temp = cards[i];
            cards[i] = cards[indexToSwap];
            cards[indexToSwap] = temp;
        }
    }
    public Card Draw()
    {
        if (cardIndex >= cards.Count)
            Debug.LogError("END OF DECK REACHED!");

        return cards[cardIndex++];
    }
}
