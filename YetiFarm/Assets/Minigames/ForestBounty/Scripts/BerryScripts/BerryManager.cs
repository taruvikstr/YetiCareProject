using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using UnityEditor;

// this script also goes as the game manager in this minigame

public class BerryManager : MonoBehaviour 
{
    public Transform[] berrySpawnerList; // list of berry spawnpoints in scene
    public static bool gameOn = false; // while player is picking berries, the game is on 
    public static int bManagerDifficulty; // 3 difficulties

    public TMP_Text endgame_txt;
    public static int endGame = 0; // when endgame == 4, game ends

    public GameObject birdSpawn;
    public GameObject containers; // aka buckets
    public GameObject buttonManager;


    private void Awake()
    {
        containers = GameObject.Find("Containers");

        GameObject berrySpawnPointObject = GameObject.Find("SpawnPoints");
        birdSpawn = GameObject.Find("BirdSpawn");

        if (berrySpawnPointObject == null)
        {
            Debug.Log("BERRY NOT FOUND");
        }
        else
        {
            Debug.Log("Marja l�ytyi");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // If the back button of the device is pressed during game.
        {
            gameOn = false;

            // Delete birds
            foreach (GameObject bird in GameObject.FindGameObjectsWithTag("ProjectileTag"))
            {
                Destroy(bird);
            }
            buttonManager.GetComponent<ButtonManagerForestBounty>().ReturnToMainScreen(); // Return to main view.
        }
        // This happens when bird gathers all berries
        if (BirdSpawnBehavior.birdScoreCounter == 0 && bManagerDifficulty != 1 && gameOn != false)
        {
            gameOn = false;
            buttonManager.GetComponent<ButtonManagerForestBounty>().ActivateGameOverScreen(0, bManagerDifficulty);
        }

        // This happens when player gathers all berries
        if (endGame == 4 && gameOn != false)
        {     
            gameOn = false;
            buttonManager.GetComponent<ButtonManagerForestBounty>().ActivateGameOverScreen(1, bManagerDifficulty);
        }
    }

    public void StartSpawn(int difficulty, int gameMode, int collectionAmount)
    {
        gameOn = true;
        endGame = 0;
        bManagerDifficulty = difficulty;

        containers.GetComponent<BucketSpawn>().SpawnBuckets();

        // Sets berry spawn points's difficulty values and spawn delay
        foreach (Transform spawn in berrySpawnerList)
        {
            spawn.GetComponent<SpawnBerry>().diff = difficulty;
        }

        switch (difficulty) // difficulty 1, 2 or 3. on default it's 2 
        {
            case 1:     
                // easy
                //BerryBucket.birdScoreCounter

                break;

            case 2:  
                // medium difficulty
                BirdSpawnBehavior.birdScoreCounter = 5;

                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter(); // Starts bird 

                break;

            case 3:
                // hard
                BirdSpawnBehavior.birdScoreCounter = 10;

                birdSpawn.GetComponent<BirdSpawnBehavior>().birdSpawnRate = 5f; // set bird to spawn a bit faster
                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter(); // Starts bird 

                break;
        }

        for (int i = 0; i < 12; i++)
        {
            berrySpawnerList[i].GetComponent<SpawnBerry>().SpawnOneBerry(); // instansiate the first berries 
        }

        BerryBucket[] childContainers = containers.GetComponentsInChildren<BerryBucket>();
        foreach (BerryBucket childContainer in childContainers)
        {
            childContainer.StartBuckets();
        }
    }
}