using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private bool isCommunityArea = false;
    [SerializeField]
    private Transform cardParent;
    [SerializeField]
    private TextMeshProUGUI handText;
    public List<Card> hand = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        // if debugging, add cards setup in editor instead of having them drawn from deck
        if (SessionManager.debugMode)
        {
            foreach (Card card in cardParent.GetComponentsInChildren<Card>())
                hand.Add(card);
        }
    }

    public void ClearHand()
    {
        int initialHandCount = hand.Count;
        for (int i = 0; i < initialHandCount; i++)
        {
            Card card = hand[0]; // 
            hand.Remove(card);
            Deck.Instance().ReturnToDeck(card);
        }
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

    public List<Card> GetCards () { return hand; }

    public void SetHandText(string text)
    {
        if (isCommunityArea)
        {
            Debug.LogError("Accidentally tried to assign hand text for community area!!");
            return;
        }
        handText.text = text;
    }
}
