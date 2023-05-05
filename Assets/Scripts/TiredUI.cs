using System;
using UnityEngine;
using UnityEngine.UI;

public class TiredUI : MonoBehaviour
{
    [SerializeField] private Image tiredLevelImage;
    private PlayerController playerController;

    private void Awake() 
    {
        GameManager.Instance.OnChangePlayer += GameManager_OnChangePlayer;
    }

    private void GameManager_OnChangePlayer(object sender, PlayerController e)
    {
        playerController = e;
        Debug.Log(playerController);
    }

    private void Update() {
        tiredLevelImage.fillAmount = playerController.GetTiredLevel()/10;
    }
}
