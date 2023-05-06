using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public event EventHandler<CharacterController> OnChangePlayer;
    public event EventHandler<CharacterController> OnRemovePlayer;
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private CharacterController selectedPlayer;
    [SerializeField] private List< CharacterController> playerControllerArray;

    private void Awake()
    {
        Instance = this;
       if (playerControllerArray.Count > 0)
        {
            if (selectedPlayer == null)
            {
                selectedPlayer = playerControllerArray[0];
                selectedPlayer.SetIsSelectedCharacter(true);
                OnChangePlayer?.Invoke(this, selectedPlayer);
            }
        }
    }
    private void Start() 
    {
        OnChangePlayer?.Invoke(this, selectedPlayer);
    }
    private void Update() 
    {
        foreach (CharacterController playerController in playerControllerArray)
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

    public void SetFatigueCharacter(CharacterController prevPlayerController)
    {
        prevPlayerController.SetIsSelectedCharacter(false);
        
        foreach (CharacterController nextPlayerController in playerControllerArray)
        {
            if(!nextPlayerController.GetRecreation())
            {
                SetPlayerController(nextPlayerController);
                selectedPlayer.SetIsSelectedCharacter(true);
                break;
            }
        } 
    }

    public void SetPlayerController(CharacterController playerController)
    {   
        selectedPlayer = playerController;
        OnChangePlayer?.Invoke(this, selectedPlayer);
    }

    public void RemovePlayerController(CharacterController playerController)
    {
        playerControllerArray.Remove(playerController);
        OnRemovePlayer?.Invoke(this, playerController);
    }

    public CharacterController GetSelectedPlayer() {return selectedPlayer;}
}
