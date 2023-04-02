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
    private float spawnRate = 2f;
    public static bool gameOn = false;
    public int difficulty; // 3 difficulties

    //public Dictionary<int, bool> isBerrySpawned;

    public static int howManyStrawberries;
    public static int howManyBlueberries;
    public static int howManyRaspberries;
    public static int howManyCowberries;

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
            Debug.Log("Marja löytyi");
        }
    }
    private void Start()
    {

        StartSpawn();

    }

    private void Update()
    {
        

        //--------------------------------------------HUOM HUOM HUOM-----------------------------------------
        //Menee tänne kerran tai jotain? koska kun jokasta marjaa laitettu koriin 2 kpl,
        // spawnaa yhden marjan randomisti tyhjään kohtaan!!!
        //if (strawberryCount <= 3 || blueberryCount <= 3 || raspberryCount <= 3 || cowberryCount <= 3)
        //{
        //    StartCoroutine(SpawnBerries());
        //}

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

    void MoveBerryBack()
    {
        // when you drag berry away from its spawnpoint and release it before the right bucket, it moves back to spawnpoint

        //Vector3 parentPos = berrySpawnerList[draggedBerry].transform.position;

        //transform.position = Vector3.MoveTowards(transform.position, parentPos, 10f * Time.deltaTime);


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
                // true if berry is in the bush, else false

                if (checkBerryMissing == false)
                {
                    Debug.Log("MARJA MISSING at " + missingBerry);
                    berrySpawnerList[missingBerry].GetComponent<SpawnBerry>().SpawnOneBerry();
                    break;
                }
            }

            //spawnRate = Random.Range(5f, 10f);            

            yield return new WaitForSeconds(spawnRate);

        }
        

        //if (missingBerry >= 0 || missingBerry <=2)
        //{
        //    // strawberry 
        //    Debug.Log("mansikka missing");
        //    berrySpawnerList[missingBerry].GetComponent<SpawnBerry>().SpawnOneBerry();
        //}
        //else if (missingBerry >3 || missingBerry <= 5)
        //{
        //    // blueberry
        //    Debug.Log("mustikka missing");
            
        //}
       
        // ei toimi, mieti asiaa
        //while(gameOn == true && strawberryCount <= 2 && blueberryCount <= 2 && raspberryCount <= 2 && cowberryCount <= 2)

        
            //int random_strawberry = Random.Range(0, 3);
            //int random_blueberry = Random.Range(3, 6);
            //int random_raspberry = Random.Range(6, 9);
            //int random_cowberry = Random.Range(9, 12);

            //if (strawberryCount < berryLimit && berrySpawnerList[random_strawberry].GetComponent<SpawnBerry>().hasBerry == false)
            //{
            //    berrySpawnerList[random_strawberry].GetComponent<SpawnBerry>().SpawnOneBerry();
            //    strawberryCount++;
            //}

            //if (blueberryCount < berryLimit && berrySpawnerList[random_blueberry].GetComponent<SpawnBerry>().hasBerry == false)
            //{
            //    berrySpawnerList[random_blueberry].GetComponent<SpawnBerry>().SpawnOneBerry();
            //    blueberryCount++;
            //}

            //if (raspberryCount < berryLimit && berrySpawnerList[random_raspberry].GetComponent<SpawnBerry>().hasBerry == false)
            //{
            //    berrySpawnerList[random_raspberry].GetComponent<SpawnBerry>().SpawnOneBerry();
            //    raspberryCount++;
            //}

            //if (cowberryCount < berryLimit && berrySpawnerList[random_cowberry].GetComponent<SpawnBerry>().hasBerry == false)
            //{
            //    berrySpawnerList[random_cowberry].GetComponent<SpawnBerry>().SpawnOneBerry();
            //    cowberryCount++;
            //}   
        
        

        //if (howManyStrawberries <= 0)
        //{

        //}

        // VAIHTOEHTO MIETINTÄÄN
        //if (strawberryCount < strawberryLimit && isBerrySpawned[random_strawberry] == false)
        //{
        //    berrySpawnerList[random_strawberry].GetComponent<SpawnBerry>().SpawnOneBerry();
        //    isBerrySpawned[random_strawberry] = true;
        //    //isBerrySpawned.Add(random_strawberry, true);
        //isBerrySpawned.Add(random_strawberry, true);
        //}
    }

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
    }
}