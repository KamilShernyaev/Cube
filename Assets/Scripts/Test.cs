using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject _doorWindow;

    [SerializeField] private MeshRenderer _sphereMeshRenderer;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _greenMaterial;

    private bool _isOpen;

    private bool _isColorRed = true;

   [SerializeField] private TextMeshProUGUI _codeText;
   string _codeTextValue = "";
   public string sageCode;
//    public GameObject CodePanel;

   private bool _isDoorOpened = false;
    
    private void Update() 
    {
        _codeText.text = _codeTextValue;

        if(_codeTextValue == sageCode)
        {
            OpenDoorButton();
        }

        if(_codeTextValue.Length >= 6 )
        {
            _codeTextValue = "";
        }


    }

    // private void OnTriggerEnter(Collider other) 
    // {
    //     if(other.tag == "Player")
    //     {
    //         _isAtDoor = true;
    //     }    
    // }

    // private void OnTriggerExit(Collider other) 
    // {
    //     _isAtDoor = false;
    //     CodePanel.SetActive(false);
    // }

    public void AddDigit(string digit)
    {
        _codeTextValue += digit;
    }
    
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
