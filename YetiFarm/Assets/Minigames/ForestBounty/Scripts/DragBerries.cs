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
                    break;
            }
        }
    }
}
