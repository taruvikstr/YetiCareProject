using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> fishBucket = new List<GameObject>();
    [SerializeField] private GameObject gameCanvas, gameEnd;
    [SerializeField] private Fish_GameManager fish_GameManager;
    [SerializeField] private GameObject[] placements;
    [SerializeField] private Image[] placementImage;
    [SerializeField] private TMP_Text[] scoreTXT;
    
    public void ActivatePlayer(int amount)
    {
        for(int i = 0; i < amount; i++) fishBucket[i].SetActive(true);
    }

    public void SetPlacements()
    {
        int index = 0;
        foreach(GameObject bucket in fishBucket)
        {
            if (bucket.gameObject.activeSelf)
            {
                placementImage[index].sprite = bucket.GetComponent<SpriteRenderer>().sprite;
                scoreTXT[index].text = bucket.GetComponent<Fish_PlayerBuckets>().collectedAmount.ToString();
            }
            else placements[index].SetActive(false);

            index++;
        }

        gameCanvas.SetActive(false);
        gameEnd.SetActive(true);

        foreach(GameObject bowl in fishBucket)
        {
            bowl.SetActive(false);
        }
    }
}
