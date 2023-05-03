using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ButtonManagerForestBounty : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject tutorialScreen;
    public TextMeshProUGUI scoreText1;

    public GameObject gameStarter; // The object that has the script and function for starting the game based on given parameters.
    private int difficultyValue;
    private int gameModeValue;
    private int desiredScoreValue;

    public TextMeshProUGUI difficultySliderNumText;
    public TextMeshProUGUI gameModeSliderNumText;
    public TextMeshProUGUI amountSliderNumText;

    public Slider gameDifficultySlider;
    public Slider gameModeSlider;
    public Slider desiredScoreSlider;
    public GameObject diffSliderObj;

    // In Awake set values to defaults. Remember to set sliders to these values as well.
    private void Awake()
    {
        Time.timeScale = 1;

        difficultyValue = PlayerPrefs.GetInt("berry_difficulty", 1);
        gameModeValue = PlayerPrefs.GetInt("berry_gameMode", 1);
        desiredScoreValue = PlayerPrefs.GetInt("berry_desiredScore", 6);

        gameDifficultySlider.value = difficultyValue;
        gameModeSlider.value = gameModeValue;
        desiredScoreSlider.value = desiredScoreValue;

        UpdateDifficulty(gameDifficultySlider);
        UpdateGameMode(gameModeSlider);
        UpdateDesiredScore(desiredScoreSlider);

    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }
    public void UpdateDifficulty(Slider slider)
    {
        difficultyValue = (int)slider.value;

        if (difficultyValue == 1)
        {
            difficultySliderNumText.text = ":)";          
        }
        if (difficultyValue == 2)
        {
            difficultySliderNumText.text = ":|";
        }
        if (difficultyValue == 3)
        {
            difficultySliderNumText.text = ":(";
        }
    }

    public void UpdateGameMode(Slider slider)
    {
        gameModeValue = (int)slider.value;
        if (gameModeValue == 1)
        {
            gameModeSliderNumText.text = "#";
            diffSliderObj.SetActive(false);

        }
        if (gameModeValue == 2)
        {
            gameModeSliderNumText.text = "~";
            diffSliderObj.SetActive(true);
        }
    }

    public void UpdateDesiredScore(Slider slider)
    {
        desiredScoreValue = (int)slider.value;
        amountSliderNumText.text = (4 * desiredScoreValue).ToString();
    }


    public void ReturnToMainScreen()
    {
        PlayerPrefs.SetInt("berry_difficulty", 1);
        PlayerPrefs.SetInt("berry_gameMode", 1);
        PlayerPrefs.SetInt("berry_desiredScore", 6);
        SceneManager.LoadScene("Main_Farm");
    }

    public void ActivateGame()
    {
        PlayerPrefs.SetInt("berry_difficulty", difficultyValue);
        PlayerPrefs.SetInt("berry_gameMode", gameModeValue);
        PlayerPrefs.SetInt("berry_desiredScore", desiredScoreValue);
        startScreen.SetActive(false); // Disable and hide the starting screen.
        gameStarter.GetComponent<BerryManager>().StartSpawn(difficultyValue, gameModeValue, desiredScoreValue);
        Debug.Log("vaikeus " + difficultyValue + " game mode " + gameModeValue + " score " + desiredScoreValue);
    }

    public void ActivateGameOverScreen(int result)
    {
        endScreen.SetActive(true); // Enable and display the game over screen.

        if (gameModeValue == 1 && result == 1)
        {
            scoreText1.text = "Hienoa, keräsit kaikki marjat!";
        }
        else if ((gameModeValue == 2 || gameModeValue == 3) && result == 1)
        {
            scoreText1.text = "Hienoa, keräsit kaikki marjat ennen lintua!";
        }
        else if ((gameModeValue == 2 || gameModeValue == 3) && result == 0)
        {
            scoreText1.text = "Lintu oli tällä kertaa nopeampi!";
        }
    }

    public void ReturnToSettingScreen()
    {
        SceneManager.LoadScene("ForestBounty");
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
