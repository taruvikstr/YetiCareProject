using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawnManager : MonoBehaviour
{
    public List<GameObject> eggSpawnerList;
    public int score; // Counts how many eggs have been collected.
    public int difficulty; // Defines how difficult the game is.
    public int gameMode; // The game mode. 1 = collect defined number of eggs and try to avoid breaking any. 2 = endless, survive until three eggs are broken.
    public int basketAmount; // The amount of collection baskets. Defines, how many eggs will spawn at once.
    private float spawnRate; // The frequency of egg spawns in seconds.
    private bool gameOn = false; // Whether or not the game is still going or not.
    public int desiredScore; // The score that the player is trying to achieve.
    public int failedEggs; // The amount of eggs that missed the baskets and broke on the ground.


    public void StartEggSpawns()
    {
        score = 0;
        gameOn = true;
        StartCoroutine(DoSpawns());
    }

    IEnumerator DoSpawns()
    {
        switch (difficulty)
        {
            case 1:
                spawnRate = 3.0f;
                break;

            case 2:
                spawnRate = 2.0f;
                break;

            case 3:
                spawnRate = 1.0f;
                break;

            default:
                break;
        }
        yield return new WaitForSeconds(3.0f);

        for (int i = 0; i < desiredScore; i++)
        {
            if (gameOn == false)
            {
                break;
            }
            int r1 = Random.Range(0, 2);
            int r2 = Random.Range(2, 4);
            int r3 = Random.Range(4, 6);
            int r4 = Random.Range(6, 8);
            int r5 = Random.Range(8, 10);
            eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg();
            eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg();
            eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg();
            eggSpawnerList[r4].GetComponent<SpawnEgg>().SpawnSingleEgg();
            eggSpawnerList[r5].GetComponent<SpawnEgg>().SpawnSingleEgg();
            yield return new WaitForSeconds(2.5f);
        }
    }
}