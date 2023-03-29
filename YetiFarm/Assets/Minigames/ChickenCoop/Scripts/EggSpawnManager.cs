using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawnManager : MonoBehaviour
{
    public GameObject buttonManager;
    public List<GameObject> eggSpawnerList;
    public int score; // Counts how many eggs have been collected.
    public int difficulty; // Defines how difficult the game is.
    public int gameMode; // The game mode. 1 = collect defined number of eggs and try to avoid breaking any. 2 = endless, survive until three eggs are broken.
    public int basketAmount; // The amount of collection baskets. Defines, how many eggs will spawn at once.
    private float spawnRate = 2.0f; // The frequency of egg spawns in seconds.
    private bool gameOn = false; // Whether or not the game is still going or not.
    public int desiredScore; // The score that the player is trying to achieve.
    private int failedEggs; // The amount of eggs that missed the baskets and broke on the ground.
    private IEnumerator coroutine; // The coroutine that handles spawning egg waves.

    public GameObject basketPrefab;
    public List<GameObject> basketSpawnerList;


    private void Start()
    {
        coroutine = DoSpawns();
    }

    // Difficulty: 1-3, Desired score: 0-100, Game mode: 1 or 2, Basket amount: 1-3
    public void StartEggSpawns(int difficulty_value, int desired_score, int game_mode, int basket_amount)
    {
        basketAmount = basket_amount;
        gameMode = game_mode;
        difficulty = difficulty_value;
        // Collection basket instantiation based on the amount of baskets.
        // Basket names: Basket1, Basket2Left & Basket2Right, Basket3Left & Basket3Middle & Basket3Right
        if (basketAmount == 1)
        {
            GameObject basket = Instantiate(basketPrefab, basketSpawnerList[1].transform.position, basketSpawnerList[1].transform.rotation);
            basket.gameObject.name = "Basket1";
        }
        if (basketAmount == 2)
        {
            GameObject basket1 = Instantiate(basketPrefab, basketSpawnerList[0].transform.position, basketSpawnerList[0].transform.rotation);
            basket1.gameObject.name = "Basket2Left";

            GameObject basket2 = Instantiate(basketPrefab, basketSpawnerList[2].transform.position, basketSpawnerList[2].transform.rotation);
            basket2.gameObject.name = "Basket2Right";
        }
        if (basketAmount == 3)
        {
            GameObject basket1 = Instantiate(basketPrefab, basketSpawnerList[0].transform.position, basketSpawnerList[0].transform.rotation);
            basket1.gameObject.name = "Basket3Left";

            GameObject basket2 = Instantiate(basketPrefab, basketSpawnerList[1].transform.position, basketSpawnerList[1].transform.rotation);
            basket2.gameObject.name = "Basket3Middle";

            GameObject basket3 = Instantiate(basketPrefab, basketSpawnerList[2].transform.position, basketSpawnerList[2].transform.rotation);
            basket3.gameObject.name = "Basket3Right";
        }

        if (game_mode == 1)
        {
            desiredScore = desired_score;
        }
        else if (game_mode == 2)
        {
            desiredScore = int.MaxValue;
        }
        score = 0;
        gameOn = true;
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        // Stops egg spawning if three eggs fall on the ground in endless mode or if desired score is reached in goal mode.
        if ((((failedEggs >= 1) && gameMode == 2) || ((score >= desiredScore) && gameMode == 1)) && gameOn == true)
        {
            StopCoroutine(coroutine);
            gameOn = false;
            desiredScore = 0;
            difficulty = 2;
            gameMode = 1;
            spawnRate = 2.0f;
            basketAmount = 0;

            // Delete baskets.
            foreach (GameObject playerBasket in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(playerBasket);
            }

            // Delete falling eggs.
            foreach (GameObject egg in GameObject.FindGameObjectsWithTag("ProjectileTag"))
            {
                Destroy(egg);
            }


            //buttonManager.GetCo
            // TODO - Pass score and failed eggs to the end screen.

            score = 0;
            failedEggs = 0;
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
        switch (difficulty) // Set the spawnrate of eggs based on the chosen difficulty.
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
        while (!((((failedEggs >= 1) && gameMode == 2) || ((score >= desiredScore) && gameMode == 1)) && gameOn == false))
        {
            if (gameMode == 2) // Increase the spawnrate of eggs in endless mode.
            {
                spawnRate -= 0.05f;
            }
            timeDelay1 = ((float) Random.Range(1, 10)) / 10;
            timeDelay2 = ((float) Random.Range(1, 10)) / 10;
            // Stop the game if three eggs are broken in endless mode or if the player has collected the desired amount of eggs.
            if ((((failedEggs >= 1) && gameMode == 2) || ((score >= desiredScore) && gameMode == 1)) && gameOn == true)
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
            yield return new WaitForSeconds(spawnRate); // The time window between waves.
        }
    }
}