using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBerries : MonoBehaviour
{
    private Transform dragging = null;
    private Vector3 offset;
    [SerializeField] private LayerMask movableLayers;
    public bool birdHasBerry = false;

    // Update is called once per frame
    void Update()
    {
        // TO DO: katso mit‰ t‰ss‰ voisi tehd‰, kun pelaaja ottaa marjan ja linnulla on marja
        // pelaajan marja j‰‰ paikalleen kunnes lintu ja linnun marja on tuhoutunut

        if (dragging == null && gameObject.GetComponent<BerryCheck>().berryLayingAround == true && birdHasBerry == false)
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
                    gameObject.GetComponent<BerryCheck>().berryLayingAround = true;
                    
                    break;

            }
        }
    }

    public void MoveBerryBack()
    {
        // when you drag berry away from its spawnpoint and release it before the right bucket, it moves back to spawnpoint (or in this case, spawnOrigin)

        Vector3 parentPos = GetComponent<BerryCheck>().spawnOrigin.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, parentPos, 10f * Time.deltaTime);


    }
}
