using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEgg : MonoBehaviour
{
    public GameObject easyEgg;
    public GameObject mediumEgg;
    public GameObject hardEgg;
    public SpriteRenderer idle;
    public SpriteRenderer laying;
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
        idle.enabled = false;
        laying.enabled = true;
        if (diff == 1)
        {
            yield return new WaitForSeconds(0.6f);
            Instantiate(easyEgg, transform.position, transform.rotation); // Spawn an egg.
            yield return new WaitForSeconds(0.6f);
        }
        if (diff == 2)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(mediumEgg, transform.position, transform.rotation); // Spawn an egg.
            yield return new WaitForSeconds(0.5f);
        }
        if (diff == 3)
        {
            yield return new WaitForSeconds(0.4f);
            Instantiate(mediumEgg, transform.position, transform.rotation); // Spawn an egg.
            yield return new WaitForSeconds(0.4f);
        }
        laying.enabled = false;
        idle.enabled = true;
    }
}
