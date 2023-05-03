using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    private List<Transform> berryPositions;
    private List<Transform> birdPositions;
     
    private Vector2 toBerry;
    private Vector2 awayFromBerry;

    private int randomIndex;
    private float movementSpeed = 2f;
    private bool isMoving = false;
    private bool berryNotVisited = true;
    public bool berryGrabbed = false;

    private void Awake()
    { 
        berryPositions = new List<Transform> { null, null, null, null, null, null, null, null, null, null, null, null};
        birdPositions = new List<Transform> { null, null, null };
        GameObject berrySpawnPointObject = GameObject.Find("SpawnPoints");
        GameObject birdSpawnPointObject = GameObject.Find("BirdSpawn");

        for (int i = 0; i < berrySpawnPointObject.transform.childCount; i++)
        {
            berryPositions[i] = berrySpawnPointObject.transform.GetChild(i).transform;
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
                isMoving = true;
                break;
            }
        }
    }

    private void Update()
    {
        // Bird's movement
        if (isMoving)
        {
            toBerry = berryPositions[randomIndex].position;
            awayFromBerry = birdPositions[2].position; // To the "BirdFinalNest" object 

            if(BirdSpawnBehavior.birdScoreCounter == 0 || BerryManager.gameOn == false)
            {
                Destroy(gameObject);
            }

            if (berryGrabbed == true || berryNotVisited == false)
            {
                // Move bird away from berry, to the final nest
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
                // Move bird to berry
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
    }
}
