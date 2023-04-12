using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBerries : MonoBehaviour
{
    private Transform dragging = null;
    private Vector3 offset;
    [SerializeField] private LayerMask movableLayers;
    public bool birdHasBerry = false;
    //BerryCheck spawnBerry;
    //BerryCheck[] berries;
    //public List<bool> birdBerryCheck;

    private void Awake()
    {
        //spawnBerry = GameObject.Find("BerryManager").GetComponent<SpawnBerry>();
        //spawnBerry = GameObject.FindWithTag("Collectible").GetComponents<BerryCheck>();
        //birdBerryCheck = new List<bool> { false, false, false, false, false, false, false, false, false, false, false, false};
        //berries = spawnBerry.GetComponentsInChildren<BerryCheck>();

    }

    // Update is called once per frame
    void Update()
    {
        // TO DO: katso mit‰ t‰ss‰ voisi tehd‰, kun pelaaja ottaa marjan ja linnulla on marja
        // pelaajan marja j‰‰ paikalleen kunnes lintu ja linnun marja on tuhoutunut

        // HUOM kun lintuu ottaa marjan, voiko marjan layerin/tagin muuttaa, jolloin pelaaja ei en‰‰ voi liikuttaa sit‰ tms.?


        //for(int i = 0; i < berries.Length; i++ )
        //{
        //    if(berries[i].birdHasBerry == true)
        //    {
        //        birdBerryCheck[i] = true;
        //    }
        //}




        if (dragging == null && gameObject.GetComponent<BerryCheck>().berryLayingAround == true && birdHasBerry == false)
        {
                MoveBerryBack();
            // almost works, but the bird behaviour breaks if this is used like this. figure it out
            
        }

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
                        dragging.transform.parent = null;
                    }
                    break;

                case TouchPhase.Ended:
                    dragging = null;
                    gameObject.GetComponent<BerryCheck>().berryLayingAround = true;
                    
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
