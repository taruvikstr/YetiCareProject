using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerryManager : MonoBehaviour
{
    public Transform[] strawberrySpawnPoints;
    public Transform[] blueberrySpawnPoints;
    public GameObject strawberryPrefab;
    public GameObject blueberryPrefab;
    public TMP_Text endgame_txt;
    public static int endGame = 0;
    
    public static int strawberryCount = 0;
    public int strawberryLimit = 2;

    public static int blueberryCount = 0;
    public int blueberryLimit = 2;

    private void Start()
    {

    }

    private void Update()
    {
        if (strawberryCount < strawberryLimit)
        {
            SpawnStrawberries();
        }

        if (blueberryCount < blueberryLimit)
        {
            SpawnBlueberries();
        }

        EndGame();
    }

    void SpawnStrawberries()
    {
        int spawnPointIndex = Random.Range(0, strawberrySpawnPoints.Length);

        if (strawberryCount >= strawberryLimit)
        {
            return;
        }

        Instantiate(strawberryPrefab, strawberrySpawnPoints[spawnPointIndex].position, strawberrySpawnPoints[spawnPointIndex].rotation);
        strawberryCount++;
    }
    void SpawnBlueberries()
    {
        int spawnPointIndex = Random.Range(0, blueberrySpawnPoints.Length - 1);

        if (blueberryCount >= blueberryLimit)
        {
            return;
        }

        Instantiate(blueberryPrefab, blueberrySpawnPoints[spawnPointIndex].position, blueberrySpawnPoints[spawnPointIndex].rotation);
        blueberryCount++;
    }

    // End the game after gathering all needed berries
    void EndGame()
    {
        if (endGame == 2 && endgame_txt != null)
        {
            endgame_txt.text = "voitit pelin";
        }
    }
}