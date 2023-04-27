using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound("MainAmbience");
      
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        
    }
    public void ToMinigame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
