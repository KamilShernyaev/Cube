using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorCharacterUI : MonoBehaviour
{
    [SerializeField] private PlayerController selectedCharacter;

    private void Awake() {
        GetComponentInChildren<Button>().onClick.AddListener(() => {
            GameManager.Instance.SetPlayerController(selectedCharacter);
        });
    }
}
