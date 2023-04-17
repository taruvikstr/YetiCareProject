using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEgg : MonoBehaviour
{
    public GameObject easyEgg;
    public GameObject mediumEgg;
    public GameObject hardEgg;
    public SpriteRenderer idle;
    public List<SpriteRenderer> laying;


    public void SpawnSingleEgg(int diff)
    {
        StartCoroutine(PlayLayingAnimation(diff));


        /*
        try
            {
                GameObject.Find("AudioManager").GetComponent<SoundManager>().Play("EnemyShoot");
            }
            catch
            {
                Debug.Log("Debug alert - Audio Manager not found.");
            }
        */
    }

    public IEnumerator PlayLayingAnimation(int diff)
    {
        float animationTime = 0;
        idle.enabled = false;
        laying[0].enabled = true;

        if (diff == 1)
        {
            animationTime = 1.2f / 14;
        }
        if (diff == 2)
        {
            animationTime = 1.0f / 14;
        }
        if (diff == 3)
        {
            animationTime = 0.8f / 14;
        }

        for (int i = 1; i < laying.Count; i++)
        {
            laying[i].enabled = true;
            laying[i - 1].enabled = false;
            yield return new WaitForSeconds(animationTime);
        }
        yield return new WaitForSeconds(animationTime * 2);
        Instantiate(easyEgg, transform.position, transform.rotation); // Spawn an egg.
        for (int i = laying.Count - 1; i > 0; i--)
        {
            laying[i].enabled = false;
            laying[i - 1].enabled = true;
            yield return new WaitForSeconds(animationTime);
        }

        laying[0].enabled = false;
        idle.enabled = true;
    }
}
