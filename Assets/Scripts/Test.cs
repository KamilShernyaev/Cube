using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject _doorWindow;

    [SerializeField] private MeshRenderer _sphereMeshRenderer;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _greenMaterial;

    private bool _isOpen;

    private bool _isColorRed = true;
    
    
    
    public void OpenDoorButton()
    {
        Debug.Log("Door opened");
        PushButton();
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

     public void ToggleDoor()
    {
        _isOpen = !_isOpen;
    }

    private void SetColorGreen()
    {
        _sphereMeshRenderer.material = _greenMaterial;
    }

    private void SetColorRed()
    {
        _sphereMeshRenderer.material = _redMaterial;
    }

    private void ToggleColor()
    {
        _isColorRed = !_isColorRed;
        if(_isColorRed)
        {
            SetColorRed();
        }
        else
        {
            SetColorGreen();
        }
    }

    public void PushButton()
    {
        ToggleColor();
    }
}
