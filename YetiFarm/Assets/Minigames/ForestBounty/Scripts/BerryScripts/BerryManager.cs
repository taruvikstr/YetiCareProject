using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using UnityEditor;

// This script also goes as the game manager in this minigame

public class BerryManager : MonoBehaviour 
{
    public Transform[] berrySpawnerList; // List of berry spawnpoints in scene
    
    public TMP_Text endgame_txt;
    public static int endGame = 0; // When endgame == 4, game ends

    public GameObject birdSpawn;
    public GameObject containers; // Buckets
    public GameObject buttonManager;

    public static int bManagerDesiredScore; // Amount of berries that need to be picked
    public static int bManagerGameMode;
    public static int bManagerDifficulty; // 3 bird difficulties 

    private int birdCount = 0; // How many birds in scene
    private bool birdSoundOn = false;
    public static bool gameOn = false; // While player is picking berries, the game is on 


    private void Awake()
    {
        GameObject berrySpawnPointObject = GameObject.Find("SpawnPoints");
        containers = GameObject.Find("Containers");
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
        birdCount = GameObject.FindGameObjectsWithTag("ProjectileTag").Length;

        // Bird's sound
        if (birdCount > 0 && birdSoundOn == false)
        {
            Debug.Log(birdCount);
            FindObjectOfType<AudioManager>().PlaySound("BirdFlap");
            birdSoundOn = true;
        }
        else if (birdCount == 0 && birdSoundOn == true)
        {
            FindObjectOfType<AudioManager>().StopSound("BirdFlap");
            birdSoundOn = false;
        }

        // If the back button of the device is pressed during game.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameOn = false;

            // Delete birds in scene when game ends
            foreach (GameObject bird in GameObject.FindGameObjectsWithTag("ProjectileTag"))
            {
                Destroy(bird);
            }
            buttonManager.GetComponent<ButtonManagerForestBounty>().ReturnToSettingScreen();
        }
        // This happens when bird gathers all berries
        if (BirdSpawnBehavior.birdScoreCounter == 0 && bManagerGameMode == 2 && gameOn != false)
        {
            Debug.Log("BirdScore: " + BirdSpawnBehavior.birdScoreCounter + "\n Gamemode: " + bManagerGameMode + "\n gameOn: " + gameOn);
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

    public void StartSpawn(int difficulty, int gameMode, int desiredScore)
    {
        FindObjectOfType<AudioManager>().PlaySound("MainAmbience");
        gameOn = true;
        endGame = 0;
        bManagerDifficulty = difficulty;
        bManagerGameMode = gameMode;
        bManagerDesiredScore = desiredScore;

        PlayerPrefs.SetInt("berry_difficulty", bManagerDifficulty);
        PlayerPrefs.SetInt("berry_gameMode", bManagerGameMode);
        PlayerPrefs.SetInt("berry_desiredScore", bManagerDesiredScore);

        foreach (Transform berrySpawn in berrySpawnerList)
        {
            berrySpawn.gameObject.SetActive(true);
        }

        int desiredSum = desiredScore * 4;
        int[] bucketScores = new int[4];
        int difference = desiredSum / 4;

        // Using uniform distribution so that the random generated bucket scores wont differ too much from each other
        // generate scores for buckets
        for (int i = 0; i < 3; i++)
        {
            bucketScores[i] = Random.Range(difference - difference/4, difference + difference/4);
            desiredSum -= bucketScores[i];
        }
        bucketScores[3] = desiredSum; // last bucket gets whatever is left from the desired sum        
        containers.GetComponent<BucketSpawn>().SpawnBuckets();

        // Sets berry spawn points's difficulty values and spawn delay
        foreach (Transform spawn in berrySpawnerList)
        {
            spawn.GetComponent<SpawnBerry>().diff = difficulty;
        }

        // Difficulty 1, 2 or 3 for bird when game mode is 2
        switch (difficulty, gameMode)
        {
            case (1, 2):
                birdSpawn.GetComponent<BirdSpawnBehavior>().birdSpawnRate = 15f;
                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter(); // Starts bird 
                break;

            case (2, 2):
                birdSpawn.GetComponent<BirdSpawnBehavior>().birdSpawnRate = 10f;
                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter(); // Starts bird 
                break;

            case (3, 2):
                birdSpawn.GetComponent<BirdSpawnBehavior>().birdSpawnRate = 5f; // set bird to spawn a bit faster
                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter(); // Starts bird 
                break;
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