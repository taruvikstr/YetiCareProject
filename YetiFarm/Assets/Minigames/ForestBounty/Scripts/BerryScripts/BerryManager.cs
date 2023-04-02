using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using UnityEditor;

public class BerryManager : MonoBehaviour
{

    public Transform[] berrySpawnerList;
    private float spawnRate = 0.5f;
    public static bool gameOn = false;
    public int difficulty; // 3 difficulties

    //public static int howManyStrawberries;
    //public static int howManyBlueberries;
    //public static int howManyRaspberries;
    //public static int howManyCowberries;

    private List<int> missingBerries;

    public TMP_Text endgame_txt;
    public static int endGame = 0;
    
    public static int strawberryCount = 0;
    public static int blueberryCount = 0;
    public static int raspberryCount = 0;
    public static int cowberryCount = 0;
    public int berryLimit = 2;


    private void Awake()
    {
        GameObject berrySpawnPointObject = GameObject.Find("SpawnPoints");
        if (berrySpawnPointObject == null)
        {
            Debug.Log("BERRY NOT FOUND");
        }
        else
        {
            Debug.Log("Marja l�ytyi");
        }
    }
    private void Start()
    {
        missingBerries = new List<int>();
        StartSpawn();

    }

    private void Update()
    {
        EndGame();
    }

    // End the game after gathering all needed berries
    void EndGame()
    {
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
            for (missingBerry = 0; missingBerry < berrySpawnerList.Length - 1; missingBerry++)
            {
                checkBerryMissing = berrySpawnerList[missingBerry].GetComponent<SpawnBerry>().hasBerry;
                // true if berry is in the spawnpoint, else false

                if (checkBerryMissing == false)
                {
                    Debug.Log("MARJA MISSING at " + missingBerry);
                    //missingBerries.Add(missingBerry); // add missing berry to list. not in use currently. 
                    berrySpawnerList[missingBerry].GetComponent<SpawnBerry>().SpawnOneBerry();
                    //break;
                }

                yield return new WaitForSeconds(spawnRate);
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
    //        for(int i = 0; i < missingBerries.Count; i++)
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

    void StartSpawn()
    {
        for(int i = 0; i < 12; i++)
        {
            berrySpawnerList[i].GetComponent<SpawnBerry>().SpawnOneBerry();
            strawberryCount = 3;
            blueberryCount = 3;
            raspberryCount = 3;
            cowberryCount = 3;

        }

        gameOn = true;
        StartCoroutine(SpawnBerries());
        //StartCoroutine(HandleMissingBerries());
    }
}