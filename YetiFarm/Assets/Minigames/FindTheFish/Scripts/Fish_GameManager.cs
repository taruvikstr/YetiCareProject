using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] public List<GameObject> fishInstances;
    [SerializeField] private List<GameObject> spawnpoints;
    [SerializeField] private SpriteRenderer dicePrimary, diceSecondary, dicePattern;

    public GameObject selectedObject;
    private Vector3 offset;

    private void Start()
    {
        StartGame();    
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                Debug.Log("dragging " + targetObject.name);
                selectedObject = targetObject.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
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
