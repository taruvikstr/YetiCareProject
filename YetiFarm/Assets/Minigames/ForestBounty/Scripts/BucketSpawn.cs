using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketSpawn : MonoBehaviour
{
    public List<GameObject> bucketList;
    public List<GameObject> spawnPoint;
    private int bucketIndex = 0;

    public void SpawnBuckets()
    {
        while (true)
        {
            int random = Random.Range(0, 4);
            if (spawnPoint[random] != null )
            {
                GameObject bucket = Instantiate(bucketList[bucketIndex], spawnPoint[random].transform);
                bucket.transform.parent = gameObject.transform;
                spawnPoint[random] = null;
                bucketIndex++;
            }

            if (bucketIndex == 4)
            {
                break;
            }
        }
    }
}
