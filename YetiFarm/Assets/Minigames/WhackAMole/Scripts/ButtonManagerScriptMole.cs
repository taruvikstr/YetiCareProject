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
        difficultyValueMole = 2;
        playerAmountValueMole = 1;
        gameSpeedValue = 0;
        gameModeValueMole = 1;
        desiredScoreValue = 60;
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
        playerAmountValueMole = (int)slider.value;
        playerAmountSliderNumText.text = playerAmountValueMole.ToString();
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
        scoreText1.text = "Tainnutit " + score_points.ToString() + " myyr‰‰.";
        scoreText2.text = "Pilalle meni " + failed_things.ToString() + " vihannesta.";

        if (failed_things == 1)
        {
            scoreText2.text = "Pilalle meni " + failed_things.ToString() + " vihannes.";
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
        endScreen.SetActive(false);
        startScreen.SetActive(true);
    }
}
