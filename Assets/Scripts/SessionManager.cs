using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField]
    private Player[] players;
    [SerializeField]
    private CommunityCards communityCards;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Player player in players)
        {
            player.AddCards(2);
        }

        communityCards.AddCards(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
