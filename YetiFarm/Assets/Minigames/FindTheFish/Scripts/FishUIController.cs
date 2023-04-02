using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> fishBucket = new List<GameObject>();
    [SerializeField] private GameObject gamePanel, gameEnd;
    [SerializeField] private Fish_GameManager fish_GameManager;
    [SerializeField] private GameObject[] placements;
    [SerializeField] private Image[] placementImage;
    [SerializeField] private TMP_Text[] scoreTXT;

    [Range(5, 15)]
    public int fishToWin = 5;
    
    public void ActivatePlayer(int index)
    {
        fishBucket[index].SetActive(true);
        fish_GameManager.playerAmount++;
    }

    public void SetPlacements(bool solo, int fishAmount)
    {
        int index = 0;
        foreach(GameObject bucket in fishBucket)
        {
            if (bucket.gameObject.activeSelf)
            {
                placementImage[index].sprite = bucket.GetComponent<SpriteRenderer>().sprite;
                scoreTXT[index].text = "Score: " + bucket.GetComponent<Fish_PlayerBuckets>().fishAmount.ToString();
            }
            else placements[index].SetActive(false);

            index++;
        }

        gamePanel.SetActive(true);
        gameEnd.SetActive(true);

        foreach(GameObject bowl in fishBucket)
        {
            bowl.SetActive(false);
        }
    }
}
