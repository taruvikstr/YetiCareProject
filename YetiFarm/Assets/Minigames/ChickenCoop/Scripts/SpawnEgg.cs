using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEgg : MonoBehaviour
{
    public GameObject egg;
    public SpriteRenderer idle;
    public SpriteRenderer laying;
    public void SpawnSingleEgg()
    {
        StartCoroutine(PlayLayingAnimation());


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

    public IEnumerator PlayLayingAnimation()
    {
        idle.enabled = false;
        laying.enabled = true;
        yield return new WaitForSeconds(0.3f);
        Instantiate(egg, transform.position, transform.rotation); // Spawn an egg.
        yield return new WaitForSeconds(0.3f);
        laying.enabled = false;
        idle.enabled = true;
    }
}
