using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BerryCheck : MonoBehaviour
{
    public GameObject spawnOrigin = null; // berry knows its spawnpoint
    public bool berryLayingAround = false;
    //[SerializeField] private ParticleSystem leaves;

    private Dictionary<int, Transform> draggingObjects = new Dictionary<int, Transform>();
    private Vector3 offset;
    public LayerMask movableLayers;
    public bool birdHasBerry = false;

    // Update is called once per frame
    void Update()
    {
        if (draggingObjects.Count == 0 && berryLayingAround == true && birdHasBerry == false && gameObject.transform.parent == null)
        {
            MoveBerryBack();
        }

        int touchCount = Input.touchCount;
        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // touch is being detected on screen
                    // cast ray, restrict the functionality to objects on "Movable" -layer 
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position),
                        Vector2.zero, float.PositiveInfinity, movableLayers);

                    if (hit)
                    {
                        FindObjectOfType<AudioManager>().PlaySound("BerryPick");
                        
                        birdHasBerry = false;
                        int touchID = touch.fingerId;
                        if (!draggingObjects.ContainsKey(touchID))
                        {
                            
                            Transform dragging = hit.transform;
                            if (dragging.transform.childCount >= 1)
                            {
                                GameObject grabbed = dragging.transform.GetChild(0).gameObject;

                                grabbed.transform.parent = null;
                                dragging = grabbed.transform;
                                dragging.parent = null;
                            }
                            dragging.transform.parent = null;
                            offset = dragging.position - Camera.main.ScreenToWorldPoint(touch.position);
                            draggingObjects.Add(touchID, dragging);
                        }                     
                    }

                    break;

                case TouchPhase.Moved:
                    // touch is moving across screen
                    if (draggingObjects.ContainsKey(touch.fingerId))
                    {
                        Transform dragging = draggingObjects[touch.fingerId];

                        if (dragging)
                        {
                            dragging.position = Camera.main.ScreenToWorldPoint(touch.position) + offset;
                            dragging.transform.parent = null;
                            //leaves.Play();
                            berryLayingAround = true;


                            //gameObject.GetComponent<BerryCheck>().spawnOrigin.GetComponent<SpawnBerry>().StartParticleEffect();
                            //spawnOrigin.GetComponent<SpawnBerry>().StartParticleEffect();
                        }

                    }
                    break;

                case TouchPhase.Ended:
                    if (draggingObjects.ContainsKey(touch.fingerId))
                    {
                        draggingObjects.Remove(touch.fingerId);
                    }
                    break;
                    //case TouchPhase.Canceled:
                    // touch ended or cancelled, remove from dictionary

            }
        }
    }

    public void OnSteal(GameObject collision)
    {
        // bird is stealing the berry
        transform.parent = collision.gameObject.transform;
        berryLayingAround = false;
    }

    public void MoveBerryBack()
    {
        // when you drag berry away from its spawnpoint and release it before the right bucket,
        // it moves back to spawnpoint (or in this case, spawnOrigin)
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