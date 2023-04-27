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
    
    public TMP_Text endgame_txt;
    public static int endGame = 0; // when endgame == 4, game ends

    public GameObject birdSpawn;
    public GameObject containers; // aka buckets
    public GameObject buttonManager;

    public static int bManagerDesiredScore;
    public static int bManagerGameMode;
    public static int bManagerDifficulty; // 3 bird difficulties 

    private int birdCount = 0;
    private bool birdSoundOn = false;


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
        birdCount = GameObject.FindGameObjectsWithTag("ProjectileTag").Length;

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

        if (Input.GetKeyDown(KeyCode.Escape)) // If the back button of the device is pressed during game.
        {
            gameOn = false;

            // Delete birds
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
        
        // using uniform distribution.... so that the random generated bucket scores wont differ too much from each other

        for (int i = 0; i < 3; i++) // generate scores for the first 3 buckets
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

        switch (difficulty, gameMode) // difficulty 1, 2 or 3 for bird
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

        //for (int i = 0; i < 12; i++)
        //{
        //    berrySpawnerList[i].GetComponent<SpawnBerry>().SpawnOneBerry(); // instansiate the first berries 
        //}

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