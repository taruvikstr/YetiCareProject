using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] fishPrefab;
    [SerializeField] public List<GameObject> fishInstances;
    [SerializeField] private List<GameObject> spawnpoints;
    [SerializeField] public SpriteRenderer dicePrimary, diceSecondary, dicePattern;
    [SerializeField] private Sprite[] dicePatternSprites;

    public float timer = 30f; //Public because the time can be set in settings
    public int playerAmount = 0;

    private GameObject selectedObject;
    private Vector3 offset;
    [HideInInspector]
    public bool gameON = false;

    [SerializeField] private FishUIController fish_UIController;

    void Update()
    {

        //This if for dragging 
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject && targetObject.gameObject.CompareTag("Collectible"))
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
        if (playerAmount == 1) gameON = true;
        //Spawning of the fishes
        foreach (GameObject spawn in spawnpoints)
        {
            GameObject fishInstance = Instantiate(fishPrefab[Random.Range(0, fishPrefab.Length)], spawn.transform);
            fishInstances.Add(fishInstance);
        }

        dicePrimary.gameObject.SetActive(true);
        diceSecondary.gameObject.SetActive(true);
        dicePattern.gameObject.SetActive(true);

        StartCoroutine("RollDice");
    }

    public IEnumerator RollDice()
    {
        yield return new WaitForSeconds(0.5f);
        //Randomly choosing one of the fishes of the instantiated fish
        GameObject chosenFish = fishInstances[Random.Range(0, fishInstances.Count)];
        FishController chosenFishController = chosenFish.GetComponent<FishController>();

        //Setting the dice features according to the chosen fish
        dicePrimary.color = chosenFishController.primaryColor[0];
        diceSecondary.color = chosenFishController.secondaryColor[0];

        //Setting the dice sprite according to the name of the pattern of the chosen fish (Will change this later to Case style, i guess)
        if (chosenFishController.pattern[0].name == "3a")
            dicePattern.sprite = dicePatternSprites[0];
        else if (chosenFishController.pattern[0].name == "3b")
            dicePattern.sprite = dicePatternSprites[1];
        else if (chosenFishController.pattern[0].name == "3c")
            dicePattern.sprite = dicePatternSprites[2];
        else if (chosenFishController.pattern[0].name == "3d")
            dicePattern.sprite = dicePatternSprites[3];
        else if (chosenFishController.pattern[0].name == "3e")
            dicePattern.sprite = dicePatternSprites[4];
        else if (chosenFishController.pattern[0].name == "3f")
            dicePattern.sprite = dicePatternSprites[5];
        else if (chosenFishController.pattern[0].name == "3g")
            dicePattern.sprite = dicePatternSprites[6];
        else if (chosenFishController.pattern[0].name == "3h")
            dicePattern.sprite = dicePatternSprites[7];


        //Going through all the instantiated fish and comparing which have the chosen fish features and tagging them as chosen fish
        foreach (GameObject fish in fishInstances)
        {
            FishController fishController = fish.GetComponent<FishController>();
            if (chosenFishController.primaryColor[0] == fishController.primaryColor[0]
                && chosenFishController.secondaryColor[0] == fishController.secondaryColor[0]
                && chosenFishController.pattern[0].name == fishController.pattern[0].name) // Is this causing the bug of not getting chosenfish tag for different type of fish with same features?
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
                GameObject fishInstance = Instantiate(fishPrefab[Random.Range(0, fishPrefab.Length)], spawn.transform);
                fishInstances.Add(fishInstance);
                break;
            }

        }

        StartCoroutine("RollDice");
    }

    public void ResetGame()
    {
        StopAllCoroutines();

        foreach (GameObject spawn in spawnpoints)
        {
            Destroy(spawn.transform.GetChild(0).gameObject);
        }

        fishInstances.Clear();
        dicePrimary.gameObject.SetActive(false);
        diceSecondary.gameObject.SetActive(false);
        dicePattern.gameObject.SetActive(false);
        playerAmount = 0;
        gameON = false;
    }

}
