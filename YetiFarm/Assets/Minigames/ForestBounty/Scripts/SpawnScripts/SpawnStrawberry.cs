using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStrawberry : MonoBehaviour
{
    public GameObject strawberry_prefab;

    public void SpawnOneStrawberry()
    {
        Debug.Log("MANSIKKKAA");
        Instantiate(strawberry_prefab, transform.position, transform.rotation);
    }

}
