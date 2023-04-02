using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBerry : MonoBehaviour
{
    public GameObject berryPrefab;
    public bool hasBerry = false;

    private static int berryCount = 0;

    public void SpawnOneBerry()
    {        
        GameObject newberry = Instantiate(berryPrefab, transform.position, transform.rotation);
        newberry.GetComponent<BerryCheck>().spawnOrigin = gameObject;
        //newberry.GetComponent<DragBerries>().berryID = berryCount++;
        hasBerry = true;
    }

}
