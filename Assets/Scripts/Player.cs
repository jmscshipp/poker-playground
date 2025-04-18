using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField]
    private bool isCommunityArea = false;
    [SerializeField]
    private Transform cardParent;
    [SerializeField]
    private TextMeshProUGUI winnerText;
    [SerializeField]
    private Image winnerOutlineImage;
    [SerializeField]
    private TextMeshProUGUI handText;
    public List<Card> hand = new List<Card>();

    private CardInfo.PlayerHandData handData;

    // Start is called before the first frame update
    void Start()
    {
        if (!isCommunityArea)
            SetWinnerGraphicsOn(false);

        // if debugging, add cards setup in editor instead of having them drawn from deck
        if (SessionManager.debugMode)
        {
            foreach (Card card in cardParent.GetComponentsInChildren<Card>())
                hand.Add(card);
        }
    }

    public void DetermineHand(List<Card> communityCards)
    {
        List<Card> completeHand = new List<Card>(hand);
        completeHand.AddRange(communityCards);

        // !!! come back here when implementing 7 card game and save only the cards being used in the 
        // best hand to handData.hand here:
        handData.hand = completeHand.OrderBy(i => i.Rank).ToList();
        handData.hand.Reverse();
        CardInfo.Hands handType = CardInfo.FindHandType(completeHand);

        handData.handType = handType;
        SetHandText(CardInfo.GetFormattedHandName(handType));
    }

    public void ClearHand()
    {
        SetWinnerGraphicsOn(false);

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

    public CardInfo.PlayerHandData GetHandData() { return handData; }

    public void SetHandText(string text)
    {
        if (isCommunityArea)
        {
            Debug.LogError("Accidentally tried to assign hand text for community area!!");
            return;
        }
        handText.text = text;
    }

    // call to turn on or off winner graphics when a hand is played
    public void SetWinnerGraphicsOn(bool on)
    {
        if (isCommunityArea)
        {
            Debug.LogError("Accidentally tried to set winner graphics for community area!!");
            return;
        }

        if (on)
        {
            winnerText.color = new Color(winnerText.color.r, winnerText.color.g, winnerText.color.b, 100f);
            winnerOutlineImage.enabled = true;
        }
        else
        {
            winnerText.color = new Color(winnerText.color.r, winnerText.color.g, winnerText.color.b, 0f);
            winnerOutlineImage.enabled = false;
        }
    }
}
