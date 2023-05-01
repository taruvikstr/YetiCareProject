using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonManagerScript : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject tutorialScreen;
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI scoreText3;
    public TextMeshProUGUI scoreText4;
    public TextMeshProUGUI feedbackText;
    public GameObject gameStarter; // The object that has the script and function for starting the game based on given parameters.
    private int difficultyValue;
    private int playerAmountValue;
    private int gameSpeedValue;
    private int gameModeValue;
    private int desiredScoreValue;
    public TextMeshProUGUI playerAmountSliderNumText;
    public TextMeshProUGUI difficultySliderNumText;
    public TextMeshProUGUI gameModeSliderNumText;
    public TextMeshProUGUI amountSliderNumText;

    public Slider playerSlider;
    public Slider diffSlider;
    public Slider modeSlider;
    public Slider collectionAmountSlider;

    private void Awake() // Set values to defaults.
    {
        Time.timeScale = 1;

        playerAmountValue = PlayerPrefs.GetInt("chicken_basketAmount", 1);
        difficultyValue = PlayerPrefs.GetInt("chicken_diff", 2);
        gameModeValue = PlayerPrefs.GetInt("chicken_gameMode", 1);
        desiredScoreValue = PlayerPrefs.GetInt("chicken_desiredScore", 50);
        gameSpeedValue = 0;

        playerSlider.value = playerAmountValue;
        diffSlider.value = difficultyValue;
        modeSlider.value = gameModeValue;
        collectionAmountSlider.value = desiredScoreValue / 10;

        UpdatePlayerAmount(playerSlider);
        UpdateDifficulty(diffSlider);
        UpdateGameMode(modeSlider);
        UpdateDesiredScore(collectionAmountSlider);

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

    public void UpdatePlayerAmount(Slider slider)
    {
        playerAmountValue = (int)slider.value;
        playerAmountSliderNumText.text = playerAmountValue.ToString();
    }

    public void UpdateGameSpeed(Slider slider)
    {
        gameSpeedValue = (int)slider.value;
    }

    public void UpdateGameMode(Slider slider)
    {
        gameModeValue = (int)slider.value;
        if (gameModeValue == 1)
        {
            gameModeSliderNumText.text = "#";
            collectionAmountSlider.gameObject.SetActive(true);
        }
        if (gameModeValue == 2)
        {
            gameModeSliderNumText.text = "~";
            collectionAmountSlider.gameObject.SetActive(false);
        }
    }

    public void UpdateDesiredScore(Slider slider)
    {
        desiredScoreValue = (int)slider.value * 10;
        amountSliderNumText.text = desiredScoreValue.ToString();
    }


    public void ReturnToMainScreen()
    {
        PlayerPrefs.SetInt("chicken_basketAmount", 1);
        PlayerPrefs.SetInt("chicken_diff", 2);
        PlayerPrefs.SetInt("chicken_gameMode", 1);
        PlayerPrefs.SetInt("chicken_desiredScore", 50);
        SceneManager.LoadScene("Main_Farm");
    }

    public void ActivateGame()
    {
        PlayerPrefs.SetInt("chicken_basketAmount", playerAmountValue);
        PlayerPrefs.SetInt("chicken_diff", difficultyValue);
        PlayerPrefs.SetInt("chicken_gameMode", gameModeValue);
        PlayerPrefs.SetInt("chicken_desiredScore", desiredScoreValue);

        startScreen.SetActive(false); // Disable and hide the starting screen.
        gameStarter.GetComponent<EggSpawnManager>().StartEggSpawns(difficultyValue, desiredScoreValue, gameModeValue, playerAmountValue);
    }

    public void ActivateGameOverScreen(int score_points, int failed_things, int game_mode)
    {
        endScreen.SetActive(true); // Enable and display the game over screen.
        scoreText1.text = "Ker‰sit " + score_points.ToString() + " kananmunaa.";
        scoreText2.text = "Rikki meni " + failed_things.ToString() + " kananmunaa.";

        if (failed_things == 1)
        {
            scoreText2.text = "Rikki meni " + failed_things.ToString() + " kananmuna.";
        }

        if (game_mode == 1 && failed_things < score_points / 4)
        {
            feedbackText.text = "Hienoa tyˆt‰!";
        }
        else if (game_mode == 1 && failed_things > score_points / 4)
        {
            feedbackText.text = "Ole ensi kerralla varovaisempi!";
        }
        else if (game_mode == 2 && score_points <= 10)
        {
            feedbackText.text = "Parempi onni ensi kerralla!";
        }
        else if (game_mode == 2 && score_points <= 20 && score_points > 10)
        {
            feedbackText.text = "P‰‰sit hyvin alkuun!";
        }
        else if (game_mode == 2 && score_points <= 40 && score_points > 20)
        {
            feedbackText.text = "Seh‰n meni hienosti!";
        }
        else if (game_mode == 2 && score_points <= 60 && score_points > 40)
        {
            feedbackText.text = "Hienoa! Olet oikea kanalan kauhu!";
        }
        else if (game_mode == 2 && score_points <= 80 && score_points > 60)
        {
            feedbackText.text = "Upeaa! Kadehdittava tulos!";
        }
        else if (game_mode == 2 && score_points > 80)
        {
            feedbackText.text = "Onneksi olkoon! Olet mestariker‰‰j‰!";
        }
    }

    public void ReturnToSettingScreen()
    {
        SceneManager.LoadScene("ChickenCoop");
        // endScreen.SetActive(false);
        // startScreen.SetActive(true);
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
