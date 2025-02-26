using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField]
    private Player[] players;
    [SerializeField]
    private CommunityCards communityCards;

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
        if (debugMode == false)
        {
            foreach (Player player in players)
            {
                player.AddCards(2);
            }

            communityCards.AddCards(3);
        }

        FindWinningHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Player FindWinningHand()
    {
        List<Card> hand = communityCards.GetCards();
        hand.AddRange(players[0].GetCards());
        CardInfo.FindBestHand(hand);
        //foreach (Player player in players)
        //{
        //    // find best hand for each player and sort
        //}
        return players[0];
    }
}
