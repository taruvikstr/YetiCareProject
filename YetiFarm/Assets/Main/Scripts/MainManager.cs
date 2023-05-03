using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{

    public GameObject creditScreen;
    public GameObject highlights;

    private void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound("MainAmbience");
      
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        StartCoroutine(blinkHighlights());
        
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

    public IEnumerator blinkHighlights()
    {
        while(true)
        {
            yield return new WaitForSeconds(5.0f);
            highlights.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            highlights.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            highlights.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            highlights.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            highlights.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            highlights.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
