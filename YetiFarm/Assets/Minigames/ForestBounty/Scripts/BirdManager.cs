using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public Transform[] berryPositions;

    public Transform[] birdPositions;

    private Vector2 toBerry;
    private Vector2 awayFromBerry;

    private float movementSpeed = 10f;
    private bool isMoving = false;

    private bool ok = false; 

    private bool berryGrabbed = false;

    private float screenWidth;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
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
        int randomIndex = Random.Range(0, berryPositions.Length);

        toBerry = berryPositions[randomIndex].position;

        transform.position = Vector2.MoveTowards(transform.position, toBerry, movementSpeed * Time.deltaTime);

        if (ok)
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
        
    }

    bool StealBerry()
    {
        berryGrabbed = true;

        // pistevähennyksiä? tai muita sanktioita? 

        return true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collectible" && !berryGrabbed)
        {
            Debug.Log("MARJA");
            Destroy(collision.gameObject);
            ok = StealBerry();

            //Destroy(collision.gameObject, 1);
            //BerryManager.strawberryCount--;
            //counter--;
            //txt.text = counter.ToString();
        }
    }

}
