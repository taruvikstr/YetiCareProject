using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform spawnPoint;
    
    public GameObject prefab;

    private bool isSpawned=true;

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.tag == "Player" && isSpawned)
         {
             Instantiate(prefab, spawnPoint.transform.position, transform.rotation);
             Debug.Log("HIT");
            isSpawned = false;
               
         }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && !isSpawned)
        {
            isSpawned = true;
        } 
    }
    private void OnMouseDown()
    {
        Instantiate(prefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

}
