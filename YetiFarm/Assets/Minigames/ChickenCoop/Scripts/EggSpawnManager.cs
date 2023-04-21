using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawnManager : MonoBehaviour
{
    public AudioManager audio;
    public GameObject buttonManager;
    public GameObject middleBoundary;
    public GameObject leftBoundary;
    public GameObject rightBoundary;
    public GameObject leftBackground3;
    public GameObject middleBackground3;
    public GameObject rightBackground3;
    public GameObject leftBackground2;
    public GameObject rightBackground2;
    public List<GameObject> eggSpawnerList;
    public List<GameObject> boardList1p;
    public List<GameObject> boardList2p;
    public List<GameObject> boardList3p;
    public int score; // Counts how many eggs have been collected.
    public int difficulty; // Defines how difficult the game is.
    public int gameMode; // The game mode. 1 = collect defined number of eggs and try to avoid breaking any. 2 = endless, survive until three eggs are broken.
    public int basketAmount; // The amount of collection baskets. Defines, how many eggs will spawn at once.
    private float spawnRate = 2.0f; // The frequency of egg spawns in seconds.
    private bool gameOn = false; // Whether or not the game is still going or not.
    public int desiredScore; // The score that the player is trying to achieve.
    private int failedEggs; // The amount of eggs that missed the baskets and broke on the ground.
    private IEnumerator coroutine; // The coroutine that handles spawning egg waves.
    private bool gameLost = false;
    

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
        // Collection basket instantiation, board color config and barrier setup based on the amount of baskets.
        // Basket names: Basket1, Basket2Left & Basket2Right, Basket3Left & Basket3Middle & Basket3Right
        if (basketAmount == 1)
        {
            GameObject basket = Instantiate(basketPrefab, basketSpawnerList[1].transform.position, basketSpawnerList[1].transform.rotation);
            basket.gameObject.name = "Basket1";

            middleBoundary.SetActive(false);
            leftBoundary.SetActive(false);
            rightBoundary.SetActive(false);

            leftBackground3.SetActive(false);
            middleBackground3.SetActive(false);
            rightBackground3.SetActive(false);
            leftBackground2.SetActive(false);
            rightBackground2.SetActive(false);

            for (int i = 0; i < 9; i++)
            {
                boardList2p[i].SetActive(false);
                boardList3p[i].SetActive(false);
                boardList1p[i].SetActive(true);
            }

        }
        if (basketAmount == 2)
        {
            GameObject basket1 = Instantiate(basketPrefab, basketSpawnerList[0].transform.position, basketSpawnerList[0].transform.rotation);
            basket1.gameObject.name = "Basket2Left";

            GameObject basket2 = Instantiate(basketPrefab, basketSpawnerList[2].transform.position, basketSpawnerList[2].transform.rotation);
            basket2.gameObject.name = "Basket2Right";

            middleBoundary.SetActive(true);
            leftBoundary.SetActive(false);
            rightBoundary.SetActive(false);

            leftBackground3.SetActive(false);
            middleBackground3.SetActive(false);
            rightBackground3.SetActive(false);
            leftBackground2.SetActive(true);
            rightBackground2.SetActive(true);

            for (int i = 0; i < 9; i++)
            {
                boardList1p[i].SetActive(false);
                boardList3p[i].SetActive(false);
                boardList2p[i].SetActive(true);
            }
        }
        if (basketAmount == 3)
        {
            GameObject basket1 = Instantiate(basketPrefab, basketSpawnerList[0].transform.position, basketSpawnerList[0].transform.rotation);
            basket1.gameObject.name = "Basket3Left";

            GameObject basket2 = Instantiate(basketPrefab, basketSpawnerList[1].transform.position, basketSpawnerList[1].transform.rotation);
            basket2.gameObject.name = "Basket3Middle";

            GameObject basket3 = Instantiate(basketPrefab, basketSpawnerList[2].transform.position, basketSpawnerList[2].transform.rotation);
            basket3.gameObject.name = "Basket3Right";

            middleBoundary.SetActive(false);
            leftBoundary.SetActive(true);
            rightBoundary.SetActive(true);

            leftBackground3.SetActive(true);
            middleBackground3.SetActive(true);
            rightBackground3.SetActive(true);
            leftBackground2.SetActive(false);
            rightBackground2.SetActive(false);

            for (int i = 0; i < 9; i++)
            {
                boardList1p[i].SetActive(false);
                boardList2p[i].SetActive(false);
                boardList3p[i].SetActive(true);
            }
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
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        // Stops egg spawning if an egg eggs fall on the ground in endless mode or if desired score is reached in goal mode.
        if ((((failedEggs >= 1) && gameMode == 2) || ((score >= desiredScore) && gameMode == 1)) && gameOn == true)
        {
            gameOn = false;
        }

        if (gameLost)
        {
            gameOn = false;
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

            buttonManager.GetComponent<ButtonManagerScript>().ActivateGameOverScreen(score, failedEggs, gameMode);

            // Reset boards
            for (int i = 0; i < 9; i++)
            {
                boardList2p[i].SetActive(true);
                boardList3p[i].SetActive(true);
                boardList1p[i].SetActive(true);
            }
        }

        // If the back button of the device is pressed during game.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buttonManager.GetComponent<ButtonManagerScript>().ReturnToSettingScreen();
        }
    }

    public void EggFail()
    {
        if (gameOn == true)
        {
            failedEggs++;
            audio.PlaySound("EggBreak");
        }
    }

    public void IncreaseScore()
    {
        if (gameOn == true)
        {
            score++;
            audio.PlaySound("Collect");
        }
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
        gameOn = true;
        while (!((((failedEggs >= 1) && gameMode == 2) || ((score >= desiredScore) && gameMode == 1)) && gameOn == false))
        {
            if (gameMode == 2) // Increase the spawnrate of eggs in endless mode.
            {
                spawnRate -= 0.01f;
            }
            timeDelay1 = ((float) Random.Range(1, 10)) / 10;
            timeDelay2 = ((float) Random.Range(1, 10)) / 10;

            // Randomize the laying order of chickens depending on the amount of baskets.
            if (basketAmount == 1 && difficulty == 3)
            {
                r1 = Random.Range(1, 11);
                eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                audio.PlaySound("EggSpawn");
                yield return new WaitForSeconds(timeDelay1);
            }
            else if (basketAmount == 1 && difficulty == 2)
            {
                r1 = Random.Range(2, 10);
                eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                audio.PlaySound("EggSpawn");
                yield return new WaitForSeconds(timeDelay1);
            }
            else if (basketAmount == 1 && difficulty == 1)
            {
                r1 = Random.Range(3, 9);
                eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                audio.PlaySound("EggSpawn");
                yield return new WaitForSeconds(timeDelay1);
            }
            else if (basketAmount == 2)
            {
                r1 = Random.Range(0, 6);
                r2 = Random.Range(6, 12);
                playerRandomizer1 = Random.Range(0, 2);
                if (playerRandomizer1 == 1)
                {
                    eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                    audio.PlaySound("EggSpawn");
                    yield return new WaitForSeconds(timeDelay1);
                    eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                    audio.PlaySound("EggSpawn");
                }
                else
                {
                    eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                    audio.PlaySound("EggSpawn");
                    yield return new WaitForSeconds(timeDelay1);
                    eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                    audio.PlaySound("EggSpawn");
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
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                    }
                    else if (playerRandomizer1 == 2)
                    {
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                    }
                    else
                    {
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                    }
                }
                else
                {
                    if (playerRandomizer1 == 1)
                    {
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                    }
                    else if (playerRandomizer1 == 2)
                    {
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                    }
                    else
                    {
                        eggSpawnerList[r3].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay1);
                        eggSpawnerList[r1].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                        yield return new WaitForSeconds(timeDelay2);
                        eggSpawnerList[r2].GetComponent<SpawnEgg>().SpawnSingleEgg(difficulty);
                        audio.PlaySound("EggSpawn");
                    }
                }
            }
            yield return new WaitForSeconds(spawnRate); // The time window between waves.
        }
        if (gameMode == 2)
        {
            yield return new WaitForSeconds(0.5f);
        }
        gameLost = true;
    }
}