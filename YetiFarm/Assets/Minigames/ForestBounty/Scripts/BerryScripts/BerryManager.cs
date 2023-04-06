using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using UnityEditor;

public class BerryManager : MonoBehaviour
{

    public Transform[] berrySpawnerList; // list of berry spawnpoints in scene
    private float spawnRate = 0.5f; // berry spawn rate
    public static bool gameOn = false; // while player is picking berries, the game is on 
    public int managerDifficulty; // 3 difficulties
    private List<int> missingBerries;

    public TMP_Text endgame_txt;
    public static int endGame = 0; // when endgame == 4, game ends

    public static int howManyberries;
    
    public static int strawberryCount = 0;
    public static int blueberryCount = 0;
    public static int raspberryCount = 0;
    public static int cowberryCount = 0;
    public int berryLimit = 2; 

    public GameObject birdSpawn;
    public GameObject containers;


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
    private void Start()
    {
        missingBerries = new List<int>();
        //StartSpawn();      
    }

    private void Update()
    {
        EndGame();
    }

    // End the game after gathering all needed berries
    void EndGame()
    {
        if(BerryBucket.birdScoreCounter == 0 && managerDifficulty != 1  && gameOn != false)
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
        Debug.Log("SPAWN BERRIES ON");
        //switch (difficulty)
        //{
        //    case 1:
        //        spawnRate = 2f;
        //        // no bird
        //        break;
        //    case 2:
        //        spawnRate = 1.5f;
        //        // bird 
        //        break;
        //    case 3:
        //        spawnRate = 1f;
        //        // two birds ? or just a faster one ?
        //        break;
        //}
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
                    //missingBerries.Add(missingBerry); // add missing berry to list. not in use currently. 
                    berrySpawnerList[missingBerry].GetComponent<SpawnBerry>().SpawnOneBerry();
                    break;
                }

                //yield return null;
                yield return new WaitForSeconds(1f);
            }

            //foreach (int index in missingBerries)
            //{
            //    berrySpawnerList[index].GetComponent<SpawnBerry>().SpawnOneBerry();
            //    missingBerries.RemoveAt(index);
            //    yield return new WaitForSeconds(spawnRate);
            //}
        }
        
    }

    //public IEnumerator HandleMissingBerries()
    //{
    //    while (gameOn && missingBerries != null)
    //    {
    //        yield return new WaitForSeconds(spawnRate);
    //        // TÄSTÄ JATKA TOMORROW 
    //    }
    //}

    //public IEnumerator HandleMissingBerries()
    //{
    //    while (gameOn && missingBerries != null)
    //    {
    //        for (int i = 0; i < missingBerries.Count; i++)
    //        {
    //            SpawnBerry spawnBerry = berrySpawnerList[missingBerries[i]].GetComponent<SpawnBerry>();

    //            if (spawnBerry != null && !spawnBerry.hasBerry)
    //            {
    //                Debug.Log("MArja missing at " + missingBerries[i]);
    //                spawnBerry.SpawnOneBerry();
    //                yield return new WaitForSeconds(spawnRate);
    //                missingBerries.RemoveAt(i);
    //                i--;
    //            }
    //        }
    //    }


        //}

        public void StartSpawn(int difficulty)
    {
        gameOn = true;
        switch (difficulty) // difficulty 1, 2 or 3. on default it's 2 
        {
            case 1:
                // few berries to collect from player
                // no bird
                // easy peasy 
                spawnRate = 1.5f;
                howManyberries = 1;


                Debug.Log("Easy Mode");

                break;

            case 2:
                // medium difficulty
                spawnRate = 4f;
                Debug.Log("Medium mode");
                howManyberries = 2;
                BerryBucket.birdScoreCounter = 8;


                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter(); // Starts bird 

                break;

            case 3:
                // hardcore .... or as hardcore as it can be in a game like this. 
                spawnRate = 6f;
                Debug.Log("Hard mode");
                howManyberries = 3;
                BerryBucket.birdScoreCounter = 5;


                birdSpawn.GetComponent<BirdSpawnBehavior>().birdSpawnRate = 5f;
                birdSpawn.GetComponent<BirdSpawnBehavior>().BirdSpawnStarter(); // Starts bird 


                break;
        }

        managerDifficulty = difficulty;

        for (int i = 0; i < 12; i++)
        {
            berrySpawnerList[i].GetComponent<SpawnBerry>().SpawnOneBerry(); // instansiate the first berries 
            strawberryCount = 3;
            blueberryCount = 3;
            raspberryCount = 3;
            cowberryCount = 3;
        }

        
        StartCoroutine(SpawnBerries());
        BerryBucket[] childContainers = containers.GetComponentsInChildren<BerryBucket>();
        foreach (BerryBucket childContainer in childContainers)
        {
            childContainer.StartBuckets();
        }

        

        //StartCoroutine(HandleMissingBerries());
    }
}