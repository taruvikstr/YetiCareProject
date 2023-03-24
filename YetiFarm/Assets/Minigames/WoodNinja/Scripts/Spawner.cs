using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform spawnPoint;
    
    public GameObject prefab;


    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.CompareTag("Player"))
         {
             Instantiate(prefab, transform.position, transform.rotation);
             Debug.Log("HIT");
         }

    }
    private void OnMouseDown()
    {
        Instantiate(prefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

}
