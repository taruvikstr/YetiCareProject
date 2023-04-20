using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fish_GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] fishPrefab;
    [SerializeField] public List<GameObject> fishInstances;
    [SerializeField] private List<GameObject> spawnpoints;
    [SerializeField] public SpriteRenderer dicePrimary, diceSecondary, dicePattern;
    [SerializeField] private Sprite[] dicePatternSprites;

    [SerializeField] private ParticleSystem bubbleParticles;

    public float timer;
    public int playerAmount;
    public int fishAmount;
    public int patternAmount;

    private bool gameON = false;
    private float time;

    [SerializeField] private Image timerImage;

    [SerializeField] private FishUIController fish_UIController;
    [SerializeField] private Fish_ButtonManager fish_ButtonManager;

    void Update()
    {
        //Timer
        if (gameON && timer > 0)
        {
            timerImage.fillAmount = timer / time;
            timer -= Time.deltaTime;
        }
        else if (gameON && timer <= 0)
        {
            fish_UIController.SetPlacements();
            ResetGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetGame();
            fish_ButtonManager.ReturnToSettingScreen();
        }
    }

    public void StartGame(int _timer, int _playerAmount, int _fishAmount, int _patternAmount)
    {
        timer = _timer;
        time = timer;
        playerAmount = _playerAmount;
        fishAmount = _fishAmount;
        patternAmount = _patternAmount;

        fish_UIController.ActivatePlayer(playerAmount);

        //Spawning of the fishes
        for (int i = fishAmount; i > 0; i--)
        {
            GameObject fishInstance = Instantiate(fishPrefab[Random.Range(0, fishPrefab.Length)], spawnpoints[i].transform);
            fishInstance.name = fishInstance.name + spawnpoints[i].name;
            fishInstances.Add(fishInstance);
        }

        gameON = true;
        bubbleParticles.Play();
        StartCoroutine(RollDice(1f));
    }

    public IEnumerator RollDice(float delay)
    {
        yield return new WaitForSeconds(delay); //This is for the bubble particle delay

        dicePrimary.gameObject.SetActive(true);
        diceSecondary.gameObject.SetActive(true);
        dicePattern.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        //Randomly choosing one of the fishes of the instantiated fish
        GameObject chosenFish = fishInstances[Random.Range(0, fishInstances.Count)];
        FishController chosenFishController = chosenFish.GetComponent<FishController>();

        //Setting the dice features according to the chosen fish
        dicePrimary.color = chosenFishController.primaryColor[0];
        diceSecondary.color = chosenFishController.secondaryColor[0];

        string chosenPattern = chosenFishController.pattern[0].name;

        switch (chosenPattern)  //Setting the dice sprite according to the name of the pattern of the chosen fish
        {
            case "3a":
                dicePattern.sprite = dicePatternSprites[0];
                break;
            case "3b":
                dicePattern.sprite = dicePatternSprites[1];
                break;
            case "3c":
                dicePattern.sprite = dicePatternSprites[2];
                break;
            case "3d":
                dicePattern.sprite = dicePatternSprites[3];
                break;
            case "3e":
                dicePattern.sprite = dicePatternSprites[4];
                break;
            case "3f":
                dicePattern.sprite = dicePatternSprites[5];
                break;
            case "3g":
                dicePattern.sprite = dicePatternSprites[6];
                break;
            case "3h":
                dicePattern.sprite = dicePatternSprites[7];
                break;
        }

        //Going through all the instantiated fish and comparing which have the chosen fish features and tagging them as chosen fish
        foreach (GameObject fish in fishInstances)
        {

            FishController fishController = fish.GetComponent<FishController>();

            if (chosenFishController.primaryColor[0] == fishController.primaryColor[0]
                && chosenFishController.secondaryColor[0] == fishController.secondaryColor[0]
                && chosenFishController.pattern[0].name == fishController.pattern[0].name) 
            {
                fishController.chosenFish = true;
            }
            else fishController.chosenFish = false;

        }

    }

    public IEnumerator AddNewFish()
    {
        //When previously found fish is destroyed, this function is called to fill out the spawnpoint slot
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject spawn in spawnpoints)
        {
            if(spawn.transform.childCount == 0) //If spawnpoint has no child, it gets a new one
            {
                GameObject fishInstance = Instantiate(fishPrefab[Random.Range(0, fishPrefab.Length)], spawn.transform);
                fishInstance.name = fishInstance.name + spawn.name;
                fishInstances.Add(fishInstance);
                break;
            }

        }

        StartCoroutine(RollDice(0f));
    }

    public void ResetGame()
    {
        StopAllCoroutines();

        foreach (GameObject fish in fishInstances) Destroy(fish);

        timer = 0;
        fishInstances.Clear();
        dicePrimary.gameObject.SetActive(false);
        diceSecondary.gameObject.SetActive(false);
        dicePattern.gameObject.SetActive(false);
        gameON = false;
    }
}
