using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBerry : MonoBehaviour
{
    public GameObject berryPrefab;
    public bool hasBerry = false;
    public int diff;
    public bool berrySpawning = false;
    public GameObject currentBerry = null;

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
        // berries spanw back with delay, and depending on difficulty the timedelay varies
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

        yield return new WaitForSeconds(delay);
        berrySpawning = false;

        currentBerry = Instantiate(berryPrefab, transform.position, transform.rotation);
        currentBerry.GetComponent<BerryCheck>().spawnOrigin = gameObject;
        hasBerry = true;
    }

    public void BerryToFalse()
    {
        StartCoroutine(RespawnDelay());
    }

}
