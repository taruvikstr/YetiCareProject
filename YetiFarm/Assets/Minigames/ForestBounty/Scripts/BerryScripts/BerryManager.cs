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
        // End the game after gathering all needed berries
        if (BerryBucket.birdScoreCounter == 0 && bManagerDifficulty != 1 && gameOn != false)
        {
            endgame_txt.text = "hävisit linnulle";
            gameOn = false;
            StopAllCoroutines();
        }
        if (endGame == 4 && endgame_txt != null)
        {
            endgame_txt.text = "voitit pelin";
            //StopCoroutine(SpawnBerries());
            StopAllCoroutines();
            gameOn = false;
        }
    }


    public IEnumerator SpawnBerries()
    {
        // TO DO: berries spawn on top of each other, fix 

        int missingBerry;
        bool checkBerryMissing;

        while (gameOn)
        {
            
            for (missingBerry = 0; missingBerry < berrySpawnerList.Length ; missingBerry++)
            {
                checkBerryMissing = berrySpawnerList[missingBerry].GetComponent<SpawnBerry>().hasBerry;
                // true if berry is in the spawnpoint, else false

                if (checkBerryMissing == false)
                {
                    Debug.Log("MARJA MISSING at " + missingBerry);
                    berrySpawnerList[missingBerry].GetComponent<SpawnBerry>().SpawnOneBerry();
                    break;
                }

            }
            yield return new WaitForSeconds(1f);

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

        
        StartCoroutine(SpawnBerries());

        BerryBucket[] childContainers = containers.GetComponentsInChildren<BerryBucket>();
        foreach (BerryBucket childContainer in childContainers)
        {
            childContainer.StartBuckets();
        }
    }
}