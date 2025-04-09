using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayersUI : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField] 
    private GameObject addRemovePlayersPrefab;
    [SerializeField]
    private Transform playerParent;
    [SerializeField]
    private int startPlayers = 3; // how many players exist when the game begins

    private GameObject addRemovePlayersUI;

    private void Start()
    {
        if (!SessionManager.debugMode)
        {
            for (int i = 0; i < startPlayers; i++)
                AddPlayer();
        }
    }

    public void AddPlayer()
    {
        if (addRemovePlayersUI != null)
            Destroy(addRemovePlayersUI);

        GameObject newPlayer = Instantiate(playerPrefab);
        newPlayer.transform.SetParent(playerParent, false);
        SessionManager.Instance().AddPlayer(newPlayer.GetComponent<Player>());

        addRemovePlayersUI = Instantiate(addRemovePlayersPrefab);
        addRemovePlayersUI.transform.SetParent(playerParent.transform, false);
        addRemovePlayersUI.GetComponent<AddRemovePlayersUI>().AssignPlayersUI(this);
    }

    public void RemovePlayer()
    {
        // get the canvas objects of the UI being removed
        Player playerToRemove = SessionManager.Instance().RemovePlayer();
        // for now, just delete the player object... may want to change this in the future?
        if (playerToRemove != null)
        {
            playerToRemove.ClearHand();
            Destroy(playerToRemove.gameObject);
        }
    }
}
