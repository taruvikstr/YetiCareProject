using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Kun pelaajan blade disabloituu väläytä taustan väriä -> tai kiven partikkeli efekti




public class GameManager : MonoBehaviour
{ 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI countdownText1;
    

    //public TextMeshProUGUI timerText;
    public GameObject flashScreen;
    public GameObject spawnerList;
    public Timer timer;
    private Blade blade;
    private Spawner spawner;
    private AudioManager audioManager;
    public int score;
    [SerializeField]private WoodButtonManagerScript WbManager;
    private bool isGameMode1 = false;
    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();

    }
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        flashScreen.SetActive(false);
    }
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            WbManager.ReturnToSettingScreen();
        }
        if(flashScreen!= null)
        {
            if(flashScreen.GetComponent<Image>().color.a > 0)
            {
                var color = flashScreen.GetComponent<Image>().color;
                color.a -= 0.01f;
                flashScreen.GetComponent<Image>().color = color;
                
            }
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
            isGameMode1 = true;
            countdownText.enabled = true;
            countdownText1.enabled = true;
            scoreText.enabled = true;
            scoreText1.enabled = true;
            timer.Timer1();
        }
        else if(gameModeValue == 2)
        {
            isGameMode1 = false;
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
    public void EndGame(bool isRock)
    {
        if (isRock)
        {
            if (isGameMode1)
            {
                StartCoroutine(BladeOff());
                
            }
            if (isGameMode1 == false)
            {
                StartCoroutine(EndGameSequence());
                blade.enabled = false;
                spawner.enabled = false;
                flashScreen.SetActive(false);

            }
        }
        else
        {
            StartCoroutine(EndGameSequence());
            blade.enabled = false;
            spawner.enabled = false;
            flashScreen.SetActive(false);

        }

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
        flashScreen.SetActive(false);
        Destroy(audioManager.gameObject);
        Time.timeScale = 0;
        WbManager.ActivateGameOverScreen(score);
    }

    private IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(3f);
    }
    private IEnumerator BladeOff()
    {
        
        blade.enabled = false;
        TrailRenderer.FindObjectOfType<TrailRenderer>().enabled = false;
        yield return new WaitForSeconds(3f);
        BladeOn();
        
    }
    private void BladeOn()
    {
        blade.enabled = true;
        TrailRenderer.FindObjectOfType<TrailRenderer>().enabled = true;
    }
    public void FlashScreen()
    {
        
        var color = flashScreen.GetComponent<Image>().color;
        color.a = 0.8f;
        flashScreen.GetComponent<Image>().color = color;
        flashScreen.SetActive(true);
    }
}
