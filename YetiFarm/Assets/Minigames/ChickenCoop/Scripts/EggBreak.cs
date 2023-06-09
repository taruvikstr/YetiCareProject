using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBreak : MonoBehaviour
{
    public GameObject brokenEgg;
    private GameObject eggSpawnParent;

    private void Awake()
    {
        eggSpawnParent = GameObject.Find("EggSpawnParent");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            eggSpawnParent.GetComponent<EggSpawnManager>().EggFail();
            Instantiate(brokenEgg, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBasket>().BasketPointIncrease();
            eggSpawnParent.GetComponent<EggSpawnManager>().IncreaseScore();
            Destroy(gameObject);
        }
    }
}
