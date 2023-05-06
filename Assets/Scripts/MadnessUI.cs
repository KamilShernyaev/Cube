using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MadnessUI : MonoBehaviour
{
    [SerializeField] private GameObject getMadnessSystemGameObject;
    [SerializeField] private Image image;
    private MadnessSystem madnessSystem;

    private void Awake() 
    {
        if (MadnessSystem.TryGetMadnessSystem(getMadnessSystemGameObject, out MadnessSystem madnessSystem)) 
        {
            SetMadnessSystem(madnessSystem);
        }
    }

    private void SetMadnessSystem(MadnessSystem madnessSystem)
    { 
        if (this.madnessSystem != null) {
                this.madnessSystem.OnInsanityChanged -= MadnessSystem_OnInsanityChanged;
            }
            this.madnessSystem = madnessSystem;

            UpdateMadnessBar();

            madnessSystem.OnInsanityChanged += MadnessSystem_OnInsanityChanged;
    }

    private void MadnessSystem_OnInsanityChanged(object sender, EventArgs e)
    {
        UpdateMadnessBar();
    }

    private void UpdateMadnessBar()
    {
         image.fillAmount = madnessSystem.GetInsantyNormalized();
    }
}
