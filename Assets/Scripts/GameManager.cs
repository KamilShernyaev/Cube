using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public event EventHandler<CharacterController> OnChangePlayer;
    public event EventHandler<CharacterController> OnRemovePlayer;
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private CharacterController selectedPlayer;
    [SerializeField] private List< CharacterController> playerControllerList;

    private void Awake()
    {
        Instance = this;
       if (playerControllerList.Count > 0)
        {
            if (selectedPlayer == null)
            {
                selectedPlayer = playerControllerList[0];
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
        foreach (CharacterController playerController in playerControllerList)
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
        
        foreach (CharacterController nextPlayerController in playerControllerList)
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
        playerControllerList.Remove(playerController);
        OnRemovePlayer?.Invoke(this, playerController);
    }

    public CharacterController GetSelectedPlayer() {return selectedPlayer;}
    public List<CharacterController> GetCharacterControllerList(){return playerControllerList;}
}
