using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class BerryManager : MonoBehaviour
{

    public Transform[] strawberrySpawnPoints;
    public Transform[] blueberrySpawnPoints;
    public GameObject strawberryPrefab;
    public static int berryCount;
    public int berryLimit;

    public GameObject sb_prefab;

    void Start()
    {
        berryCount = 1;
        berryLimit = 12;

    }

    private void Update()
    {
        SpawnBerries();
    }

    private void SpawnBerries()
    {
        if (berryCount <= 0)
        {
            Instantiate(strawberryPrefab, strawberrySpawnPoints[1].position, strawberrySpawnPoints[1].rotation);
        }
    }

        

}