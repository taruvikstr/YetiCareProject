using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Fish_PlayerBuckets : MonoBehaviour
{
    private Color colorNormal;
    private SpriteRenderer spriteRenderer;
    public int collectedAmount = 0;
    public TMP_Text amountTxt;
    [SerializeField] private Fish_GameManager fish_GameManager;
    [SerializeField] private FishUIController fish_UIController;
    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
        colorNormal = GetComponent<SpriteRenderer>().color;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
                fish_GameManager.AnimateDice(false);
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
        audioManager.PlaySound("FishCollect");
        collectedAmount++;
        amountTxt.text = collectedAmount.ToString();
        fish_GameManager.StartCoroutine("AddNewFish");
    }

    public void ResetFishBucket()
    {
        collectedAmount = 0;
        amountTxt.text = collectedAmount.ToString();
        GetComponent<Collider2D>().enabled = true;
    }

    private IEnumerator FishBucketCooldown() //If wrong fish is dragged to the bucket, the bucket has a cooldown,
                                             //so that the player can't drag multiple fish at the same time
    {
        GetComponent<Collider2D>().enabled = false;
        spriteRenderer.color = new Color(colorNormal.r, colorNormal.g, colorNormal.b, 0.6f);
        yield return new WaitForSeconds(2f);
        GetComponent<Collider2D>().enabled = true;
        spriteRenderer.color = colorNormal;

    }

    private void OnDisable()
    {
        //In case game ended whilst FishBucketCooldown was active, the color needs to be reset.
        spriteRenderer.color = new Color(colorNormal.r, colorNormal.g, colorNormal.b, 1f); 
    }
}
