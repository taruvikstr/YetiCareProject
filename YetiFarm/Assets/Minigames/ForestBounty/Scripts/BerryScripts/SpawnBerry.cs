using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBerry : MonoBehaviour
{
    public GameObject currentBerry = null;
    public GameObject berryPrefab;

    public int diff;
    public bool berrySpawning = false;
    public bool hasBerry = false;
    private bool firstSpawn = true;

    private void Update()
    {
        SpawnOneBerry();
    }
    public void SpawnOneBerry()
    {        
        if (currentBerry == null && berrySpawning == false)
        {
            StartCoroutine(RespawnDelay());            
        }
    }

    public IEnumerator RespawnDelay()
    {
        // Berries spanw back with delay, and depending on difficulty the timedelay varies
        berrySpawning = true;
        float delay = 0;

        if (diff == 1)
        {
            delay = 1.0f;
        }
        if (diff == 2)
        {
            delay = 2.0f;
        }
        if (diff == 3)
        {
            delay = 3.0f;
        }

        // Berries spawning in the beginning of game
        if (firstSpawn)
        {
            currentBerry = Instantiate(berryPrefab, transform.position, transform.rotation);
            currentBerry.GetComponent<BerryCheck>().spawnOrigin = gameObject;
            hasBerry = true;
            firstSpawn = false;
        }

        // Berries spawning during game
        else
        {
            yield return new WaitForSeconds(delay);

            currentBerry = Instantiate(berryPrefab, transform.position, transform.rotation);
            currentBerry.GetComponent<BerryCheck>().spawnOrigin = gameObject;
            hasBerry = true;
        }

        berrySpawning = false;
    }

    public void BerryToFalse()
    {
        StartCoroutine(RespawnDelay());
    }
}
