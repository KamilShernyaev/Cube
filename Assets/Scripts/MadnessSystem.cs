using System;
using System.Collections.Generic;
using UnityEngine;

public class MadnessSystem 
{
    public event EventHandler OnInsanityChanged;
    public event EventHandler OnGoingCrazy;
    public event EventHandler OnCalmDown;
    public event EventHandler OnBecameInsane;

    private float insantyMaxLevel;
    private float insantyLevel;

    public MadnessSystem(float insantyMaxLevel) {
        this.insantyMaxLevel = insantyMaxLevel;
        insantyLevel = 0;
    }

    public float GetInsantyLavel() {
        return insantyLevel;
    }

    public float GetInsantyMaxLevel() {
        return insantyMaxLevel;
    }

    public float GetInsantyNormalized() {
        return insantyLevel / insantyMaxLevel;
    }

    public void GoCrazy(float amount) 
    {
        insantyLevel += amount;

        OnInsanityChanged?.Invoke(this, EventArgs.Empty);
        OnGoingCrazy?.Invoke(this, EventArgs.Empty);

        if (insantyLevel >= insantyMaxLevel) {
            insantyLevel = 0;
            BecameInsane();
        }
    }

    public void BecameInsane() {
        OnBecameInsane?.Invoke(this, EventArgs.Empty);
    }

    public void CalmDown(float amount) 
    {
        insantyLevel -= amount;
        if (insantyLevel <= 0) 
        {
            insantyLevel = 0;
        }
        OnInsanityChanged?.Invoke(this, EventArgs.Empty);
        OnCalmDown?.Invoke(this, EventArgs.Empty);
    }

    public static bool TryGetMadnessSystem(GameObject getMadnessSystemPlayerController, out MadnessSystem madnessSystem, bool logErrors = false) {
        madnessSystem = null;

        if (getMadnessSystemPlayerController != null) {
            if (getMadnessSystemPlayerController.TryGetComponent(out IGetMadnessSystem getMadnessSystem)) {
                madnessSystem = getMadnessSystem.GetMadnessSystem();
                if (madnessSystem != null) {
                    return true;
                } else {
                    if (logErrors) {
                        Debug.LogError($"Got MadnessSystem from object but MadnessSystem is null! Should it have been created? Maybe you have an issue with the order of operations.");
                    }
                    return false;
                }
            } else {
                if (logErrors) {
                    Debug.LogError($"Referenced PlayerController '{getMadnessSystemPlayerController}' does not have a script that implements IGetMadnessSystem!");
                }
                return false;
            }
        } else {
            // No reference assigned
            if (logErrors) {
                Debug.LogError($"You need to assign the field 'getMadnessSystemPlayerController'!");
            }
            return false;
        }
    }

}
