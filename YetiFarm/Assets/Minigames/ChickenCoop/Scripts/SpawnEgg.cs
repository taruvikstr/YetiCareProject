using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEgg : MonoBehaviour
{
    public GameObject egg;
    public void SpawnSingleEgg()
    {
        Instantiate(egg, transform.position, transform.rotation); // Spawn an egg.

        // TODO - Play egg spawning animation for the parent chicken of the egg spawn point.


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
}
