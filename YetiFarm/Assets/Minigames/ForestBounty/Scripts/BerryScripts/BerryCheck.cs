using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryCheck : MonoBehaviour
{
    public GameObject spawnOrigin = null; // berry knows its spawnpoint
    public bool berryLayingAround = false;

    // DragBerries stuff: 
    private Transform dragging = null;
    private Vector3 offset;
    [SerializeField] private LayerMask movableLayers;
    public bool birdHasBerry = false;

    // Update is called once per frame
    void Update()
    {
        // TO DO: katso mitä tässä voisi tehdä, kun pelaaja ottaa marjan ja linnulla on marja
        // pelaajan marja jää paikalleen kunnes lintu ja linnun marja on tuhoutunut

        if (dragging == null && berryLayingAround == true && birdHasBerry == false)
        {
            MoveBerryBack();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // store detected touch. only the first one, id there is multiple touch actions at once. 

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // touch is being detected on screen
                    // cast ray, restrict the functionality to objects on "Movable" -layer 
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, float.PositiveInfinity, movableLayers);

                    if (hit)
                    {
                        dragging = hit.transform;
                        offset = dragging.position - Camera.main.ScreenToWorldPoint(touch.position);
                    }

                    break;

                case TouchPhase.Moved:
                    // touch is moving across screen
                    if (dragging)
                    {
                        dragging.position = Camera.main.ScreenToWorldPoint(touch.position) + offset;
                        dragging.transform.parent = null;
                    }
                    break;

                case TouchPhase.Ended:
                    // screen is not detecting touch
                    dragging = null;
                    berryLayingAround = true;

                    break;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // bird is stealing the berry
        if (collision.CompareTag("ProjectileTag")) // collision with bird object
        {
            gameObject.transform.parent = collision.gameObject.transform;
            collision.gameObject.GetComponent<BirdManager>().StealBerry();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            berryLayingAround = false;
        }
        
    }

    public void MoveBerryBack()
    {
        // when you drag berry away from its spawnpoint and release it before the right bucket, it moves back to spawnpoint (or in this case, spawnOrigin)

        Vector3 parentPos = spawnOrigin.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, parentPos, 10f * Time.deltaTime);

    }
}
