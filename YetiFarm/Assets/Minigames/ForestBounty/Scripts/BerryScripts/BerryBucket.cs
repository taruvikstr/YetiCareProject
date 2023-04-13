using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerryBucket : MonoBehaviour
{
    int counter = 0; // berrycounter
    public static int birdScoreCounter = 0; // = how many berries the bird has to collect before it beats you
    public TMP_Text txt; // berrycounter text
    public TMP_Text birdScore; 
    public string bucketType; // cowberry, blueberry, strawberry or raspberry

    // Start is called before the first frame update
    public void StartBuckets()
    {
        switch (BerryManager.howManyberries) // aka difficulty
        {
            case 1:
                counter = Random.Range(1, 5);
                break;
            case 2:
                counter = Random.Range(4, 10);
                break;
            case 3:
                counter = Random.Range(10, 20);
                break;
        }
                
        txt.text = counter.ToString();
        birdScore.text = birdScoreCounter.ToString();

    }
    private void Update()
    {
        // Update the counter everytime bird steals berry
        birdScore.text = birdScoreCounter.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Strawberry") && bucketType == "StrawberryBucket")
        {            
            counter--;
            collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().BerryToFalse();
            Destroy(collision.gameObject);

            if (counter > 0) 
            {
                txt.text = counter.ToString();
            }
            else  // all berries collected in this berrybucket
            {
                txt.text = "Kaikki marjat kerätty!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            
        }
        if (collision.gameObject.name.StartsWith("Blueberry") && bucketType == "BlueberryBucket")
        {
            counter--;
            collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().BerryToFalse();
            Destroy(collision.gameObject);

            if (counter > 0)
            {
                txt.text = counter.ToString();
            }
            else  // all berries collected in this berrybucket
            {
                txt.text = "Kaikki marjat kerätty!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            
        }
        if (collision.gameObject.name.StartsWith("Raspberry") && bucketType == "RaspberryBucket")
        {
            counter--;
            collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().BerryToFalse();
            Destroy(collision.gameObject);

            if (counter > 0)
            {
                txt.text = counter.ToString();
            }
            else  // all berries collected in this berrybucket
            {
                txt.text = "Kaikki marjat kerätty!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

        }
        if (collision.gameObject.name.StartsWith("Cowberry") && bucketType == "CowberryBucket")
        {
            counter--;
            collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().BerryToFalse();
            Destroy(collision.gameObject);

            if (counter > 0)
            {     
                txt.text = counter.ToString();
            }
            else  // all berries collected in this berrybucket
            {
                txt.text = "Kaikki marjat kerätty!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

        }
    }
}
