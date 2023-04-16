using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WoodButtonManagerScript : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject endScreen;
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI feedbackText;
    private int difficultyValue;
    private int playerAmountValue;
    private int gameSpeedValue;
    private int gameModeValue;
    private int desiredScoreValue;
    public TextMeshProUGUI playerAmountSliderNumText;
    public TextMeshProUGUI difficultySliderNumText;
    public TextMeshProUGUI gameModeSliderNumText;
    public TextMeshProUGUI amountSliderNumText;
    [SerializeField] private GameManager gameManager;

    private void Awake() // Set values to defaults. Remember to set sliders to these values as well.
    {
        difficultyValue = 2;
        playerAmountValue = 1;
        gameSpeedValue = 0;
        gameModeValue = 1;
        desiredScoreValue = 50;
        FindObjectOfType<Blade>().enabled = false;
        gameManager.PauseGame();
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
            difficultySliderNumText.text = ":/";
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
        }
        if (gameModeValue == 2)
        {
            gameModeSliderNumText.text = ">";
        }
    }

    public void UpdateDesiredScore(Slider slider)
    {
        desiredScoreValue = (int)slider.value * 10;
        amountSliderNumText.text = desiredScoreValue.ToString();
    }
   

    public void ReturnToMainScreen()
    {
        SceneManager.LoadScene("Main_Farm");
    }

    public void ActivateGame()
    {
        
        startScreen.SetActive(false); // Disable and hide the starting screen.
        gameManager.StartWoodSpawns(difficultyValue, desiredScoreValue, gameModeValue, playerAmountValue);
        FindObjectOfType<Blade>().enabled = true;
        

    }

    public void ActivateGameOverScreen(int score)
    {
        endScreen.SetActive(true); // Enable and display the game over screen.
        scoreText1.text = "Pilkoit " + score.ToString() + " puuta.";
       /* scoreText2.text = "Rikki meni " + failed_things.ToString() + " kananmunaa.";

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
        }*/
    }

    public void ReturnToSettingScreen()
    {
        endScreen.SetActive(false);
        startScreen.SetActive(true);
    }
}
