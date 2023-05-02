using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameManager gameManager;
    private float timeLeft = 20f;

    // Update is called once per frame
    public void Update()
    {
        Timer1();
    }

    public void Timer1()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            updateTimer(timeLeft);
        }
        else if (timeLeft <= 0 && gameManager != null)
        {
            updateTimer(0);
            gameManager.EndGame();
        }
    }

    public void updateTimer(float remainingTime)
    {
        if (remainingTime < 0)
        {
            remainingTime = 0;
        }
        else if (remainingTime > 0)
        {
            remainingTime += 1;
        }
        float minutes = Mathf.FloorToInt(remainingTime / 60);
        float seconds = Mathf.FloorToInt(remainingTime % 60);
        gameManager.countdownText1.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
