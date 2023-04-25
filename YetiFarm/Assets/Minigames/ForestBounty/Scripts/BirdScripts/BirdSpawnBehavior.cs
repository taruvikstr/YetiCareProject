using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class BirdSpawnBehavior : MonoBehaviour
{
    public GameObject bird;
    public List<GameObject> birdSpawnPoints;
    private IEnumerator coroutine;
    public float birdSpawnRate = 10f;
    private int spawnPoint;
    public GameObject birdNest;

    public static int birdScoreCounter; // = how many berries the bird has to collect before it beats you
    public TMP_Text birdScoreText;

    private void Update()
    {
        // Update the counter everytime bird steals berry
        birdScoreText.text = birdScoreCounter.ToString();

        if (BerryManager.gameOn == false)
        {
            birdScoreText.enabled = false;
            birdNest.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public IEnumerator DelayedBirdSpawn()
    {
        while (BerryManager.gameOn == true)
        {
            Debug.Log("Spawn Bird");
            spawnPoint = Random.Range(0, 2);
            Debug.Log(spawnPoint);
            if (spawnPoint == 1)
            {
                bird.gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
            else
            {
                bird.gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
            }

            yield return new WaitForSeconds(birdSpawnRate);
            Instantiate(bird, birdSpawnPoints[spawnPoint].transform.position, birdSpawnPoints[spawnPoint].transform.rotation);
        }      
    }

    public void BirdSpawnStarter()
    {
        coroutine = DelayedBirdSpawn();
        StartCoroutine(coroutine);

        birdScoreText.enabled = true;
        birdScoreText.text = birdScoreCounter.ToString();
        birdNest.GetComponent<SpriteRenderer>().enabled = true;

    }
}
