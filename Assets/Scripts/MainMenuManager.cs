using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour {

    public string newGameSceneName;

    [Header("Options Panel")]
    public GameObject optionPanel;
    public GameObject mainGamePanel;
    public GameObject infoPanel;

    [Header("Buttons")]
    public Button startGameButton;
    public Button optionButton;
    public Button infoButton;
    public Button quitButton;
    public AudioMixer audioMixer;

    public Resolution[] resolutionArray;
    public TMP_Dropdown resolutionDropDown;
    

    private void Start()
    {
        resolutionArray = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        
        int currentResolution = 0;
        for (int i = 0; i < resolutionArray.Length; i++)
        {
            string option = resolutionArray[i].width + "x" + resolutionArray[i].height;
            options.Add(option);

            if(resolutionArray[i].width == Screen.currentResolution.width &&
                resolutionArray[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolution;
        resolutionDropDown.RefreshShownValue();

        optionPanel.SetActive(false);
        mainGamePanel.SetActive(true);
        infoPanel.SetActive(false);
        
        startGameButton.onClick.AddListener(() => {
            StartGame();
        });

        optionButton.onClick.AddListener(() => {
            OpenOptions();
        });

        infoButton.onClick.AddListener(() => {
            OpenInfoPanel();
        });

        quitButton.onClick.AddListener(() => {
            Quit();
        });
    }
    public void OpenOptions()
    {
        optionPanel.SetActive(true);
        mainGamePanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void OpenInfoPanel()
    {
        optionPanel.SetActive(false);
        mainGamePanel.SetActive(false);
        infoPanel.SetActive(true);
    }

    public void BackToMenuPanel()
    {
        optionPanel.SetActive(false);
        mainGamePanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(newGameSceneName))
            SceneManager.LoadScene(newGameSceneName);
        else
            Debug.Log("Please write a scene name in the 'newGameSceneName' field of the Main Menu Script and don't forget to " +
                "add that scene in the Build Settings!");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetVolume(float amount)
    {
        audioMixer.SetFloat("Volume", amount);
    }

    public void SetQuality(int qualityAmount)
    {
        QualitySettings.SetQualityLevel(qualityAmount);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutionArray[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
