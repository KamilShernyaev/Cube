using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sedative : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<CharacterController>(out CharacterController playerController))
        {
            playerController.CalmDown();
            Destroy(gameObject);
        }
    }
}
