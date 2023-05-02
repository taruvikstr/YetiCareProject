using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{

    public GameObject creditScreen;

    private void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound("MainAmbience");
      
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ToMinigame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleCredits()
    {
        if (creditScreen.activeSelf)
        {
            creditScreen.SetActive(false);
        }
        else
        {
            creditScreen.SetActive(true);
        }
    }
}
