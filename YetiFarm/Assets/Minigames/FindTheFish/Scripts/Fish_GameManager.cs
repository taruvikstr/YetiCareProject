using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] public List<GameObject> fishInstances;
    [SerializeField] private List<GameObject> spawnpoints;
    [SerializeField] public SpriteRenderer dicePrimary, diceSecondary, dicePattern;

    public GameObject selectedObject;
    private Vector3 offset;

    private void Start()
    {
        StartGame();    
    }

    void Update()
    {
        //This if for dragging 
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                //Debug.Log("dragging " + targetObject.name);
                selectedObject = targetObject.transform.gameObject;
                selectedObject.GetComponent<FishController>().isDragged = true;
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject.GetComponent<FishController>().isDragged = false;
            selectedObject = null;
        }
    }

    public void StartGame()
    {
        //Spawning of the fishes
        foreach(GameObject spawn in spawnpoints)
        {
            GameObject fishInstance = Instantiate(fishPrefab, spawn.transform);
            fishInstances.Add(fishInstance);
        }
    }

    public void RollDice()
    {
        //Randomly choosing one of the fishes of the instantiated fish
        GameObject chosenFish = fishInstances[Random.Range(0, fishInstances.Count)];
        FishController chosenFishController = chosenFish.GetComponent<FishController>();
        //Setting the dice features according to the chosen fish
        dicePrimary.color = chosenFishController.primaryColor[0];
        diceSecondary.color = chosenFishController.secondaryColor[0];
        dicePattern.sprite = chosenFishController.pattern[0];

        //Going through all the instantiated fish and comparing which have the chosen fish features and tagging them as chosen fish
        foreach(GameObject fish in fishInstances)
        {
            FishController fishController = fish.GetComponent<FishController>();
            if(chosenFishController.primaryColor[0] == fishController.primaryColor[0]
                && chosenFishController.secondaryColor[0] == fishController.secondaryColor[0]
                && chosenFishController.pattern[0] == fishController.pattern[0])
            {
                fishController.chosenFish = true;
            }
            else fishController.chosenFish = false;
        }
    }

    public IEnumerator AddNewFish()
    {
        //When previously found fish is destroyed, this function is called to fill out the spawnpoint slot
        yield return new WaitForSeconds(1f);
        foreach (GameObject spawn in spawnpoints)
        {
            if(spawn.transform.childCount == 0) //If spawnpoint has no child, it gets a new one
            {
                GameObject fishInstance = Instantiate(fishPrefab, spawn.transform);
                fishInstances.Add(fishInstance);
                break;
            }

        }

        RollDice();
    }

    public void ResetGame()
    {

    }

}
