using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Fish_ButtonManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject gameStarter; // The object that has the script and function for starting the game based on given parameters.
    private int fishAmountValue;
    private int playerAmountValue;
    private int timerValue;
    private int patternAmountValue;

    private void Awake() // Set values to defaults. Remember to set sliders to these values as well.
    {
        fishAmountValue = 10;
        playerAmountValue = 1;
        timerValue = 60;
        patternAmountValue = 4;
    }


    public void UpdateDifficulty(Slider slider)
    {
        fishAmountValue = (int)slider.value;
    }

    public void UpdatePlayerAmount(Slider slider)
    {
        playerAmountValue = (int)slider.value;
    }

    public void UpdateGameSpeed(Slider slider)
    {
        timerValue = (int)slider.value;
    }

    public void UpdatePatternAmount(Slider slider)
    {
        patternAmountValue = (int)slider.value;
    }


    public void ReturnToMainScreen()
    {
        SceneManager.LoadScene("Main_Farm");
    }

    public void ActivateGame()
    {
        startScreen.SetActive(false); // Disable and hide the starting screen.
        gameStarter.GetComponent<Fish_GameManager>().StartGame(timerValue, playerAmountValue, fishAmountValue, patternAmountValue);
    }

    public void ReturnToSettingScreen()
    {
        endScreen.SetActive(false);
        startScreen.SetActive(true);
    }
}
