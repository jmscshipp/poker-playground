using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// card that can be manually setup in the editor for testing hands!
public class DebugCard : Card
{
    [SerializeField]
    CardData cardData;

    // copy data to card object
    private void Start()
    {
        Sprite = cardData.Sprite;
        Suit = cardData.Suit;
        Rank = cardData.Rank;

        GetComponent<Image>().sprite = cardData.Sprite;
    }

}
