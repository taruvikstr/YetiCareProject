using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> fishBucket = new List<GameObject>();
    [SerializeField] private GameObject gameCanvas, gameEnd;
    [SerializeField] private GameObject[] placements;
    [SerializeField] private Image[] placementImage;
    [SerializeField] private TMP_Text[] scoreTXT;
    
    public void ActivatePlayer(int amount) // Activating buckets according to the player amount
    {
        for(int i = 0; i < amount; i++) fishBucket[i].SetActive(true);
    }

    public void SetPlacements() // Game time ended and the score is being set and displayed
    {
        int index = 0;
        foreach(GameObject bucket in fishBucket)
        {
            if (bucket.gameObject.activeSelf)
            {
                placements[index].SetActive(true);
                placementImage[index].sprite = bucket.GetComponent<SpriteRenderer>().sprite;
                scoreTXT[index].text = bucket.GetComponent<Fish_PlayerBuckets>().collectedAmount.ToString();
            }
            else placements[index].SetActive(false);

            index++;
        }

        gameCanvas.SetActive(false);
        gameEnd.SetActive(true);

        foreach (GameObject bucket in fishBucket) // deactivating the buckets
        {
            bucket.SetActive(false);
        }

        index = 0;
    }
}
