using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Fish_PlayerBuckets : MonoBehaviour
{
    public int collectedAmount = 0;
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
                fishController.enabled = false;
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
        collectedAmount++;
        amountTxt.text = collectedAmount.ToString();
        fish_GameManager.StartCoroutine("AddNewFish");
    }

    public void ResetFishBucket()
    {
        collectedAmount = 0;
        amountTxt.text = collectedAmount.ToString();
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
