using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spawner : MonoBehaviour
{
    

    private Collider2D spawnArea;
    public GameObject[] woodPrefabs;
    public GameObject rockPrefab;
    public float bombChance = 0.1f;

    public float minSpawnDelayWoodOnly;
    public float maxSpawnDelayWoodOnly;
    public float minSpawnDelayWoodAndRock;
    public float maxSpawnDelayWoodAndRock;

    public float minAngle;
    public float maxAngle;

    private float minForce = 11f;
    private float maxForce = 13f;

    private float maxLifeTime = 5f;
    public Timer timer;
    private void Awake()
    {
        spawnArea = GetComponent<Collider2D>();
    }

    public void StartSpawns(int difficultyValue, int gameModeValue)
    {
        StartCoroutine(Spawn(difficultyValue, gameModeValue));
    }
   
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public IEnumerator Spawn(int difficultyValue, int gameModeValue)
    {
        yield return new WaitForSeconds(2f);

        while (gameObject.activeSelf)
        {
            GameObject prefab = null;
            float minSpawnDelay = 0.4f;
            float maxSpawnDelay = 1.7f;

            switch (gameModeValue)
            {
                case 1:
                    
                    if (Random.value < bombChance)
                    {
                        prefab = rockPrefab;
                    }
                    else
                    {
                        prefab = woodPrefabs[Random.Range(0, woodPrefabs.Length)];
                    }
                    
                    break;
                case 2:
                    if (Random.value < bombChance)
                    {
                        prefab = rockPrefab;
                    }
                    else
                    {
                        prefab = woodPrefabs[Random.Range(0, woodPrefabs.Length)];
                    }
                  
                    break;
            }
            switch (difficultyValue)
            {
                case 1:
                    minSpawnDelay = minSpawnDelayWoodOnly;
                    maxSpawnDelay = maxSpawnDelayWoodOnly;
                    bombChance = 0.1f;
                    break;


                case 2:
                    minSpawnDelay = minSpawnDelayWoodOnly - 0.1f;
                    maxSpawnDelay = maxSpawnDelayWoodOnly - 0.3f;
                    bombChance = 0.15f;
                    break;
                case 3:
                    minSpawnDelay = minSpawnDelayWoodOnly - 0.2f;
                    maxSpawnDelay = maxSpawnDelayWoodOnly - 0.5f;
                    bombChance = 0.2f;
                    break;
            }

            Vector2 position = new Vector2();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject wood = Instantiate(prefab, position, rotation);
            Destroy(wood, maxLifeTime);

            float force = Random.Range(minForce, maxForce);
            wood.GetComponent<Rigidbody2D>().AddForce(wood.transform.up * force, ForceMode2D.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
