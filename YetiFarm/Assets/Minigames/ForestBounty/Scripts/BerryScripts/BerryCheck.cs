using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BerryCheck : MonoBehaviour
{
    private Dictionary<int, Transform> draggingObjects = new Dictionary<int, Transform>();
    private Vector3 offset; 
    public LayerMask movableLayers;

    public GameObject spawnOrigin = null;
    public bool berryLayingAround = false;
    public bool birdHasBerry = false;


    void Update()
    {
        // MoveBerryBack function using
        if (!draggingObjects.ContainsValue(gameObject.transform) && berryLayingAround == true 
            && birdHasBerry == false && gameObject.transform.parent == null)
        {
            MoveBerryBack();
        }

        int touchCount = Input.touchCount;

        // Game's touch and multitouch 
        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                // Touch is being detected on screen
                case TouchPhase.Began:
                    // Cast ray, restrict the functionality to objects on "Movable" -layer 
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, float.PositiveInfinity, movableLayers);

                    if (hit)
                    {
                        FindObjectOfType<AudioManager>().PlaySound("BerryPick");
                        birdHasBerry = false;
                        int touchID = touch.fingerId;

                        if (!draggingObjects.ContainsKey(touchID))
                        {
                            Transform dragging = hit.transform;
                            dragging.parent = null;

                            if (transform.parent == null)
                            {
                                birdHasBerry = false;
                            }
                            if (transform.parent != null)
                            {
                                birdHasBerry = true;
                            }

                            // Particle effect
                            if (dragging.position == spawnOrigin.transform.position)
                            {
                                dragging.gameObject.GetComponent<ParticleSystem>().Play();
                            }
                            dragging.transform.parent = null;
                            offset = dragging.position - Camera.main.ScreenToWorldPoint(touch.position);
                            draggingObjects.Add(touchID, dragging);
                        }                     
                    }
                    break;

                // Touch is moving across screen
                case TouchPhase.Moved:
                    if (draggingObjects.ContainsKey(touch.fingerId))
                    {
                        Transform dragging = draggingObjects[touch.fingerId];

                        if (dragging)
                        {
                            dragging.position = Camera.main.ScreenToWorldPoint(touch.position) + offset;
                            dragging.transform.parent = null;
                            berryLayingAround = true;
                        }
                    }
                    break;

                // Touch is no longer detected
                case TouchPhase.Ended:
                    if (draggingObjects.ContainsKey(touch.fingerId))
                    {
                        draggingObjects.Remove(touch.fingerId);
                    }
                    break;
            }
        }
    }

    // Bird is stealing the berry
    public void OnSteal(GameObject collision)
    {
        transform.parent = collision.gameObject.transform;
        berryLayingAround = false;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    // When you drag berry away from its spawnpoint and release it before the right bucket,
    // it moves back to spawnpoint (spawnOrigin)
    public void MoveBerryBack()
    { 
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Vector3 parentPos = spawnOrigin.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, parentPos, 10f * Time.deltaTime);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

        if (transform.position.Equals(parentPos))
        {
            berryLayingAround = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}