using System;
using UnityEngine;
using UnityEngine.UI;

public class TiredUI : MonoBehaviour
{
    [SerializeField] private Image tiredLevelImage;
    [SerializeField] private CharacterController playerController;

    private void Update() {
        tiredLevelImage.fillAmount = playerController.GetTiredLevel()/10;
    }
}
