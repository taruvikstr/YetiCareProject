using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawnBehavior : MonoBehaviour
{
    public GameObject bird;
    public List<GameObject> birdSpawnPoints;
    private IEnumerator coroutine;

    void Start()
    {
        coroutine = DelayedBirdSpawn();
        StartCoroutine(coroutine);
    }


    // TODO - PLACEHOLDER, CHANGE LATER
    public IEnumerator DelayedBirdSpawn()
    {
        yield return new WaitForSeconds(3.0f);
        Instantiate(bird, birdSpawnPoints[0].transform.position, birdSpawnPoints[0].transform.rotation);
    }
}
