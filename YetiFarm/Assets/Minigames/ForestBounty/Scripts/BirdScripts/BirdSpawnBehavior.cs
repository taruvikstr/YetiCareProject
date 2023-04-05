using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawnBehavior : MonoBehaviour
{
    public GameObject bird;
    public List<GameObject> birdSpawnPoints;
    private IEnumerator coroutine;
    private float birdSpawnRate = 5f;
    private int spawnPoint;

    //void Start()
    //{
    //    coroutine = DelayedBirdSpawn();
    //    StartCoroutine(coroutine);

    //    spawnPoint = Random.Range(0, 2);
    //}


    // TODO - PLACEHOLDER, CHANGE LATER
    public IEnumerator DelayedBirdSpawn()
    {

        while (BerryManager.gameOn == true)
        {
            Debug.Log("Spawn Bird");
            yield return new WaitForSeconds(birdSpawnRate);
            Instantiate(bird, birdSpawnPoints[spawnPoint].transform.position, birdSpawnPoints[spawnPoint].transform.rotation);
        }
        
    }

    public void BirdSpawnStarter()
    {
        coroutine = DelayedBirdSpawn();
        StartCoroutine(coroutine);

        spawnPoint = Random.Range(0, 2);
    }
}
