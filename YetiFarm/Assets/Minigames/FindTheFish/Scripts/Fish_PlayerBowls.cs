using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fish_PlayerBowls : MonoBehaviour
{
    public int fishAmount = 0;
    public TMP_Text amountTxt;
    [SerializeField] private Fish_GameManager fish_GameManager;
    [SerializeField] private FishUIController fish_UIController;

    private void OnEnable()
    {
        ResetFishBucket();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        GameObject fish = collision.gameObject;

        if (fish.CompareTag("Collectible"))
        {
            
            FishController fishController = fish.GetComponent<FishController>();

            if (fishController.chosenFish)
            {
                IncreaseFishAmount();
                fish_GameManager.fishInstances.Remove(fish);
                Destroy(collision.gameObject, 0.1f);
                
            }
            else
            {
                StartCoroutine("FishBucketCooldown");
            }
   
        }
    }

    private void IncreaseFishAmount()
    {
        fishAmount++;
        amountTxt.text = fishAmount.ToString();
        if(fishAmount == fish_UIController.fishToWin)
        {
            fish_UIController.SetPlacements();
            fish_GameManager.ResetGame();
        }
        else fish_GameManager.StartCoroutine("AddNewFish");
    }

    public void ResetFishBucket()
    {
        fishAmount = 0;
        amountTxt.text = fishAmount.ToString();
    }

    private IEnumerator FishBucketCooldown() //If wrong fish is dragged to the bucket, the bucket has a cooldown,
                                             //so that the player can't drag multiple fish at the same time
    {
        Color colorNormal = GetComponent<SpriteRenderer>().color;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(2f);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = colorNormal;

    }
}
