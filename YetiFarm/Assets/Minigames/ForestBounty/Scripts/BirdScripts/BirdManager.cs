using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

// HUOM HUOM kun tehdään animaatiota linnulle, ei voi käytttää childobjecteja. tee animaatio

public class BirdManager : MonoBehaviour
{
    private List<Transform> berryPositions;
    private List<Transform> birdPositions;
     
    private Vector2 toBerry; // vector from bird spawnpoint to berryPositions[randomIndex] 
    private Vector2 awayFromBerry; // vector from berry back to birdspawnpoint

    private float movementSpeed = 2f; // bird movement speed
    private bool isMoving = false; // is bird moving
    public bool berryGrabbed = false; // did bird grab berry
    private int randomIndex; // random index for berryPositions list
    private bool berryNotVisited = true;  // when bird has reached berryPositions[randomIndex], berryNotVisited = false;

    private void Awake()
    { 
        berryPositions = new List<Transform> { null, null, null, null, null, null, null, null, null, null, null, null}; // Add more as the number of spawn points in scene increases.
        birdPositions = new List<Transform> { null, null, null }; // Add more as the number of spawn points in scene increases.
        GameObject berrySpawnPointObject = GameObject.Find("SpawnPoints");

        if (berrySpawnPointObject == null)
        {
            Debug.Log("BERRY NOT FOUND");
        }

        for (int i = 0; i < berrySpawnPointObject.transform.childCount; i++)
        {
            berryPositions[i] = berrySpawnPointObject.transform.GetChild(i).transform;
        }

        GameObject birdSpawnPointObject = GameObject.Find("BirdSpawn");
        if (berrySpawnPointObject == null)
        {
            Debug.Log("BIRD NOT FOUND");
        }

        for (int i = 0; i < birdSpawnPointObject.transform.childCount - 1; i++) // -1 because the last childobject is canvas/text for birdscore
        {
            birdPositions[i] = birdSpawnPointObject.transform.GetChild(i).transform;
        }
    }

    void Start()
    {  
        while (true)
        {
            randomIndex = Random.Range(1, berryPositions.Count);
            if (berryPositions[randomIndex].GetComponent<SpawnBerry>().hasBerry == true && berryPositions[randomIndex].GetComponent<SpawnBerry>().berrySpawning == false)
            {
                Debug.Log("lintu menee: " + randomIndex);
                isMoving = true;
                break;
            }
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            toBerry = berryPositions[randomIndex].position;
            awayFromBerry = birdPositions[2].position; // to the "BirdFinalNest" object 

            if(BirdSpawnBehavior.birdScoreCounter == 0 || BerryManager.gameOn == false)
            {
                Destroy(gameObject);
            }

            if (berryGrabbed == true || berryNotVisited == false)
            {
                // move bird away from berry, to the final nest
                if (gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX == true)
                {
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                }

                transform.position = Vector2.MoveTowards(transform.position, awayFromBerry, movementSpeed * Time.deltaTime);

                if (transform.position.Equals(awayFromBerry))
                {
                    if (gameObject.transform.childCount >= 2)
                    {
                        BirdSpawnBehavior.birdScoreCounter--;
                    }
                    Destroy(gameObject);              
                }
            }
            else if (berryNotVisited)
            {
                // move bird to berry
                gameObject.transform.position = Vector2.MoveTowards(transform.position, toBerry, movementSpeed * Time.deltaTime);

                if (transform.position.Equals(toBerry) && berryPositions[randomIndex].GetComponent<SpawnBerry>().hasBerry == true && berryPositions[randomIndex].GetComponent<SpawnBerry>().berrySpawning == false)
                {
                    StealBerry();
                    berryNotVisited = false;
                }
                else if (transform.position.Equals(toBerry) && berryPositions[randomIndex].GetComponent<SpawnBerry>().hasBerry == true && berryPositions[randomIndex].GetComponent<SpawnBerry>().berrySpawning == true)
                {
                    berryNotVisited = false;
                }
            }
        }
    }

    public void StealBerry()
    {
        if (berryPositions[randomIndex].GetComponent<SpawnBerry>().currentBerry.GetComponent<BerryCheck>().berryLayingAround == false)
        {
            berryPositions[randomIndex].GetComponent<SpawnBerry>().currentBerry.GetComponent<BerryCheck>().OnSteal(gameObject);
            berryGrabbed = true;
            gameObject.transform.GetChild(1).GetComponent<BerryCheck>().birdHasBerry = true;
        }

        // ÄLÄ POISTA
        //berryPositions[randomIndex].GetComponent<SpawnBerry>().currentBerry.GetComponent<BerryCheck>().OnSteal(gameObject);
        //berryGrabbed = true;
        //gameObject.transform.GetChild(1).GetComponent<BerryCheck>().birdHasBerry = true;
    }
}
