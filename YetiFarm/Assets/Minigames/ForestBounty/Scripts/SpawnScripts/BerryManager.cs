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
    private bool gameOn = false;
    public int difficulty; // 3 difficulties

    //public static bool[ ] isBerrySpawned = {false, false, false, false, false, false, false, false, false, false, false, false};   

    public Dictionary<int, bool> isBerrySpawned;

    public static int howManyStrawberries;
    public static int howManyBlueberries;
    public static int howManyRaspberries;
    public static int howManyCowberries;


    public TMP_Text endgame_txt;
    public static int endGame = 0;
    
    public static int strawberryCount = 0;
    public int strawberryLimit = 2;

    public static int blueberryCount = 0;
    public int blueberryLimit = 2;

    public static int raspberryCount = 0;
    public int raspberryLimit = 2;

    public static int cowberryCount = 0;
    public int cowberryLimit = 2;

    private void Awake()
    {
        //berrySpawnerList = new List<Transform> { null, null, null, null, null, null, null, null, null, null, null, null };

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

        //isBerrySpawned = new Dictionary<int, bool>();

        //for (int i = 0; i < 12; i++)
        //{
        //    isBerrySpawned.Add(i, false);
        //}
        StartSpawn();

    }

    private void Update()
    {
        //if (strawberryCount < strawberryLimit)
        //{
        //    SpawnStrawberries();
        //}

        //if (blueberryCount < blueberryLimit)
        //{
        //    SpawnBlueberries();
        //}

        //--------------------------------------------HUOM HUOM HUOM-----------------------------------------
        //Menee tänne kerran tai jotain? koska kun jokasta marjaa laitettu koriin 2 kpl,
        // spawnaa yhden marjan randomisti tyhjään kohtaan!!!
        if (strawberryCount <= 3 || blueberryCount <= 3 || raspberryCount <= 3 || cowberryCount <= 3)
        {
            StartCoroutine(SpawnBerries());
        }

            EndGame();
    }


    // End the game after gathering all needed berries
    void EndGame()
    {
        if (endGame == 4 && endgame_txt != null)
        {
            endgame_txt.text = "voitit pelin";
            StopCoroutine(SpawnBerries());
            gameOn = false;
        }
    }

    public IEnumerator SpawnBerries()
    {
        
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

        //int howManyStrawberries = Random.Range(1, 5);
       
        // ei toimi, mieti asiaa
        //while(gameOn == true && strawberryCount <= 2 && blueberryCount <= 2 && raspberryCount <= 2 && cowberryCount <= 2)

        
            int random_strawberry = Random.Range(0, 3);
            int random_blueberry = Random.Range(3, 6);
            int random_raspberry = Random.Range(6, 9);
            int random_cowberry = Random.Range(9, 12);

            if (strawberryCount < strawberryLimit && berrySpawnerList[random_strawberry].GetComponent<SpawnBerry>().hasBerry == false)
            {
                berrySpawnerList[random_strawberry].GetComponent<SpawnBerry>().SpawnOneBerry();
                strawberryCount++;
            }

            if (blueberryCount < blueberryLimit && berrySpawnerList[random_blueberry].GetComponent<SpawnBerry>().hasBerry == false)
            {
                berrySpawnerList[random_blueberry].GetComponent<SpawnBerry>().SpawnOneBerry();
                blueberryCount++;
            }

            if (raspberryCount < raspberryLimit && berrySpawnerList[random_raspberry].GetComponent<SpawnBerry>().hasBerry == false)
            {
                berrySpawnerList[random_raspberry].GetComponent<SpawnBerry>().SpawnOneBerry();
                raspberryCount++;
            }

            if (cowberryCount < cowberryLimit && berrySpawnerList[random_cowberry].GetComponent<SpawnBerry>().hasBerry == false)
            {
                berrySpawnerList[random_cowberry].GetComponent<SpawnBerry>().SpawnOneBerry();
                cowberryCount++;
            }   
        
        yield return new WaitForSeconds(spawnRate);

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