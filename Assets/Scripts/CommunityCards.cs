using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityCards : MonoBehaviour
{
    [SerializeField]
    private Transform cardParent;
    private List<Card> hand = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCards(int numOfCards)
    {
        for (int i = 0; i < numOfCards; i++)
        {
            Card newCard = Deck.Instance().Draw();
            newCard.GetComponent<RectTransform>().parent = cardParent;
            hand.Add(newCard);
        }
    }
}
