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
            selectedPlayer.SetIsSelectedCharacter(true);
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
                playerController.SetIsSelectedCharacter(false);
            }
            else
            {
                selectedPlayer.SetIsSelectedCharacter(true);
            }
        }
    }

    public void SetFatigueCharacter(PlayerController playerController)
    {
        playerController.SetIsSelectedCharacter(false);
        for (int i = 0; i < playerControllerArray.Length; i++)
        {
            if(playerControllerArray[i].GetRecreation() != true)
            {
                SetPlayerController(playerControllerArray[i]);
                selectedPlayer.SetIsSelectedCharacter(true);
                break;
            }
        }
    }

    public void SetPlayerController(PlayerController playerController)
    {   
        selectedPlayer = playerController;
    }

    public PlayerController GetSelectedPlayer() {return selectedPlayer;}
}
