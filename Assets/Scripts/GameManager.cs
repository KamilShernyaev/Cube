using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public event EventHandler<PlayerController> OnChangePlayer;
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private PlayerController selectedPlayer;
    [SerializeField] private List< PlayerController> playerControllerArray;

    private void Awake()
    {
        Instance = this;
        if (selectedPlayer == null)
        {
            selectedPlayer = playerControllerArray[0];
            selectedPlayer.SetIsSelectedCharacter(true);
        }
    }
    private void Start() 
    {
        OnChangePlayer?.Invoke(this, selectedPlayer);
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

    public void SetFatigueCharacter(PlayerController prevPlayerController)
    {
        prevPlayerController.SetIsSelectedCharacter(false);
        
        foreach (PlayerController nextPlayerController in playerControllerArray)
        {
            if(!nextPlayerController.GetRecreation())
            {
                SetPlayerController(nextPlayerController);
                selectedPlayer.SetIsSelectedCharacter(true);
                break;
            }
        } 
    }

    public void SetPlayerController(PlayerController playerController)
    {   
        selectedPlayer = playerController;
        OnChangePlayer?.Invoke(this, selectedPlayer);
    }

    public void RemovePlayerController(PlayerController playerController)
    {
        playerControllerArray.Remove(playerController);
    }

    public PlayerController GetSelectedPlayer() {return selectedPlayer;}
}
