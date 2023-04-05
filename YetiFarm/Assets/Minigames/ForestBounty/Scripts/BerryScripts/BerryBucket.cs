using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerryBucket : MonoBehaviour
{
    int counter = 0;
    public static int birdScoreCounter = 0;
    public static bool berriesCollected;
    public TMP_Text txt;
    public TMP_Text birdScore;
    public string bucketType;


    // Start is called before the first frame update
    void Start()
    {
        counter = Random.Range(1, 5); 
        txt.text = counter.ToString();

        // How many berries bird has to steal for winning
        birdScoreCounter = 5;
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
            if (counter > 0)
            {
                collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().hasBerry = false;
                Destroy(collision.gameObject);
                BerryManager.strawberryCount--;
                txt.text = counter.ToString();
                Debug.Log("strawberryCount =" + BerryManager.strawberryCount);
            }
            else 
            {
                collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().hasBerry = false;
                Destroy(collision.gameObject);
                txt.text = "Kaikki marjat ker�tty!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            
        }
        if (collision.gameObject.name.StartsWith("Blueberry") && bucketType == "BlueberryBucket")
        {
            counter--;
            if (counter > 0)
            {
                collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().hasBerry = false;
                Destroy(collision.gameObject);
                BerryManager.blueberryCount--;
                txt.text = counter.ToString();
                Debug.Log("blueberryCount =" + BerryManager.blueberryCount);
            }
            else
            {
                collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().hasBerry = false;
                Destroy(collision.gameObject);
                txt.text = "Kaikki marjat ker�tty!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            
        }
        if (collision.gameObject.name.StartsWith("Raspberry") && bucketType == "RaspberryBucket")
        {
            counter--;

            if (counter > 0)
            {
                collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().hasBerry = false;
                Destroy(collision.gameObject);
                BerryManager.raspberryCount--;
                txt.text = counter.ToString();
                Debug.Log("raspberryCount =" + BerryManager.raspberryCount);
            }
            else
            {
                collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().hasBerry = false;
                Destroy(collision.gameObject);
                txt.text = "Kaikki marjat ker�tty!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

        }
        if (collision.gameObject.name.StartsWith("Cowberry") && bucketType == "CowberryBucket")
        {
            counter--;

            if (counter > 0)
            {
                collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().hasBerry = false;
                Destroy(collision.gameObject);
                BerryManager.cowberryCount--;
                txt.text = counter.ToString();
                Debug.Log("cowberryCount =" + BerryManager.cowberryCount);
            }
            else
            {
                collision.gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().hasBerry = false;
                Destroy(collision.gameObject);
                txt.text = "Kaikki marjat ker�tty!";
                BerryManager.endGame++;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

        }
    }
}
