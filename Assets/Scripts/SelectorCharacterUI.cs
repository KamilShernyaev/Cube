using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorCharacterUI : MonoBehaviour
{
    [SerializeField] private CharacterController selectedCharacter;
    [SerializeField] private Button changeCharacter;

    private void Start() {
        GameManager.Instance.OnRemovePlayer += GameManager_OnRemovePlayer;
        changeCharacter.onClick.AddListener(() => {
            GameManager.Instance.SetPlayerController(selectedCharacter);
        });
    }

    private void GameManager_OnRemovePlayer(object sender, CharacterController e)
    {
        if (e = this.selectedCharacter)
        {
            changeCharacter.interactable = false;
        }
    }
}
