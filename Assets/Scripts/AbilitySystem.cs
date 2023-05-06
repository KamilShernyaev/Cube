using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : ScriptableObject
{
    public new string name;

    public virtual void Activate(GameObject parent) { }
    public virtual void BeginCooldown(GameObject parent) { }
}