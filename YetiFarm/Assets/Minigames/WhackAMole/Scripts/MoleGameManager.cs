using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MoleGameManager : MonoBehaviour
{
    [SerializeField] private List<Mole> moles;

    [SerializeField] private List<GameObject> holes;

    [Header("UI objects")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameUI;
   // [SerializeField] private GameObject outOfTimeText;
    [SerializeField] private GameObject bombText;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private GameObject startingCanvas;
    [SerializeField] private TMPro.TextMeshProUGUI vegetableCount;

    [SerializeField] private GameObject scoreTextObject;
    [SerializeField] private GameObject scoreHeader;
    [SerializeField] private GameObject timeTextObject;
    [SerializeField] private GameObject timeheader;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject vegeCount;
    [SerializeField] private GameObject vegeCountHeader;
    // [SerializeField] private GameObject Explosion;
    public AudioManager audioManager;

    public GameObject buttonManager;


    //Hardcoded variables you may want to tune.
    private float startingTime = 60f;
    //Global variables
    private float timeRemaining;
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    int score;
    private bool playing = false;
    public ParticleSystem explosion;
    public ParticleSystem helmetSparks;
    public int difficultyLevel;
    private int molesInGame;
    public int vegetables;
    private int endlessGame;
    private int vegetablesStart;
    public int grabTimer;
    private int rowsInGame;

    private List<int> samenumbercheck = new List<int>();

    private int random;

    

    private void Start()
    {
        
        //Setting time and score text objects to false at startingCanvas.
        scoreHeader.SetActive(false);
        scoreTextObject.SetActive(false);
        timeTextObject.SetActive(false);
        timeheader.SetActive(false);
        exitButton.SetActive(false);
        vegeCount.SetActive(false);
        vegeCountHeader.SetActive(false);
        // Getting number of vegetables in the start of the game
        vegetablesStart = moles.Count;
        //Hide all holes before game to prevent clicking holes
        foreach (GameObject hole in holes)
        {
            hole.SetActive(false);
        }



    }
    public void SetMainMenu()
    {
        SceneManager.LoadScene(2);
    }

    //This is public so the play button can see it.
    public void StartGame(int player_amount, int diff, int game_mode,int rows)
    {
        //Activate holes before game.
        foreach (GameObject hole in holes)
        {
            hole.SetActive(true);
        }

        //Set endlessGamemode from buttonmanagerscript.
        endlessGame = game_mode;
        rowsInGame = rows;
        // Getting difficultyvalue from MoleGameManager Script.
        difficultyLevel = diff;
        grabTimer = diff;
        // Getting moles in game value from buttonmanager script.
        molesInGame = player_amount;
        PlayerPrefs.SetInt("player_amount", molesInGame);
        PlayerPrefs.SetInt("diff", difficultyLevel);
        PlayerPrefs.SetInt("game_mode", endlessGame);
        PlayerPrefs.SetInt("rows", rowsInGame);

        //Changin difficulty for molegame
        if (endlessGame == 1)
        {
            if (difficultyLevel == 1)
            {
                // difficultylevel 9 so it is under 10
                difficultyLevel = 9;
            }
            else if (difficultyLevel == 2)
            {
                difficultyLevel = 10;
            }
            else
            {
                difficultyLevel = 20;
            }
            
            //Change amount of moles in game
            //Choose 3 random holes for moles
            if (molesInGame == 3 && rows == 3)
            {
                for (int i = 0; i < 6; i++)
                {
                    Debug.Log(molesInGame);
                    random = Random.Range(0, moles.Count);

                    while (samenumbercheck.Contains(random))
                    {
                        random = Random.Range(0, moles.Count);
                        Debug.Log("Sama numero " + random);
                    }
                    samenumbercheck.Add(random);
                    moles[random].Hide();
                    moles[random].StopAllCoroutines();
                   
                    moles.Remove(moles[random]);
                    

                }
            }
            // If active mole holes in game is set to 6, generate 6 random moles in game and else all holes are available.
            if (molesInGame == 6 && rows == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    random = Random.Range(0, moles.Count);

                    while (samenumbercheck.Contains(random))
                    {
                        random = Random.Range(0, moles.Count);
                        Debug.Log("Sama numero " + random);
                    }
                    samenumbercheck.Add(random);
                     moles[random].Hide();
                    moles[random].StopAllCoroutines();
                    moles.Remove(moles[random]);
                    
                   // Debug.Log(samenumbercheck[i]);
                }
            }
            //If rows are something else than all in use, it will be priority over active mole count.
            if(rows != 3)
            {
                switch (rows)
                {
                    case 1:
                        for (int i = 0; i < 6; i++)
                        {
                            moles[0].Hide();
                            moles[0].StopAllCoroutines();
                            moles.Remove(moles[0]);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 3; i++)
                        {
                            moles[0].Hide();
                            moles[0].StopAllCoroutines();
                            moles.Remove(moles[0]);
                        }
                        break;
                    default:
                        break;
                }
            }
        }





        //Setting startingcanvas to false and time and scoretextobjects to true.
        scoreHeader.SetActive(true);
        timeheader.SetActive(true);
        scoreTextObject.SetActive(true);
        timeTextObject.SetActive(true);
        //Hide/show the UI elements we dont / do want to see.
       // playButton.SetActive(false);
       // outOfTimeText.SetActive(false);
        bombText.SetActive(false);
        gameUI.SetActive(true);
        vegeCount.SetActive(true);
        vegeCountHeader.SetActive(true);
        exitButton.SetActive(true);
        //Hide all the visible moles.

        for(int i = 0; i < moles.Count; i++)
        {
            moles[i].Hide();
            moles[i].SetIndex(i);
            moles[i].enabled = true;
        }
        

        currentMoles.Clear();
        timeRemaining = startingTime;
        score = 0;
        scoreText.text = "0";
        playing = true;

    }
    // If bomb is clicked explosion particle effect is triggered.
    public void BombExplosion(Vector2 molepos,int moleindex)
    {
        
        ParticleSystem newExplosion = Instantiate(explosion);
        GameObject expGameObject = newExplosion.gameObject;
        newExplosion.transform.position = molepos;
        newExplosion.Play();
        StartCoroutine(DeleteOldPS(expGameObject));
    }
    //If helmet mole is clicked it will play particle effect.
    public void HelmetSpark(Vector2 helmetPos)
    {
        ParticleSystem newHelmetSpark = Instantiate(helmetSparks);
        GameObject tempGameObject = newHelmetSpark.gameObject;
        newHelmetSpark.transform.position = helmetPos;
        newHelmetSpark.Play();
        StartCoroutine(DeleteOldPS(tempGameObject));

    }
    //Old particlesystem explosion gameobject is deleted from scene after 3 seconds.
    IEnumerator DeleteOldPS(GameObject particle)
    {
        yield return new WaitForSeconds(2);
        Destroy(particle);
    }
  
    public void GameOver(int type)
    {
        //Show message
        //if(type == 0)
        //{
        //    outOfTimeText.SetActive(true);
        //}
        //else
        //{
        //    bombText.SetActive(true);
        //}
        

        //Hide all moles
        foreach(Mole mole in moles)
        {
            mole.StopGame();
        }
        //Setting time and score text objects to false at endcanvas.
        scoreHeader.SetActive(false);
        scoreTextObject.SetActive(false);
        timeTextObject.SetActive(false);
        timeheader.SetActive(false);
        exitButton.SetActive(false);
        vegeCount.SetActive(false);
        vegeCountHeader.SetActive(false);

        //Stop the game and show the start UI.
        playing = false;
        buttonManager.GetComponent<ButtonManagerScriptMole>().ActivateGameOverScreen(score, (9 - vegetables), endlessGame);
        //playButton.SetActive(true);
        //exitButton.SetActive(true);
    }
    public void AddScore(int moleIndex, bool isMole)
    {
        //Add and update score if it is a mole.
        if (isMole)
        {
            score += 1;
            scoreText.text = $"{score}";
        }
        
        // Increase time little bit.
       // timeRemaining += 1;

        //Remove from active moles.
        currentMoles.Remove(moles[moleIndex]);
    }
    public void Missed(int moleIndex, bool isMole)
    {
        if (isMole)
        {
            //Decrease time by a little bit.
           // timeRemaining -= 2;
        }
       
        currentMoles.Remove(moles[moleIndex]);
    }

    
    void Update()
    {
        //If a veggie is destroyed it's game over.
        if (playing)
        {
            //if endless game mode time is set to 1 for the whole duration. And difficultylevel increases as score increases, and if vegetable is destroyed game is over.
            vegetableCount.text = vegetables.ToString();
            if(endlessGame == 2)
            {
                difficultyLevel = score;
                if (vegetablesStart != vegetables)
                {
                 //   audioManager.PlaySound("VegePick");
                    GameOver(0);
                }
                // Set timer text objects to false during endless mode
                timeRemaining = 1;
                timeheader.SetActive(false);
                timeTextObject.SetActive(false);
            }
            else
            {
                timeRemaining -= Time.deltaTime;
            }
            
            if(timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver(0);
            }

            // Stop the game and return to the starting screen if the back button is pressed.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                foreach (Mole mole in moles)
                {
                    mole.StopGame();
                }

                //Stop the game and show the start UI.
                playing = false;
                buttonManager.GetComponent<ButtonManagerScriptMole>().ReturnToSettingScreen();
            }
                    timeText.text = $"{(int)timeRemaining / 60} : {(int)timeRemaining % 60:D2}";
            //Check if we need to start any more moles.
            if(currentMoles.Count <= (difficultyLevel / 10))
            {
                //Choose a random mole.
                int index = Random.Range(0, moles.Count);
                if (!currentMoles.Contains(moles[index]))
                {
                    // Doesn't matter if its already doing something, we'll just try again next frame
                    currentMoles.Add(moles[index]);
                    moles[index].Activate(difficultyLevel / 10);
                }

            }
        }

    }
}
