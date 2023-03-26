using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StrawberryBucket : MonoBehaviour
{
    int counter;
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Strawberry")
        {
            Destroy(collision.gameObject, 2);
            counter--;
            txt.text = counter.ToString();
        }
    }
}
