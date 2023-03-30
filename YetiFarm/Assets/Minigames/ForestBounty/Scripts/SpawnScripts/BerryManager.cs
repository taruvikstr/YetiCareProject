using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class BerryManager : MonoBehaviour
{

    public List<GameObject> berrySpawnerList;
    private float spawnRate = 2f;
    private bool gameOn = true;
    public int difficulty; // 3 difficulties

    public static bool[ ] isBerrySpawned = {false, false, false, false, false, false, false, false, false, false, false, false};   
    

    public TMP_Text endgame_txt;
    public static int endGame = 0;
    
    public static int strawberryCount = 0;
    public int strawberryLimit = 2;

    public static int blueberryCount = 0;
    public int blueberryLimit = 2;

  
    private void Start()
    {
        if (!isBerrySpawned.Contains(true))
        {
            Debug.Log("TOTTA TOSIAAN");
        }
        StartCoroutine(SpawnBerries());

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

        //EndGame();
    }


    // End the game after gathering all needed berries
    void EndGame()
    {
        if (endGame == 2 && endgame_txt != null)
        {
            endgame_txt.text = "voitit pelin";
        }
    }

    public IEnumerator SpawnBerries()
    {
        // tähän tehään vaikeustason valinta
        switch (difficulty)
        {
            case 1:
                spawnRate = 2f;
                // no bird
                break;
            case 2:
                spawnRate = 1.5f;
                // bird 
                break;
            case 3:
                spawnRate = 1f;
                // two birds ? or just a faster one ?
                break;
        }

        
        int random_blueberry = Random.Range(3, 6);

        

        int i = 0;
        
        while (i < 7)
        {
            int random_strawberry = Random.Range(0, 3);

            if (strawberryCount < strawberryLimit && isBerrySpawned[random_strawberry] == false)
            {
                berrySpawnerList[random_strawberry].GetComponent<SpawnStrawberry>().SpawnOneStrawberry();
                isBerrySpawned[random_strawberry] = true;
            }

            i++;
        }
        
        if (blueberryCount >= blueberryLimit)
        {
            
        }


        yield return new WaitForSeconds(spawnRate);
        

    }

}