using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryCheck : MonoBehaviour
{
    public GameObject spawnOrigin = null; // berry knows its spawnpoint
    public bool berryLayingAround = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // bird is stealing the berry
        if (collision.CompareTag("ProjectileTag")) // collision with bird object
        {
            gameObject.transform.parent = collision.gameObject.transform;
            collision.gameObject.GetComponent<BirdManager>().StealBerry();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            berryLayingAround = false;
        }
        
    }
}
