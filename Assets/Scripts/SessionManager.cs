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

    public static SessionManager Instance()
    {
        return instance;
    }

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
            communityCards.AddCards(3);
            foreach (Player player in players)
                player.AddCards(2);
        }
        // debugging hand created players
        else
        {
            foreach(GameObject debugPlayer in GameObject.FindGameObjectsWithTag("Player"))
                players.Add(debugPlayer.GetComponent<Player>());
        }

        foreach(Player player in FindWinningHand())
            player.SetWinnerGraphicsOn(true);
    }

    private List<Player> FindWinningHand()
    {
        // set up hand data for each player
        foreach (Player player in players)
            player.DetermineHand(communityCards.GetCards());

        // determine winner
        List<Player> winningPlayers = new List<Player> { players[0] };
        for (int i = 1; i < players.Count; i++)
        {
            foreach (Player currentWinner in winningPlayers)
                winningPlayers = CardInfo.FindBestHand(currentWinner, players[i]);

            // -> CONTINUE HERE
            // running into a very specific issue in the configuration setup in debug rn.
            // if there's a tie following by a loser, only the more recent player in the tie will stay as a winner
            // the original tie holder will be overwritten. need to find a way to keep the tie holders together or something
        }
        return winningPlayers;
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