using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Kun pelaajan blade disabloituu v�l�yt� taustan v�ri� -> tai kiven partikkeli efekti




public class GameManager : MonoBehaviour
{ 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI countdownText1;

    //public TextMeshProUGUI timerText;
    public GameObject spawnerList;
    public Timer timer;
    private Blade blade;
    private Spawner spawner;
    private AudioManager audioManager;
    public int score;
    [SerializeField]private WoodButtonManagerScript WbManager;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();

    }
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            WbManager.ReturnToSettingScreen();
        }
    }

    public void StartWoodSpawns(int difficultyValue, int gameModeValue)
    {
        // Start the game with the settings given in the parameters.
        audioManager.PlaySound("Ambience");
        WaitForSeconds();
        Time.timeScale = 1;
        spawner.StartSpawns(difficultyValue, gameModeValue);
        if(gameModeValue == 1)
        {
            countdownText.enabled = true;
            countdownText1.enabled = true;
            scoreText.enabled = true;
            scoreText1.enabled = true;
            timer.Timer1();
        }
        else if(gameModeValue == 2)
        {
            scoreText.enabled = true;
            scoreText1.enabled = true;
            countdownText.enabled = false;
            countdownText1.enabled = false;
            Destroy(timer);
        }

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
        countdownText.enabled = false;
        countdownText1.enabled = false;
        Time.timeScale = 0;
    }
    public void EndGame()
    {
        blade.enabled = false;
        spawner.enabled = false;
        StartCoroutine(EndGameSequence());
    }

    
    
    private IEnumerator EndGameSequence()
    {

        yield return new WaitForSeconds(1f);
        
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
        countdownText.enabled = false;
        countdownText1.enabled = false;
        Destroy(audioManager.gameObject);
        Time.timeScale = 0;
        WbManager.ActivateGameOverScreen(score);
    }

    private IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(3f);
    }
}
