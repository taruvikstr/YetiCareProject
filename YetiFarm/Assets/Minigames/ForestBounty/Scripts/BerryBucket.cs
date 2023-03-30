using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerryBucket : MonoBehaviour
{
    int counter;
    int i = 1;
    public TMP_Text txt;
    public string bucketType;


    // Start is called before the first frame update
    void Start()
    {
        counter = Random.Range(1, 5);
        txt.text = counter.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        while (i == 1)
        {
            if (counter == 0)
            {
                txt.text = "kaikki marjat keratty!";
                BerryManager.endGame++;
                i = 0;
            }

            break;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Strawberry") && bucketType == "StrawberryBucket")
        {
            Destroy(collision.gameObject);
            BerryManager.strawberryCount--;
            counter--;
            txt.text = counter.ToString();
            BerryManager.howManyStrawberries--;

        }
        if (collision.gameObject.name.StartsWith("Blueberry") && bucketType == "BlueberryBucket")
        {
            Destroy(collision.gameObject);
            BerryManager.blueberryCount--;
            counter--;
            txt.text = counter.ToString();
            BerryManager.howManyBlueberries--;

        }
        if (collision.gameObject.name.StartsWith("Raspberry") && bucketType == "RaspberryBucket")
        {
            Destroy(collision.gameObject);
            BerryManager.raspberryCount--;
            counter--;
            txt.text = counter.ToString();
            BerryManager.howManyRaspberries--;

        }
        if (collision.gameObject.name.StartsWith("Cowberry") && bucketType == "CowberryBucket")
        {
            Destroy(collision.gameObject);
            BerryManager.cowberryCount--;
            counter--;
            txt.text = counter.ToString();
            BerryManager.howManyCowberries--;

        }
    }
}
