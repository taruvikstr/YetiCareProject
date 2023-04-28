using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonManagerScriptMole : MonoBehaviour
{
    public Color disabledColor;

    public GameObject rowsDifficultyHandle;
    public GameObject rowsDifficultyHandleText;
    public GameObject moleDifficultyHandle;
    public GameObject moleDifficultyHandleText;
    public GameObject moleCountHandle;
    public GameObject moleCountHandleText;
    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject tutorialScreen;
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI scoreText3;
    public TextMeshProUGUI scoreText4;
    public TextMeshProUGUI feedbackText;

    public Slider moleCountSlider;
    public Slider moleDifficultySlider;
    public Slider moleGameModeSlider;
    public Slider rowsSlider;
    public GameObject gameStarter; // The object that has the script and function for starting the game based on given parameters.
    public int difficultyValueMole;
    public int playerAmountValueMole;
    private int gameSpeedValue;
    public int gameModeValueMole;
    private int desiredScoreValue;
    private int rowsInGame;
    

    public TextMeshProUGUI playerAmountSliderNumText;
    public TextMeshProUGUI difficultySliderNumText;
    public TextMeshProUGUI gameModeSliderNumText;
    public TextMeshProUGUI rowsInGameText;

    private void Awake() // Set values to defaults. Remember to set sliders to these values as well.
    {
        Time.timeScale = 1;
      //  difficultyValueMole = 2;
       // playerAmountValueMole = 1;
        gameSpeedValue = 0;
      //  gameModeValueMole = 1;
        desiredScoreValue = 60;


        //Fetching previous start settings from player prefabs, and assigning corresponding values to sliders.
        playerAmountValueMole = PlayerPrefs.GetInt("player_amount", 3);
        difficultyValueMole = PlayerPrefs.GetInt("diff", 2);
        gameModeValueMole = PlayerPrefs.GetInt("game_mode", 1);
        rowsInGame = PlayerPrefs.GetInt("rows", 3);
    
       
        playerAmountValueMole =playerAmountValueMole/3;
       // Debug.Log(playerAmountValueMole + " " + difficultyValueMole + " " + gameModeValueMole);
        moleCountSlider.value = playerAmountValueMole;
        moleDifficultySlider.value = difficultyValueMole;
        moleGameModeSlider.value = gameModeValueMole;
        rowsSlider.value = rowsInGame;
        UpdateGameMode(moleGameModeSlider);
        UpdatePlayerAmount(moleCountSlider);
        UpdateDifficulty(moleDifficultySlider);
        UpdateRowsAmount(rowsSlider);
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
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
    public void UpdateRowsAmount(Slider slider)
    {
        rowsInGame = (int)slider.value;
        rowsInGameText.text = (slider.value).ToString();
        if(rowsInGame != 3 || gameModeValueMole == 2)
        {
            //Change handle and text color to gray to indicate disabled.
            moleCountSlider.interactable = false;
            moleCountHandle.GetComponent<Image>().color=disabledColor;
            moleCountHandleText.GetComponent<TextMeshProUGUI>().color = disabledColor;
            
        }
        else
        {
            //Set color back to original color if interactable.
            moleCountSlider.interactable = true;
            moleCountHandle.GetComponent<Image>().color = Color.white;
            moleCountHandleText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
      

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
            //Change handle and text color to gray to indicate disabled.
            moleCountSlider.interactable = false;
            moleCountHandle.GetComponent<Image>().color = disabledColor;
            moleCountHandleText.GetComponent<TextMeshProUGUI>().color = disabledColor;
            moleDifficultySlider.interactable = false;
            moleDifficultyHandle.GetComponent<Image>().color = disabledColor;
            moleDifficultyHandleText.GetComponent<TextMeshProUGUI>().color = disabledColor;
            rowsSlider.interactable = false;
            rowsDifficultyHandle.GetComponent<Image>().color = disabledColor;
            rowsDifficultyHandleText.GetComponent<TextMeshProUGUI>().color = disabledColor;
        }
        else
        {
            //Set color back to original color if interactable.
            moleCountSlider.interactable = true;
            moleCountHandle.GetComponent<Image>().color = Color.white;
            moleCountHandleText.GetComponent<TextMeshProUGUI>().color = Color.white;
            moleDifficultySlider.interactable = true;
            moleDifficultyHandle.GetComponent<Image>().color = Color.white;
            moleDifficultyHandleText.GetComponent<TextMeshProUGUI>().color = Color.white;
            rowsSlider.interactable = true;
            rowsDifficultyHandle.GetComponent<Image>().color = Color.white;
            rowsDifficultyHandleText.GetComponent<TextMeshProUGUI>().color = Color.white;
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
        gameStarter.GetComponent<MoleGameManager>().StartGame(playerAmountValueMole, difficultyValueMole, gameModeValueMole,rowsInGame);
    }

    public void ActivateGameOverScreen(int score_points, int failed_things, int game_mode)
    {
        endScreen.SetActive(true); // Enable and display the game over screen.
        scoreText1.text = "Tainnutit " + score_points.ToString() + " myyrää.";
        scoreText2.text = "Menetit " + failed_things.ToString() + " vihannesta.";

        /*if (failed_things == 1)
        {
            scoreText2.text = "Rikki meni " + failed_things.ToString() + " kananmuna.";
        }*/

        if (game_mode == 1 && failed_things < score_points / 4)
        {
            feedbackText.text = "Hienoa työtä!";
        }
        else if (game_mode == 1 && failed_things > score_points / 4)
        {
            feedbackText.text = "Ole ensi kerralla varovaisempi!";
        }
        else if (game_mode == 2 && score_points <= 15)
        {
            feedbackText.text = "Parempi onni ensi kerralla!";
        }
        else if (game_mode == 2 && score_points <= 25 && score_points > 15)
        {
            feedbackText.text = "Pääsit hyvin alkuun!";
        }
        else if (game_mode == 2 && score_points <= 40 && score_points > 25)
        {
            feedbackText.text = "Sehän meni hienosti!";
        }
        else if (game_mode == 2 && score_points <= 60 && score_points > 40)
        {
            feedbackText.text = "Hienoa! Olet oikea myyrien karkoittaja!";
        }
        else if (game_mode == 2 && score_points <= 80 && score_points > 60)
        {
            feedbackText.text = "Upeaa! Kadehdittava tulos!";
        }
        else if (game_mode == 2 && score_points > 80)
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
