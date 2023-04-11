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
    [SerializeField] private GameObject outOfTimeText;
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

    public GameObject buttonManager;


    //Hardcoded variables you may want to tune.
    private float startingTime = 60f;
    //Global variables
    private float timeRemaining;
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    int score;
    private bool playing = false;
    public ParticleSystem explosion; 
    int difficultyLevel;
    int molesInGame;
    public int vegetables;
    private int endlessGame;
    private int vegetablesStart;

    private List<int> samenumbercheck = new List<int>();

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
        
        


    }
    public void SetMainMenu()
    {
        SceneManager.LoadScene(2);
    }
    //This is public so the play button can see it.
    public void StartGame()
    {
        //Set endlessGamemode from buttonmanagerscript.
        endlessGame = buttonManager.GetComponent<ButtonManagerScriptMole>().gameModeValue;
        
        // Getting difficultyvalue from MoleGameManager Script.
        difficultyLevel = buttonManager.GetComponent<ButtonManagerScriptMole>().difficultyValue;
        // Getting moles in game value from buttonmanager script.
        molesInGame = buttonManager.GetComponent<ButtonManagerScriptMole>().playerAmountValue;
        //Changin difficulty for molegame
        if (difficultyLevel == 1)
        {
            difficultyLevel = 10;
        }
        else if(difficultyLevel == 2)
        {
            difficultyLevel = 20;
        }
        else
        {
            difficultyLevel = 30;
        }
        //  Debug.Log(molesInGame);
        //Change amount of moles in game
        if (molesInGame == 1)
        {
            
            for (int i = 0; i <= 5; i++)
            {
                int random = Random.Range(0, moles.Count);
               
                while(samenumbercheck.Contains(random))
                {
                    random = Random.Range(0, moles.Count);
                }
                moles[random].Hide();
                moles.Remove(moles[random]);
                samenumbercheck.Add(random);

            }
        }
        if (molesInGame == 2)
        {
            for (int i = 0; i <= 2; i++)
            {
                int random = Random.Range(0, moles.Count);

                while (samenumbercheck.Contains(random))
                {
                    random = Random.Range(0, moles.Count);
                }
                moles[random].Hide();
                moles.Remove(moles[random]);
                samenumbercheck.Add(random);
            }
        }
        
        

        //Setting startingcanvas to false and time and scoretextobjects to true.
        scoreHeader.SetActive(true);
        timeheader.SetActive(true);
        scoreTextObject.SetActive(true);
        timeTextObject.SetActive(true);
        startingCanvas.SetActive(false);
        //Hide/show the UI elements we dont / do want to see.
        playButton.SetActive(false);
        outOfTimeText.SetActive(false);
        bombText.SetActive(false);
        gameUI.SetActive(true);
        vegeCount.SetActive(true);
        vegeCountHeader.SetActive(true);
        //Hide all the visible moles.

        for(int i = 0; i < moles.Count; i++)
        {
            moles[i].Hide();
            moles[i].SetIndex(i);
        }
        

        currentMoles.Clear();
        timeRemaining = startingTime;
        score = 0;
        scoreText.text = "0";
        playing = true;

    }
    // If bomb is clicked explosion particle effect is triggered.
    public void BombExplosion(Vector2 molepos)
    {
        
        ParticleSystem newExplosion = Instantiate(explosion);
        GameObject expGameObject = newExplosion.gameObject;
        newExplosion.transform.position = molepos;
        newExplosion.Play();
        StartCoroutine(DeleteOldExplosion(expGameObject));
    }
    //Old particlesystem explosion gameobject is deleted from scene after 3 seconds.
    IEnumerator DeleteOldExplosion(GameObject explosion)
    {
        yield return new WaitForSeconds(3);
        Destroy(explosion);
    }
    public void GameOver(int type)
    {
        //Show message
        if(type == 0)
        {
            outOfTimeText.SetActive(true);
        }
        //else
        //{
        //    bombText.SetActive(true);
        //}

        //Hide all moles
        foreach(Mole mole in moles)
        {
            mole.StopGame();
        }

        //Stop the game and show the start UI.
        playing = false;
        playButton.SetActive(true);
        exitButton.SetActive(true);
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
        if(endlessGame == 2)
        {
            difficultyLevel = score;
            if(vegetablesStart != vegetables)
            {
                GameOver(0);
            }
        }
        if (playing)
        {
            //Update time
            vegetableCount.text = vegetables.ToString();
            timeRemaining -= Time.deltaTime;
            if(timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver(0);
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
