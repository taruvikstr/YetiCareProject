using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public Transform[] berryPositions;
    public Transform[] birdPositions;

    private Vector2 toBerry;
    private Vector2 awayFromBerry;

    private float movementSpeed = 5f;
    private bool isMoving = false;
    private bool berryGrabbed = false;
    private float screenWidth;
    private int randomIndex;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
        randomIndex = Random.Range(0, berryPositions.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            Move();
        }

    }

    void Move()
    {

        toBerry = berryPositions[randomIndex].position;

        if (berryGrabbed == true)
        {
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
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, toBerry, movementSpeed * Time.deltaTime);
        }     
    }

    public void StealBerry()
    {
        berryGrabbed = true;

        // pistevähennyksiä? tai muita sanktioita? 
    }

}
