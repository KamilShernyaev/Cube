using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiredUI : MonoBehaviour
{
    [SerializeField] private Image tiredLevelImage;

    private void Update() {
        tiredLevelImage.fillAmount = GameManager.Instance.GetSelectedPlayer().GetTiredLevel()/10;
    }
}
