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
    private float spawnRate = 1.0f; // The frequency of egg spawns in seconds.
    private bool gameOn = false; // Whether or not the game is still going or not.
    public int desiredScore; // The score that the player is trying to achieve.
    private int failedEggs; // The amount of eggs that missed the baskets and broke on the ground.
    private IEnumerator coroutine; // The coroutine that handles spawning egg waves.


    private void Start()
    {
        coroutine = DoSpawns();
    }

    public void StartEggSpawns()
    {
        score = 0;
        gameOn = true;
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        // Stops egg spawning if three eggs fall on the ground in endless mode.
        if (failedEggs >= 3 && gameMode == 2)
        {
            StopCoroutine(coroutine);
        }
    }

    public void EggFail()
    {
        failedEggs++;
    }

    public void IncreaseScore()
    {
        score++;
    }

    public IEnumerator DoSpawns()
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

        // Integers for randomizing the player that gets their egg spawned first in a wave.
        int playerRandomizer1;
        int playerRandomizer2;

        // Integers for randomizing which chickens lay eggs.
        int r1;
        int r2;
        int r3;
        float timeDelay1;
        float timeDelay2;
        while (!(((failedEggs >= 3) && gameMode == 2) || ((score >= desiredScore) && gameMode == 1) || gameOn == false))
        {
            timeDelay1 = ((float) Random.Range(1, 10)) / 10;
            timeDelay2 = ((float) Random.Range(1, 10)) / 10;
            // Stop the game if three eggs are broken in endless mode or if the player has collected the desired amount of eggs.
            if (((failedEggs >= 3) && gameMode == 2) || ((score >= desiredScore) && gameMode == 1) || gameOn == false)
            {
                gameOn = false;
                break;
            }

            // Randomize the laying order of chickens depending on the amount of baskets.
            if (basketAmount == 1)
            {
                r1 = Random.Range(0, 12);
                eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg();
            }
            else if (basketAmount == 2)
            {
                r1 = Random.Range(0, 6);
                r2 = Random.Range(6, 12);
                playerRandomizer1 = Random.Range(0, 2);
                if (playerRandomizer1 == 1)
                {
                    eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg();
                    yield return new WaitForSeconds(timeDelay1);
                    eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg();
                }
                else
                {
                    eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg();
                    yield return new WaitForSeconds(timeDelay1);
                    eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg();
                }
            }
            else
            {
                r1 = Random.Range(0, 4);
                r2 = Random.Range(4, 8);
                r3 = Random.Range(8, 12);
                playerRandomizer1 = Random.Range(0, 3);
                playerRandomizer2 = Random.Range(0, 2);
                if (playerRandomizer2 == 1)
                {
                    if (playerRandomizer1 == 1)
                    {
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg();
                    }
                    else if (playerRandomizer1 == 2)
                    {
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg();
                    }
                    else
                    {
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg();
                    }
                }
                else
                {
                    if (playerRandomizer1 == 1)
                    {
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg();
                    }
                    else if (playerRandomizer1 == 2)
                    {
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg();
                    }
                    else
                    {
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg();
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg();
                    }
                }
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }
}