using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class BirdManager : MonoBehaviour
{
    private List<Transform> berryPositions;
    private List<Transform> birdPositions;
     
    private Vector2 toBerry; // vector from bird spawnpoint to berryPositions[randomIndex] 
    private Vector2 awayFromBerry; // vector from berry back to birdspawnpoint

    private float movementSpeed = 5f;
    private bool isMoving = false; // is bird moving
    private bool berryGrabbed = false; // did bird grab berry
    private float screenWidth;
    private int randomIndex;
    private bool berryNotVisited = true;

    private void Awake()
    {
        berryPositions = new List<Transform> { null, null, null, null, null, null, null, null, null, null, null, null}; // Add more as the number of spawn points in scene increases.
        birdPositions = new List<Transform> { null, null }; // Add more as the number of spawn points in scene increases.

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

        for (int i = 0; i < birdSpawnPointObject.transform.childCount; i++)
        {
            birdPositions[i] = birdSpawnPointObject.transform.GetChild(i).transform;
        }
    }

    void Start()
    {
        screenWidth = Screen.width;

        // Set bird's goal
        // TO DO: add for different difficulties
        //counter = 10;
        //birdScore.text = counter.ToString();     
        while (true)
        {
            randomIndex = Random.Range(0, berryPositions.Count);
            if (berryPositions[randomIndex].GetComponent<SpawnBerry>().hasBerry == true && berryPositions[randomIndex].GetComponent<SpawnBerry>().berrySpawning == false)
            {
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

            if (berryGrabbed == true)
            {
                // move bird away with the berry 
                if (transform.position.x < screenWidth / 2)
                {
                    // left side of the screen
                    awayFromBerry = birdPositions[0].position;

                }
                else
                {
                    // right side of the screen
                    awayFromBerry = birdPositions[1].position;
                }

                transform.position = Vector2.MoveTowards(transform.position, awayFromBerry, movementSpeed * Time.deltaTime);

                if ((transform.position.Equals(birdPositions[0].position) || transform.position.Equals(birdPositions[1].position)))
                {
                    Destroy(gameObject);
                }
                

            }
            else if (berryNotVisited == false)
            {
                // move away from berry when berry not found
                transform.position = Vector2.MoveTowards(transform.position, birdPositions[1].position, movementSpeed * Time.deltaTime);

                if (transform.position.Equals(birdPositions[1].position))
                {
                    Destroy(gameObject);
                }

            }
            else if (berryNotVisited)
            {
                // move bird to berry
                gameObject.transform.position = Vector2.MoveTowards(transform.position, toBerry, movementSpeed * Time.deltaTime);

                if (transform.position.Equals(toBerry))
                {
                    berryNotVisited = false;
                }

            }

        }
    }

    public void StealBerry()
    {
        // when berry collider has been triggered, this function is called
        if (gameObject.transform.childCount >= 1)
        {
            berryGrabbed = true;
            gameObject.transform.GetChild(0).GetComponent<DragBerries>().birdHasBerry = true;
            berryPositions[randomIndex].GetComponent<SpawnBerry>().BerryToFalse();

            // TO DO: bird gets score when it takes berry to nest
            BerryBucket.birdScoreCounter--;
        }
    }
}
