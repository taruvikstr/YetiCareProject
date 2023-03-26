using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fish_PlayerBowls : MonoBehaviour
{
    public int fishAmount = 0;
    public TMP_Text amountTxt;
    private Fish_GameManager fish_GameManager;

    private void Start()
    {
        fish_GameManager = GameObject.Find("GameManager").GetComponent<Fish_GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject fish = collision.gameObject;
        FishController fishController = fish.GetComponent<FishController>();
        if (fish.CompareTag("Collectible"))
        {
            if(fishController.chosenFish)
            {
                Debug.Log("More fishes for " + gameObject.name);
                IncreaseFishAmount();
                fish_GameManager.fishInstances.Remove(fish);
                Destroy(collision.gameObject, 0.2f);
                fish_GameManager.RollDice();
            }
   
        }
    }

    private void IncreaseFishAmount()
    {
        fishAmount++;
        amountTxt.text = fishAmount.ToString();
    }

    public void ResetFishBowl()
    {
        fishAmount = 0;
        amountTxt.text = fishAmount.ToString();
    }
}
