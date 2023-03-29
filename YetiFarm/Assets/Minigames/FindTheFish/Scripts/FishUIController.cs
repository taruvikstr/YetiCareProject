using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> fishBucket = new List<GameObject>();
    [SerializeField] private Image[] placementImage;

    [SerializeField] private GameObject gamePanel, gameEnd;

    [Range(5, 15)]
    public int fishToWin = 5;
    
    public void ActivatePlayer(int index)
    {
        fishBucket[index].SetActive(true);
    }

    public void SetPlacements()
    {
        int fishAmount_0 = fishBucket[0].GetComponent<Fish_PlayerBowls>().fishAmount;
        int fishAmount_1 = fishBucket[1].GetComponent<Fish_PlayerBowls>().fishAmount;
        int fishAmount_2 = fishBucket[2].GetComponent<Fish_PlayerBowls>().fishAmount;
        int fishAmount_3 = fishBucket[3].GetComponent<Fish_PlayerBowls>().fishAmount;

        //j‰rjest‰ fishbowl lista kalam‰‰rn mukaan, kirjasto (avain ja arvoparit?)

        //placementImage[0].sprite = fishbowl[0].GetComponent<SpriteRenderer>().sprite;
        //placementImage[1].sprite = fishbowl[1].GetComponent<SpriteRenderer>().sprite;
        //placementImage[2].sprite = fishbowl[2].GetComponent<SpriteRenderer>().sprite;
        //placementImage[3].sprite = fishbowl[3].GetComponent<SpriteRenderer>().sprite;

        gamePanel.SetActive(true);
        gameEnd.SetActive(true);

        foreach(GameObject bowl in fishBucket)
        {
            bowl.SetActive(false);
        }
    }
}
