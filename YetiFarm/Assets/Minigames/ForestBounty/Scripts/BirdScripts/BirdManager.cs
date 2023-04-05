using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class BirdManager : MonoBehaviour
{
    private List<Transform> berryPositions;
    private List<Transform> birdPositions;

    private Vector2 toBerry;
    private Vector2 awayFromBerry;

    private float movementSpeed = 5f;
    private bool isMoving = false;
    private bool berryGrabbed = false;
    private float screenWidth;
    private int randomIndex;
    private bool berryNotVisited = true;

    //public TMP_Text birdScore;
    //private int counter = 0;



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

        while(true)
        {
            randomIndex = Random.Range(0, berryPositions.Count);
            if (berryPositions[randomIndex].GetComponent<SpawnBerry>().hasBerry == true)
            {
                break;
            }
        }



        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!isMoving)
        {
            StartCoroutine(Move());
        }
        
    }

    public IEnumerator Move()
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
            yield return null;
            
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
            transform.position = Vector2.MoveTowards(transform.position, toBerry, movementSpeed * Time.deltaTime);

            if (transform.position.Equals(toBerry))
            {
                berryNotVisited = false;
            }
            
        }

    }

    public void StealBerry()
    {
        // when berry collider has been triggered, this function is called
        berryGrabbed = true;
        berryPositions[randomIndex].GetComponent<SpawnBerry>().hasBerry = false;
        BerryBucket.birdScoreCounter--;
        

        // pistevähennyksiä? tai muita sanktioita? 
    }



}
