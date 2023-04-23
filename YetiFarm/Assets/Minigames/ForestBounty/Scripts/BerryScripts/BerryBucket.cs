using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerryBucket : MonoBehaviour
{
    public int counter = 0; // berrycounter
    public TMP_Text txt; // berrycounter text
    public string bucketType; // cowberry, blueberry, strawberry or raspberry

    public void StartBuckets()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        txt.enabled = true;
        txt.text = counter.ToString();

    }
    private void Update()
    {
        if (BerryManager.endGame == 4 || (BirdSpawnBehavior.birdScoreCounter == 0 && BerryManager.bManagerGameMode != 1))
        {
            txt.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Strawberry") && bucketType == "StrawberryBucket")
        {            
            counter--;
            collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().BerryToFalse();
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().PlaySound("PickUp");

            if (counter > 0) 
            {
                txt.text = counter.ToString();
            }
            else  // all berries collected in this berrybucket
            {
                txt.text = "�mp�ri t�ynn�!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            
        }
        if (collision.gameObject.name.StartsWith("Blueberry") && bucketType == "BlueberryBucket")
        {
            counter--;
            collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().BerryToFalse();
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().PlaySound("PickUp");

            if (counter > 0)
            {
                txt.text = counter.ToString();
            }
            else  // all berries collected in this berrybucket
            {
                txt.text = "�mp�ri t�ynn�!!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            
        }
        if (collision.gameObject.name.StartsWith("Raspberry") && bucketType == "RaspberryBucket")
        {
            counter--;
            collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().BerryToFalse();
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().PlaySound("PickUp");

            if (counter > 0)
            {
                txt.text = counter.ToString();
            }
            else  // all berries collected in this berrybucket
            {
                txt.text = "�mp�ri t�ynn�!!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

        }
        if (collision.gameObject.name.StartsWith("Cowberry") && bucketType == "CowberryBucket")
        {
            counter--;
            collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().BerryToFalse();
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().PlaySound("PickUp");

            if (counter > 0)
            {     
                txt.text = counter.ToString();
            }
            else  // all berries collected in this berrybucket
            {
                txt.text = "�mp�ri t�ynn�!!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

        }
    }
}
