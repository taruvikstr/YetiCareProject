using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText1;

   
    public TextMeshProUGUI timerText;


    private Blade blade;
    private SpawnerV2 spawner;
    
    public int score;
    [SerializeField]private WoodButtonManagerScript WbManager;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<SpawnerV2>();

    }
    // Start is called before the first frame update

    public void StartWoodSpawns(int difficultyValue, int gameModeValue)
    {
        // Start the game with the settings given in the parameters.
        Time.timeScale = 1;
        GameStart();
        scoreText.enabled = true;
        scoreText1.enabled = true;


    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText1.text = score.ToString();

    }
    public void PauseGame()
    {
        scoreText.enabled = false;
        scoreText1.enabled = false;
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
        yield return new WaitForSeconds(3f);
    }
    
    private IEnumerator EndGameSequence()
    {

        yield return new WaitForSeconds(4f);

        WoodSlicer[] woods = FindObjectsOfType<WoodSlicer>();
        foreach (WoodSlicer wood in woods)
        {
            Destroy(wood.gameObject);
        }
        Rock[] rocks = FindObjectsOfType<Rock>();
        foreach (Rock rock in rocks)
        {
            Destroy(rock.gameObject);
        }
        scoreText.enabled = false;
        scoreText1.enabled = false;
        Time.timeScale = 0;
        WbManager.ActivateGameOverScreen(score);
    }

}
