using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject _doorWindow;
    
    
    public void OpenDoorButton()
    {
        Debug.Log("Door opened");
        ClosedDoorWindow();
    }

    public void CloseWindowButton()
    {
        ClosedDoorWindow();
    }

    private void ClosedDoorWindow()
    {
        _doorWindow.SetActive(false);
        Time.timeScale = 1;
    }
}
