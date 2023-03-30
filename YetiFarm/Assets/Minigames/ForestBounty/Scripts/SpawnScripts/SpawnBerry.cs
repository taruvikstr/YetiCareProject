using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBerry : MonoBehaviour
{
    public GameObject berryPrefab;
    public bool hasBerry = false;

    public void SpawnOneBerry()
    {
        Debug.Log("MANSIKKKAA");
        Instantiate(berryPrefab, transform.position, transform.rotation);
        hasBerry = true;
    }

}
