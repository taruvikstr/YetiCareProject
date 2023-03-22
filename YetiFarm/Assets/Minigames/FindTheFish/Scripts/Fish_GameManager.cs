using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] public List<GameObject> fishInstances;
    [SerializeField] private List<GameObject> spawnpoints;

    private void Start()
    {
        StartGame();    
    }

    public void StartGame()
    {
        foreach(GameObject spawn in spawnpoints)
        {
            GameObject fishInstance = Instantiate(fishPrefab, spawn.transform);
            fishInstances.Add(fishInstance);
        }
    }
    public void ResetGame()
    {

    }

}
