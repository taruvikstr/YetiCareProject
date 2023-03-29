using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSlicing : MonoBehaviour
{
    
    private bool topLeftFlag = false;
    private bool topRightFlag = false;
    private bool topCenterFlag = false;
    private bool bottomCenterFlag = false;
    private bool bottomLeftFlag = false;
    private bool bottomRightFlag = false;

    public Collider2D topLeftCollider;
    public Collider2D topRightCollider;
    public Collider2D topCenterCollider;
    public Collider2D bottomCenterCollider;
    public Collider2D bottomLeftCollider;
    public Collider2D bottomRightCollider;
    public Collider2D leftSideCollider;
    public Collider2D rightSideCollider;

    private IEnumerator couroutine;

    private void Slice(Vector3 direction, Vector3 position, float force)
    {

        switch (topLeftFlag, topCenterFlag, topRightFlag, bottomLeftFlag, bottomCenterFlag, bottomRightFlag)
        {
            case (true, false, false, true, false, false):
                // Cut from top left to bottom left

                break;

            case (true, false, false, false, true, false):
                // Cut from top left to bottom mid
                break;

            case (true, false, false, false, false, true):
                // Cut from top left to bottom right
                break;

            case (false, true, false, true, false, false):
                // Cut from top mid to bottom left
                break;

            case (false, true, false, false, true, false):
                // Cut from top mid to bottom mid
                break;

            case (false, true, false, false, false, true):
                // Cut from top mid to bottom right
                break;

            case (false, false, true, true, false, false):
                // Cut from top right to bottom left
                break;

            case (false, false ,true, false, true, false):
                // Cut from top right to bottom mid
                break;

            case (false, false, true, false, false, true):
                // Cut from top right to bottom right
                break;

            default:
                // No cut in the given cut timeframe.
                break;
        }



    }




private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction,blade.transform.position, blade.sliceForce);
            //Destroy(gameObject, 2f);
        }
    }
    


}
