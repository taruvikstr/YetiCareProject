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
    
    private int gameModeValue;

    
    public TextMeshProUGUI difficultySliderNumText;
    public TextMeshProUGUI gameModeSliderNumText;
    
    [SerializeField] private GameManager gameManager;

    private void Awake() // Set values to defaults. Remember to set sliders to these values as well.
    {
        difficultyValue = 2;
        
        gameModeValue = 1;
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
            difficultySliderNumText.text = ":(";
        }
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


    public void ReturnToMainScreen()
    {
        SceneManager.LoadScene("Main_Farm");
    }

    public void ActivateGame()
    {
        
        startScreen.SetActive(false); // Disable and hide the starting screen.
        gameManager.StartWoodSpawns(difficultyValue, gameModeValue);
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
