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

    // Start is called before the first frame update
    void Start()
    {
        // copying cards in resources folder to our deck
        CardData[] cardData = Resources.LoadAll<CardData>(CardInfo.cardDataDirectory);
        
        foreach(CardData cardDatum in cardData)
        {
            Card card = Instantiate(cardPrefab, cardParent).GetComponent<Card>();
            card.Sprite = cardDatum.Sprite;
            card.Suit = cardDatum.Suit;
            card.Rank = cardDatum.Rank;

            card.gameObject.GetComponent<Image>().sprite = card.Sprite;
            cards.Add(card);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shuffle()
    {

    }
}
