using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<BirdManager>().StealBerry();
        collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.transform.parent = collision.gameObject.transform;
    }
}
