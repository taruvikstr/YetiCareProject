using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerV2 : MonoBehaviour
{

    private Collider2D spawnArea;
    public GameObject[] woodPrefabs;
    public GameObject rockPrefab;
    public float bombChance = 0.1f;

    public float minSpawnDelay;
    public float maxSpawnDelay;

    public float minAngle;
    public float maxAngle;

    private float minForce = 11f;
    private float maxForce = 13f;

    private float maxLifeTime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);


        while (enabled)
        {
            GameObject prefab = woodPrefabs[Random.Range(0, woodPrefabs.Length)];

            if(Random.value < bombChance)
            {
                prefab = rockPrefab;
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
