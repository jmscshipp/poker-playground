using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField]
    private Player[] players;
    [SerializeField]
    private Player communityCards;

    // set to true in editor to manually setup debug cards instead of randomly drawing them from the deck
    [SerializeField]
    private bool setToDebugMode = false;
    public static bool debugMode = false;


    private void Awake()
    {
        // this feels kinda dumb
        if (setToDebugMode)
            debugMode = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayRound();
    }

    private void PlayRound()
    {
        if (debugMode == false)
        {
            Deck.Instance().Shuffle();

            communityCards.ClearHand();
            communityCards.AddCards(3);

            foreach (Player player in players)
            {
                player.ClearHand();
                player.AddCards(2);
            }
        }
        FindWinningHand();
    }

    private Player FindWinningHand()
    {
        foreach (Player player in players)
        {
            List<Card> hand = communityCards.GetCards();
            hand.AddRange(player.GetCards());
            player.SetHandText(CardInfo.GetFormattedHandName(CardInfo.FindBestHand(hand)));
        }
        return players[0];
    }
}