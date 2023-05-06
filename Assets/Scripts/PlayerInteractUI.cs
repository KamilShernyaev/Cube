using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;
    private CharacterController playerController;

    private void Awake() 
    {
        GameManager.Instance.OnChangePlayer += GameManager_OnChangePlayer;
    }

    private void GameManager_OnChangePlayer(object sender, CharacterController e)
    {
        playerController = e;
    }

    private void Update() 
    {
        if(playerController.GetInteractableObject() != null)
        {
            Show(playerController.GetInteractableObject());
        }
        else
        {
            Hide();
        }
    }

    private void Show(IInteractable interactable)
    {
        containerGameObject.SetActive(true);
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
    }
}
