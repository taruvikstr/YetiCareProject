using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (dragging == null && berryLayingAround == true && birdHasBerry == false && gameObject.transform.parent == null)
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
                        birdHasBerry = false;
                        dragging = hit.transform;
                        if (dragging.transform.childCount >= 1)
                        {
                            GameObject grabbed = dragging.transform.GetChild(0).gameObject;
                            grabbed.transform.parent = null;
                            dragging = grabbed.transform;
                            grabbed = null;
                            dragging.parent = null;
                        }
                        dragging.transform.parent = null;
                        offset = dragging.position - Camera.main.ScreenToWorldPoint(touch.position);
                    }

                    break;

                case TouchPhase.Moved:

                    // touch is moving across screen
                    if (dragging)
                    {
                        dragging.position = Camera.main.ScreenToWorldPoint(touch.position) + offset;
                        dragging.transform.parent = null;
                        berryLayingAround = true;
                    }
                    break;

                case TouchPhase.Ended:

                    // screen is not detecting touch
                    dragging = null;

                    break;
            }
        }
}

    public void OnSteal(GameObject collision)
    {
        // bird is stealing the berry
        gameObject.transform.parent = collision.gameObject.transform;
        berryLayingAround = false;           
    }

    public void MoveBerryBack()
    {
        // when you drag berry away from its spawnpoint and release it before the right bucket, it moves back to spawnpoint (or in this case, spawnOrigin)
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Vector3 parentPos = spawnOrigin.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, parentPos, 10f * Time.deltaTime);

        if (transform.position.Equals(parentPos))
        {
            berryLayingAround = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }           
    }
}
