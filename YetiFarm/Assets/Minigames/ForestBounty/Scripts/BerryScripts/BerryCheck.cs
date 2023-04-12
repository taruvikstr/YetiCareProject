using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryCheck : MonoBehaviour
{
    public GameObject spawnOrigin = null;
    public bool berryLayingAround = false;
    //public bool birdHasBerry = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // bird is stealing the berry
        if (collision.CompareTag("ProjectileTag"))
        {
            gameObject.transform.parent = collision.gameObject.transform;
            collision.gameObject.GetComponent<BirdManager>().StealBerry();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            berryLayingAround = false;
        }
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    berryLayingAround = false;
    //}

}
