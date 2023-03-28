using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoleGameManager : MonoBehaviour
{
    [SerializeField] private List<Mole> moles;

    [Header("UI objects")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject outOfTimeText;
    [SerializeField] private GameObject bombText;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
 



    //Hardcoded variables you may want to tune.
    private float startingTime = 30f;
    //Global variables
    private float timeRemaining;
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    int score;
    private bool playing = false;
    
   
    //This is public so the play button can see it.
    public void StartGame()
    {
        //Hide/show the UI elements we dont / do want to see.
        playButton.SetActive(false);
        outOfTimeText.SetActive(false);
        bombText.SetActive(false);
        gameUI.SetActive(true);
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
    public void GameOver(int type)
    {
        //Show message
        if(type == 0)
        {
            outOfTimeText.SetActive(true);
        }
        else
        {
            bombText.SetActive(true);
        }

        //Hide all moles
        foreach(Mole mole in moles)
        {
            mole.StopGame();
        }

        //Stop the game and show the start UI.
        playing = false;
        playButton.SetActive(true);
    }
    public void AddScore(int moleIndex)
    {
        //Add and update score.
        score += 1;
        scoreText.text = $"{score}";
        // Increase time little bit.
        timeRemaining += 1;
        //Remove from active moles.
        currentMoles.Remove(moles[moleIndex]);
    }
    public void Missed(int moleIndex, bool isMole)
    {
        if (isMole)
        {
            //Decrease time by a little bit.
            timeRemaining -= 2;
        }
        currentMoles.Remove(moles[moleIndex]);
    }

    
    void Update()
    {
        
        if (playing)
        {
            //Update time
            timeRemaining -= Time.deltaTime;
            if(timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver(0);
            }
            timeText.text = $"{(int)timeRemaining / 60} : {(int)timeRemaining % 60:D2}";
            //Check if we need to start any more moles.
            if(currentMoles.Count <= (score / 10))
            {
                //Choose a random mole.
                int index = Random.Range(0, moles.Count);
                if (!currentMoles.Contains(moles[index]))
                {
                    // Doesn't matter if its already doing something, we'll just try again next frame
                    currentMoles.Add(moles[index]);
                    moles[index].Activate(score / 10);
                }

            }
        }

    }
}
