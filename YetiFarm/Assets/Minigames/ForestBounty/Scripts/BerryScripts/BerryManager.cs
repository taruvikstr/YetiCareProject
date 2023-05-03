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
    public Transform[] berrySpawnerList;
    
    public TMP_Text endgame_txt;
    public static int endGame = 0;

    public GameObject birdSpawn;
    public GameObject containers;
    public GameObject buttonManager;

    public static int bManagerDesiredScore;
    public static int bManagerGameMode;
    public static int bManagerDifficulty;

    private int birdCount = 0;
    private bool birdSoundOn = false;
    public static bool gameOn = false;


    private void Awake()
    {
        containers = GameObject.Find("Containers");
        birdSpawn = GameObject.Find("BirdSpawn");
    }

    private void Update()
    {
        birdCount = GameObject.FindGameObjectsWithTag("ProjectileTag").Length;

        // Bird's sound
        if (birdCount > 0 && birdSoundOn == false)
        {
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

        // Generate scores for buckets
        for (int i = 0; i < 3; i++)
        {
            bucketScores[i] = Random.Range(difference - difference/4, difference + difference/4);
            desiredSum -= bucketScores[i];
        }
        bucketScores[3] = desiredSum; // Last bucket gets whatever is left from the desired sum        
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
                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter();
                break;

            case (2, 2):
                birdSpawn.GetComponent<BirdSpawnBehavior>().birdSpawnRate = 10f;
                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter();
                break;

            case (3, 2):
                birdSpawn.GetComponent<BirdSpawnBehavior>().birdSpawnRate = 5f;
                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter();
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