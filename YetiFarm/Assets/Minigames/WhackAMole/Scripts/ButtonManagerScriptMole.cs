using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonManagerScriptMole : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject endScreen;
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI scoreText3;
    public TextMeshProUGUI scoreText4;
    public TextMeshProUGUI feedbackText;

    public Slider moleCountSlider;
    public Slider moleDifficultySlider;
    public Slider moleGameModeSlider;
    public GameObject gameStarter; // The object that has the script and function for starting the game based on given parameters.
    public int difficultyValueMole;
    public int playerAmountValueMole;
    private int gameSpeedValue;
    public int gameModeValueMole;
    private int desiredScoreValue;

    public TextMeshProUGUI playerAmountSliderNumText;
    public TextMeshProUGUI difficultySliderNumText;
    public TextMeshProUGUI gameModeSliderNumText;

    private void Awake() // Set values to defaults. Remember to set sliders to these values as well.
    {
        Time.timeScale = 1;
       // difficultyValueMole = 2;
       // playerAmountValueMole = 1;
        gameSpeedValue = 0;
        // gameModeValueMole = 1;
        desiredScoreValue = 60;

        // Fetching previous start settings from player prefabs, and assigning corresponding values to sliders.
        playerAmountValueMole = PlayerPrefs.GetInt("player_amount", 3);
        difficultyValueMole = PlayerPrefs.GetInt("diff", 2);
        gameModeValueMole = PlayerPrefs.GetInt("game_mode", 1);

        playerAmountValueMole =playerAmountValueMole/3;
       // Debug.Log(playerAmountValueMole + " " + difficultyValueMole + " " + gameModeValueMole);
        moleCountSlider.value = playerAmountValueMole;
        moleDifficultySlider.value = difficultyValueMole;
        moleGameModeSlider.value = gameModeValueMole;
        UpdateGameMode(moleGameModeSlider);
        UpdatePlayerAmount(moleCountSlider);
        UpdateDifficulty(moleDifficultySlider);
    }

    public void UpdateDifficulty(Slider slider)
    {
        difficultyValueMole = (int)slider.value;

        if (difficultyValueMole == 1)
        {
            difficultySliderNumText.text = ":)";
        }
        if (difficultyValueMole == 2)
        {
            difficultySliderNumText.text = ":|";
        }
        if (difficultyValueMole == 3)
        {
            difficultySliderNumText.text = ":(";
        }
    }

    public void UpdatePlayerAmount(Slider slider)
    {
        playerAmountValueMole = (int)slider.value*3;
        playerAmountSliderNumText.text = (slider.value*3).ToString();
    }

    public void UpdateGameSpeed(Slider slider)
    {
        gameSpeedValue = (int)slider.value;
    }

    public void UpdateGameMode(Slider slider)
    {
        gameModeValueMole = (int)slider.value;
        gameModeSliderNumText.text = gameModeValueMole.ToString();
        if (gameModeValueMole == 2)
        {
            moleCountSlider.interactable = false;
            moleDifficultySlider.interactable = false;
        }
        else
        {
            moleCountSlider.interactable = true;
            moleDifficultySlider.interactable = true;
        }
    }

    public void UpdateDesiredScore(Slider slider)
    {
        desiredScoreValue = (int)slider.value * 10;
    }

    public void ReturnToMainScreen()
    {
        SceneManager.LoadScene("Main_Farm");
    }

    public void ActivateGame()
    {
        startScreen.SetActive(false); // Disable and hide the starting screen.
        gameStarter.GetComponent<MoleGameManager>().StartGame(playerAmountValueMole, difficultyValueMole, gameModeValueMole);
    }

    public void ActivateGameOverScreen(int score_points, int failed_things, int game_mode)
    {
        endScreen.SetActive(true); // Enable and display the game over screen.
        scoreText1.text = "Tainnutit " + score_points.ToString() + " myyrää.";
        scoreText2.text = "Menetit " + failed_things.ToString() + " vihannesta.";

        if (score_points <= 10)
        {
            feedbackText.text = "Ole ensi kerralla varovaisempi!";
        }
        else if (score_points <= 20 && score_points > 10)
        {
            feedbackText.text = "Parempi onni ensi kerralla!";
        }
        else if (score_points <= 30 && score_points > 20)
        {
            feedbackText.text = "Pääsit hyvin alkuun!";
        }
        else if ( score_points <= 40 && score_points > 30)
        {
            feedbackText.text = "Sehän meni hienosti!";
        }
        else if (score_points <= 50 && score_points > 40)
        {
            feedbackText.text = "Hienoa! Olet oikea myyrien kauhu!";
        }
        else if (score_points <= 60 && score_points > 50)
        {
            feedbackText.text = "Upeaa! Kadehdittava tulos!";
        }
        else if (score_points < 60)
        {
            feedbackText.text = "Onneksi olkoon! Olet kasvimaan sankari!";
        }
    }

    public void ReturnToSettingScreen()
    {
        SceneManager.LoadScene("WhackAMole");
        // endScreen.SetActive(false);
        // startScreen.SetActive(true);
    }
}
