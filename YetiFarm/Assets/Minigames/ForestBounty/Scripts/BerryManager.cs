using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryManager : MonoBehaviour
{
    public Transform[] strawberrySpawnPoints;
    public Transform[] blueberrySpawnPoints;
    public GameObject strawberryPrefab;
    public GameObject blueberryPrefab;
    
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
    }

    public void startInvoke()
    {
        InvokeRepeating("Spawn", 0f, 5f);
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
        int spawnPointIndex = Random.Range(0, blueberrySpawnPoints.Length);

        if (blueberryCount >= blueberryLimit)
        {
            return;
        }

        Instantiate(blueberryPrefab, blueberrySpawnPoints[spawnPointIndex].position, blueberrySpawnPoints[spawnPointIndex].rotation);
        blueberryCount++;
    }
}