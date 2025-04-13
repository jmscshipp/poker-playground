using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField]
    private static List<Player> players = new List<Player>();
    [SerializeField]
    private Player communityCards;

    // set to true in editor to manually setup debug cards instead of randomly drawing them from the deck
    [SerializeField]
    private bool setToDebugMode = false;
    public static bool debugMode = false;

    private static SessionManager instance;

    private void Awake()
    {
        // setting up singleton
        if (instance != null && instance != this)
            Destroy(this);
        instance = this;

        // this feels kinda dumb
        if (setToDebugMode)
            debugMode = true;
    }

    public static SessionManager Instance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayRound();
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
            // clear last round
            communityCards.ClearHand();
            foreach (Player player in players)
                player.ClearHand();

            // shuffle
            Deck.Instance().Shuffle();

            // re-deal
            communityCards.AddCards(1);
            foreach (Player player in players)
                player.AddCards(2);
        }
        else
        {
            // setup debug players
            foreach (GameObject debugPlayer in GameObject.FindGameObjectsWithTag("Player"))
                players.Add(debugPlayer.GetComponent<Player>());
        }

        List<Player> winningPlayers = FindWinningHand();
        //foreach (Player player in winningPlayers)
        //    player.SetWinnerGraphicsOn(true);

        // here mess with statistics for winning hand too
    }

    private void DebugPrintCard(Card card)
    {
        Debug.Log(card.Rank + ", " + card.Suit);
    }

    private List<Player> FindWinningHand()
    {
        // print every 2 card combination of the first player's cards
        List<Card> cards = players[0].hand;
        for (int i = 0; i < cards.Count; i++)
        {
            DebugPrintCard(cards[i]);
        }

        //foreach (Player player in players)
        //    player.DetermineHand(communityCards.GetCards());
        //
        //List<Player> winners = new List<Player>{ players[0] };
        //for (int i = 1; i < players.Count; i++)
        //    winners = CardInfo.FindBestHand(winners, players[i]);

        //return winners;

        return players;
    }

    // set up to return player that was removed to playersUI to cleanup canvas objects
    public Player RemovePlayer()
    {
        if (players.Count == 1) 
            return null;

        Player playerToRemove = players[0];
        players.Remove(players[0]);
        return playerToRemove;
    }

    public void AddPlayer(Player player)
    {
        if (players.Count < 10)
            players.Add(player);
    }
}