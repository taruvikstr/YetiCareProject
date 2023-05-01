using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Fish_ButtonManager : MonoBehaviour
{   
    [SerializeField] private GameObject startScreen, endScreen, tutorialScreen, gameCanvas, gameStarter;
    [SerializeField] private TMP_Text playersSlider, fishSlider, patternSlider, timeSlider;
    private int fishAmountValue;
    private int playerAmountValue;
    private int timerValue;
    private int patternAmountValue;

    public Slider playerAmountSlider;
    public Slider fishAmountSlider;
    public Slider patternAmountSlider;
    public Slider timeAmountSlider;

    private void Awake() // Set values to defaults.
    {
        Time.timeScale = 1;

        playerAmountValue = PlayerPrefs.GetInt("fish_basketAmount", 1);
        patternAmountValue = PlayerPrefs.GetInt("fish_patternAmount", 4);
        timerValue = PlayerPrefs.GetInt("fish_timerAmount", 2);
        fishAmountValue = PlayerPrefs.GetInt("fish_fishAmount", 10);

        fishAmountSlider.value = fishAmountValue;
        playerAmountSlider.value = playerAmountValue;
        timeAmountSlider.value = timerValue;
        patternAmountSlider.value = patternAmountValue;

        UpdateDifficulty(fishAmountSlider);
        UpdatePlayerAmount(playerAmountSlider);
        UpdateGameSpeed(timeAmountSlider);
        UpdatePatternAmount(patternAmountSlider);

        // UpdateSliderHandleValues();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
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
        timerValue = (int)slider.value ;
        timeSlider.text = timerValue.ToString();
    }

    public void UpdatePatternAmount(Slider slider)
    {
        patternAmountValue = (int)slider.value;
        patternSlider.text = patternAmountValue.ToString();
    }


    public void ReturnToMainScreen()
    {
        PlayerPrefs.SetInt("fish_basketAmount", 1);
        PlayerPrefs.SetInt("fish_patternAmount", 4);
        PlayerPrefs.SetInt("fish_timerAmount", 2);
        PlayerPrefs.SetInt("fish_fishAmount", 10);
        SceneManager.LoadScene("Main_Farm");
    }

    public void ActivateGame()
    {
        PlayerPrefs.SetInt("fish_basketAmount", playerAmountValue);
        PlayerPrefs.SetInt("fish_patternAmount", patternAmountValue);
        PlayerPrefs.SetInt("fish_timerAmount", timerValue);
        PlayerPrefs.SetInt("fish_fishAmount", fishAmountValue);

        startScreen.SetActive(false); // Disable and hide the starting screen.
        gameStarter.GetComponent<Fish_GameManager>().StartGame(timerValue * 60, playerAmountValue, fishAmountValue, patternAmountValue);
    }

    public void ReturnToSettingScreen()
    {
        SceneManager.LoadScene("FindTheFish");
    }

    public void ToggleTutorial()
    {
        if (tutorialScreen.activeSelf)
        {
            tutorialScreen.SetActive(false);
        }
        else
        {
            tutorialScreen.SetActive(true);
        }
    }
}
