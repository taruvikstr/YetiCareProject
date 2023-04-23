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
    public static int bManagerDifficulty; // 3 bird difficulties 

    public TMP_Text endgame_txt;
    public static int endGame = 0; // when endgame == 4, game ends

    public GameObject birdSpawn;
    public GameObject containers; // aka buckets
    public GameObject buttonManager;
    public static int bManagerGameMode;


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
            Debug.Log("Marja löytyi");
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
        if (BirdSpawnBehavior.birdScoreCounter == 0 && bManagerGameMode == 2 && gameOn != false)
        {
            gameOn = false;
            buttonManager.GetComponent<ButtonManagerForestBounty>().ActivateGameOverScreen(0);
        }

        // This happens when player gathers all berries
        if (endGame == 4 && (bManagerGameMode == 1 || bManagerGameMode == 2) && gameOn != false)
        {     
            gameOn = false;
            buttonManager.GetComponent<ButtonManagerForestBounty>().ActivateGameOverScreen(1);
        }
    }

    
    // TO DO: berry amount changing int
    public void StartSpawn(int difficulty, int gameMode, int desiredScore)
    {
        gameOn = true;
        endGame = 0;
        bManagerDifficulty = difficulty;
        bManagerGameMode = gameMode;
        desiredScore = desiredScore * 4;
        int[] bucketScores = new int[4];

        FindObjectOfType<AudioManager>().PlaySound("MainAmbience");
        
        for (int i = 0; i < 3; i++) // generate scores for the first 3 buckets
        {
            bucketScores[i] = Random.Range(1, desiredScore - (3 - i));
            desiredScore -= bucketScores[i];
        }

        bucketScores[3] = desiredScore; // last bucket gets whatever is left from the desired score        

        containers.GetComponent<BucketSpawn>().SpawnBuckets();

        // Sets berry spawn points's difficulty values and spawn delay
        foreach (Transform spawn in berrySpawnerList)
        {
            spawn.GetComponent<SpawnBerry>().diff = difficulty;
        }

        switch (difficulty, gameMode) // difficulty 1, 2 or 3
        {
            case (1, 1):
                // easy, no bird

                break;

            case (1, 2):
                BirdSpawnBehavior.birdScoreCounter = 3;
                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter(); // Starts bird 

                break;

            case (2, 2):
                // medium difficulty
                BirdSpawnBehavior.birdScoreCounter = 5;
                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter(); // Starts bird 

                break;

            case (3, 2):
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
        int temp = 0;
        foreach (BerryBucket childContainer in childContainers)
        {
            childContainer.counter = bucketScores[temp];
            childContainer.StartBuckets();
            temp++;
        }

    }
}