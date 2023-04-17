using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{ 
    public TextMeshProUGUI scoreText;
   
    private Blade blade;
    private SpawnerV2 spawner;
    public int score;
    private WoodButtonManagerScript WbManager;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<SpawnerV2>();

    }
    // Start is called before the first frame update

    public void StartWoodSpawns(int difficultyValue, int desiredScoreValue, int gameModeValue, int playerAmountValue)
    {
        // Start the game with the settings given in the parameters.
        Time.timeScale = 1;
        GameStart();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();

    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void EndGame()
    {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(EndGameSequence());
       // Pass values to end screen in order to give player feedback and display score values, then reset all values to default.
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2f);
    }
    
    private IEnumerator EndGameSequence()
    {
        yield return new WaitForSeconds(2f);

        Time.timeScale = 0;       
        
    }

}
