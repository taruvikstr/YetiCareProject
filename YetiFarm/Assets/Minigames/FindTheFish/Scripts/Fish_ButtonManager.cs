using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Fish_ButtonManager : MonoBehaviour
{   
    [SerializeField] private GameObject startScreen, endScreen, gameStarter;
    [SerializeField] private TMP_Text playersSlider, fishSlider, patternSlider, timeSlider;
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

        UpdateSliderHandleValues();
    }

    private void UpdateSliderHandleValues()
    {
        playersSlider.text = playerAmountValue.ToString();
        fishSlider.text = fishAmountValue.ToString();
        patternSlider.text = patternAmountValue.ToString();
        timeSlider.text = timerValue.ToString();
    }


    public void UpdateDifficulty(Slider slider)
    {
        fishAmountValue = (int)slider.value;
        fishSlider.text = fishAmountValue.ToString();
        
    }

    public void UpdatePlayerAmount(Slider slider)
    {
        playerAmountValue = (int)slider.value;
        playersSlider.text = playerAmountValue.ToString();
    }

    public void UpdateGameSpeed(Slider slider)
    {
        timerValue = (int)slider.value * 10;
        timeSlider.text = timerValue.ToString();
    }

    public void UpdatePatternAmount(Slider slider)
    {
        patternAmountValue = (int)slider.value;
        patternSlider.text = patternAmountValue.ToString();
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
