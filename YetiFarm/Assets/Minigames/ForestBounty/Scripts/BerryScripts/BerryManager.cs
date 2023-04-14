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
    private int bManagerDifficulty; // 3 difficulties

    public TMP_Text endgame_txt;
    public static int endGame = 0; // when endgame == 4, game ends
    public static int howManyberries;

    public GameObject birdSpawn;
    public GameObject containers; // aka buckets
    public GameObject buttonManager;


    private void Awake()
    {
        GameObject berrySpawnPointObject = GameObject.Find("SpawnPoints");
        birdSpawn = GameObject.Find("BirdSpawn");
        containers = GameObject.Find("Containers");

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
        // This happens when bird gathers all berries
        if (BerryBucket.birdScoreCounter == 0 && bManagerDifficulty != 1 && gameOn != false)
        {
            buttonManager.GetComponent<ButtonManagerForestBounty>().ActivateGameOverScreen(0, bManagerDifficulty);
            gameOn = false;
            StopAllCoroutines();
        }

        // This happens when player gathers all berries
        if (endGame == 4 && endgame_txt != null)
        {
            buttonManager.GetComponent<ButtonManagerForestBounty>().ActivateGameOverScreen(1, bManagerDifficulty);
            gameOn = false;
            StopAllCoroutines();
        }
    }

    public void StartSpawn(int difficulty)
    {
        gameOn = true;
        bManagerDifficulty = difficulty;

        // Sets berry spawn points's difficulty values and spawn delay
        foreach (Transform spawn in berrySpawnerList)
        {
            spawn.GetComponent<SpawnBerry>().diff = difficulty;
        }

        switch (difficulty) // difficulty 1, 2 or 3. on default it's 2 
        {
            case 1:     
                // easy
                howManyberries = 1;

                break;

            case 2:  
                // medium difficulty
                howManyberries = 2;
                BerryBucket.birdScoreCounter = 15;

                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter(); // Starts bird 

                break;

            case 3:
                // hard
                howManyberries = 3;
                BerryBucket.birdScoreCounter = 10;

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