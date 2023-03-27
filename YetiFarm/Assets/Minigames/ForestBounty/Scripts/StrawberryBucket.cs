using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StrawberryBucket : MonoBehaviour
{
    int counter;
    int i = 1;
    public TMP_Text txt;


    // Start is called before the first frame update
    void Start()
    {
        counter = Random.Range(1, 5);
        txt.text = counter.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        while(i == 1)
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
        if (collision.gameObject.name.StartsWith("Strawberry"))
        {
            Destroy(collision.gameObject, 1);
            BerryManager.strawberryCount--;
            counter--;
            txt.text = counter.ToString();
        }
    }
}
