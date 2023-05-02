using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _doorScreen;

    [SerializeField] private string interactText;

   

    private void Start() 
    {
        _doorScreen.SetActive(false);    
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Door window is Open");
        Time.timeScale = 0;
        _doorScreen.SetActive(true);
    }
}