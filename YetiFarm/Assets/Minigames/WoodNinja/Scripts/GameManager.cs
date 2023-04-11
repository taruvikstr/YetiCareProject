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

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<SpawnerV2>();
    }
    // Start is called before the first frame update

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();

    }
    
    public void endGame()
    {
        blade.enabled = false;
        spawner.enabled = false;

       // StartCoroutine(endGameSequence());
    }

   /* private IEnumerator endGameSequence()
    {
   
    }*/

}
