using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsanityAbility : ActiveAbility
{
    public override void Activate()
    {
        foreach(CharacterController character in GameManager.Instance.GetCharacterControllerList())
        {
            character.CalmDown(1f);
        }

        GameManager.Instance.GetSelectedPlayer().GoCrazy(1f);

        BeginCooldown();
    }

    public override void BeginCooldown()
    {
        base.BeginCooldown();
        cooldownTime += 5f;
    }
}
