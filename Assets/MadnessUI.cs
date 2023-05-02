using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MadnessUI : MonoBehaviour
{
    private GameObject[] madnessContainers;
    private Image[] madnessFills;

    public Transform madnessParent;
    public GameObject madnessContainerPrefab;

    private void Start()
    {
        madnessContainers = new GameObject[GameManager.Instance.GetSelectedPlayer().GetMadnessLevel()];
        madnessFills = new Image[GameManager.Instance.GetSelectedPlayer().GetMaxMadnessLevel()];

        GameManager.Instance.GetSelectedPlayer().OnMadnessChangedCallback += UpdateMadnessUI;
        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        SetMadnessContainers();
        SetFilledMadnessPoint();
    }

    private void UpdateMadnessUI(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

     void SetMadnessContainers()
    {
        for (int i = 0; i < madnessContainers.Length; i++)
        {
            if (i < GameManager.Instance.GetSelectedPlayer().GetMaxMadnessLevel())
            {
                madnessContainers[i].SetActive(true);
            }
            else
            {
                madnessContainers[i].SetActive(false);
            }
        }
    }

    void SetFilledMadnessPoint()
    {
        for (int i = 0; i < madnessFills.Length; i++)
        {
            if (i < GameManager.Instance.GetSelectedPlayer().GetTiredLevel())
            {
                madnessFills[i].fillAmount = 1;
            }
            else
            {
                madnessFills[i].fillAmount = 0;
            }
        }

        if (GameManager.Instance.GetSelectedPlayer().GetTiredLevel() % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(GameManager.Instance.GetSelectedPlayer().GetTiredLevel());
            madnessFills[lastPos].fillAmount = GameManager.Instance.GetSelectedPlayer().GetTiredLevel() % 1;
        }
    }

    void InstantiateHeartContainers()
    {
        for (int i = 0; i < GameManager.Instance.GetSelectedPlayer().GetMaxMadnessLevel(); i++)
        {
            GameObject temp = Instantiate(madnessContainerPrefab);
            temp.transform.SetParent(madnessParent, false);
            madnessContainers[i] = temp;
            madnessFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}
