using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    public Text ScoreText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        ScoreText.text = score.ToString() + " Points";
    }
}
