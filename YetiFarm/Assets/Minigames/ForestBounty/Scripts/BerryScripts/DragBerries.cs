using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBerries : MonoBehaviour
{
    private Transform dragging = null;
    private Vector3 offset;
    [SerializeField] private LayerMask movableLayers;

    // Update is called once per frame
    void Update()
    {
        //if (dragging == null && BerryCheck.berryLayingAround)
        //{
        //    //  && BirdManager.berryGrabbed == false
        //    // almost works, but the bird behaviour breaks if this is used like this. figure it out
        //    MoveBerryBack();
        //}

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    // cast ray, restrict the functionality to objects on "Movable" -layer 
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, float.PositiveInfinity, movableLayers);

                    if (hit)
                    {
                        dragging = hit.transform;
                        offset = dragging.position - Camera.main.ScreenToWorldPoint(touch.position);
                    }
                    
                    break;

                case TouchPhase.Moved:
                    if (dragging)
                    {
                        dragging.position = Camera.main.ScreenToWorldPoint(touch.position) + offset;
                    }
                    break;

                case TouchPhase.Ended:
                    dragging = null;
                    BerryCheck.berryLayingAround = true;
                    break;

            }
        }
    }

    public void MoveBerryBack()
    {
        // when you drag berry away from its spawnpoint and release it before the right bucket, it moves back to spawnpoint

        Vector3 parentPos = GetComponent<BerryCheck>().spawnOrigin.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, parentPos, 10f * Time.deltaTime);


    }
}
