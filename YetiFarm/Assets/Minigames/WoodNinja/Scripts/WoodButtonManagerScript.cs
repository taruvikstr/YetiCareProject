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
        difficultyValue = 1;
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
            gameModeSliderNumText.text = "~";
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
        


       /* if (game_mode == 1 && failed_thingsscore <  4)
        {
            feedbackText.text = "Hienoa tyˆt‰!";
        }
        else if (/*game_mode == 1 && failed_thingsscore > 4)
        {
            feedbackText.text = "Ole ensi kerralla varovaisempi!";
        }*/
        if (/*game_mode == 2 &&*/ score == 1)
        {
            scoreText1.text = "Pilkoit " + score.ToString() + " puun.";
            feedbackText.text = "Parempi onni ensi kerralla!";
        }
        else if (/*game_mode == 2 &&*/ score <= 10)
        {
            scoreText1.text = "Pilkoit " + score.ToString() + " puuta.";
            feedbackText.text = "Parempi onni ensi kerralla!";
        }
        else if (/*game_mode == 2 &&*/ score <= 20 && score > 10)
        {
            scoreText1.text = "Pilkoit " + score.ToString() + " puuta.";
            feedbackText.text = "P‰‰sit hyvin alkuun!";
        }
        else if (/*game_mode == 2 &&*/ score <= 40 && score > 20)
        {
            scoreText1.text = "Pilkoit " + score.ToString() + " puuta.";
            feedbackText.text = "Seh‰n meni hienosti!";
        }
        else if (/*game_mode == 2 &&*/score <= 60 && score > 40)
        {
            scoreText1.text = "Pilkoit " + score.ToString() + " puuta.";
            feedbackText.text =  "Olet oikea metsien kuningas!";
        }
        else if (/*game_mode == 2 &&*/ score <= 80 && score > 60)
        {
            scoreText1.text = "Pilkoit " + score.ToString() + " puuta.";
            feedbackText.text = "Upeaa! Kadehdittava tulos!";
        }
        else if (/*game_mode == 2 && */score > 80)
        {
            scoreText1.text = "Pilkoit " + score.ToString() + " puuta.";
            feedbackText.text = "Onneksi olkoon! Olet mestaripuunhakkaaja!";
        }
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToSettingScreen()
    {
        
        endScreen.SetActive(false);
        startScreen.SetActive(true);
    }
}
