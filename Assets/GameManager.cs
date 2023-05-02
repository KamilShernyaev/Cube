using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private PlayerController selectedPlayer;
    [SerializeField] private PlayerController[] playerControllerArray;
    private void Awake()
    {
        Instance = this;
        if (selectedPlayer == null)
        {
            selectedPlayer = playerControllerArray[0];
        }
        if(playerControllerArray == null)
        {
            playerControllerArray = FindObjectsOfType<PlayerController>();
        }
    }
    private void Update() 
    {
        foreach (PlayerController playerController in playerControllerArray)
        {
            if(playerController != selectedPlayer)
            {
                playerController.enabled = false;
            }
            else
            {
                selectedPlayer.enabled = true;
            }
        }
    }

    public PlayerController SetPlayerController(PlayerController playerController) => selectedPlayer = playerController;
}
