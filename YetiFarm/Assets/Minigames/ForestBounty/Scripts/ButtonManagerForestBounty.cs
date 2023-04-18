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
    public TextMeshProUGUI scoreText1;

    public GameObject gameStarter; // The object that has the script and function for starting the game based on given parameters.
    private int difficultyValue;
    //private int playerAmountValue;
    //private int gameSpeedValue;
    //private int gameModeValue;
    //private int desiredScoreValue;

    public ToggleGroup berryAmountGroup;
    public Toggle toggleLow;
    public Toggle toggleMedium;
    public Toggle toggleHigh;


    private void Awake() // Set values to defaults. Remember to set sliders to these values as well.
    {
        difficultyValue = 2;
        //playerAmountValue = 1;
        //gameSpeedValue = 0;
        //gameModeValue = 1;
        //desiredScoreValue = 60;
    }

    //public Toggle difficultySelection
    //{
    //    get { return berryAmountGroup.ActiveToggles().FirstOrDefault (); }
    //}


    public void UpdateDifficulty(Slider slider)
    {
        
    }

    // Uses toggle group, where player decides the amount of berries
    public void UpdateBerryAmount()
    {
        if (toggleLow.isOn)
        {
            difficultyValue = 1;
        }
        else if (toggleMedium.isOn)
        {
            difficultyValue = 2;
        }
        else if (toggleHigh.isOn)
        {
            difficultyValue = 3;
        }
    }

    public void UpdateGameSpeed(Slider slider)
    {
        //gameSpeedValue = (int)slider.value;
    }

    public void UpdateGameMode(Slider slider)
    {
        //gameModeValue = (int)slider.value;
    }

    public void UpdateDesiredScore(Slider slider)
    {
        //desiredScoreValue = (int)slider.value * 10;
    }


    public void ReturnToMainScreen()
    {
        SceneManager.LoadScene("Main_Farm");
    }

    public void ActivateGame()
    {
        startScreen.SetActive(false); // Disable and hide the starting screen.
        gameStarter.GetComponent<BerryManager>().StartSpawn(difficultyValue);
            //(difficultyValue, desiredScoreValue, gameModeValue, playerAmountValue);
    }

    public void ActivateGameOverScreen(int result, int game_mode)
    {
        // result = 0 lintu voitti, result = 1 pelaaja voitti

        endScreen.SetActive(true); // Enable and display the game over screen.

        if (game_mode == 1 && result == 1)
        {
            scoreText1.text = "Hienoa, ker�sit kaikki marjat!";
        }
        else if ((game_mode == 2 || game_mode == 3) && result == 1)
        {
            scoreText1.text = "Hienoa, ker�sit kaikki marjat ennen lintua!";
        }
        else if ((game_mode == 2 || game_mode == 3) && result == 0)
        {
            scoreText1.text = "Lintu oli t�ll� kertaa nopeampi!";
        }
    }

    public void ReturnToSettingScreen()
    {
        endScreen.SetActive(false);
        startScreen.SetActive(true);
    }
}
