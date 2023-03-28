using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSlicing : MonoBehaviour
{


    // private collider, nimeä uudelleen
    // switch case, 
    // is trigger mini colliderit
    // kaikki boolit tähän --
    private bool topLeftCorner = false;


/*  public Collider2D topLeftCollider;
    public Collider2D topRightCollider;
    public Collider2D topLeftCollider;
    public Collider2D topLeftCollider;
    public Collider2D topLeftCollider;
    public Collider2D topLeftCollider;
    public Collider2D topLeftCollider;
    public Collider2D topLeftCollider;
*/
    private Rigidbody2D woodRigidbody;
    private Collider2D woodCollider;
    


    private void Awake()
    {
        woodRigidbody = GetComponent<Rigidbody2D>();
        woodCollider = GetComponent<Collider2D>();
        
    }

 /* private void Slice(Vector3 direction, Vector3 position, float force)
    {
        whole.SetActive(false);
        sliced.SetActive(true);

        woodCollider.enabled = false;


        Rigidbody2D[] slices = sliced.GetComponentsInChildren<Rigidbody2D>();
        foreach(Rigidbody2D slice in slices)
        {
            
            slice.AddForceAtPosition(direction * force, position, ForceMode2D.Impulse);
        }
    }
 */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
          //Slice(blade.direction,blade.transform.position, blade.sliceForce);
            Destroy(gameObject, 2f);
        }
    }
    


}
