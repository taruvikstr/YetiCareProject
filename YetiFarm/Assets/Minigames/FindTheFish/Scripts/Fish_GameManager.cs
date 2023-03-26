using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] public List<GameObject> fishInstances;
    [SerializeField] private List<GameObject> spawnpoints;
    [SerializeField] private SpriteRenderer dicePrimary, diceSecondary, dicePattern;

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

    public void RollDice()
    {
        GameObject chosenFish = fishInstances[Random.Range(0, fishInstances.Count)];
        dicePrimary.color = chosenFish.GetComponent<FishController>().primaryColor[0];
        diceSecondary.color = chosenFish.GetComponent<FishController>().secondaryColor[0];
        dicePattern.sprite = chosenFish.GetComponent<FishController>().pattern[0];
    }

    public void ResetGame()
    {

    }

}
