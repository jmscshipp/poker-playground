using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddRemovePlayersUI : MonoBehaviour
{
    private PlayersUI playersUI;

    public void AssignPlayersUI (PlayersUI newPlayersUI)
    {
        playersUI = newPlayersUI;
    }

    public void OnClickAddPlayer()
    {
        playersUI.AddPlayer();
    }

    public void OnClickRemovePlayer()
    {
        playersUI.RemovePlayer();
    }
}
