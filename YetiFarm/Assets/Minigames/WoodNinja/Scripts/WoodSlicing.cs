using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSlicing : MonoBehaviour
{
    private List<bool> flagList;

    public List<PartColliderBehavior> colliderList;
    public List<GameObject> woodPartList;
    public List<GameObject> birchPartList;
    public bool woodType;
    private float destructionTimer = 2.0f;


    private void Start()
    {
        flagList = new List<bool> { false, false, false, false, false, false, false, false };
    }

    private void FindLargestTimerIndex(int index1, int index2, int index3)
    {
        if (colliderList[index1].cutTimer > colliderList[index2].cutTimer && colliderList[index1].cutTimer > colliderList[index3].cutTimer)
        {
            flagList[index2] = false;
            colliderList[index2].StopCounter();
            colliderList[index2].cutOn = false;
            colliderList[index2].cutTimer = 0.0f;
            flagList[index3] = false;
            colliderList[index3].StopCounter();
            colliderList[index3].cutOn = false;
            colliderList[index3].cutTimer = 0.0f;
        }
        else if (colliderList[index3].cutTimer > colliderList[index1].cutTimer && colliderList[index3].cutTimer > colliderList[index2].cutTimer)
        {
            flagList[index1] = false;
            colliderList[index1].StopCounter();
            colliderList[index1].cutOn = false;
            colliderList[index1].cutTimer = 0.0f;
            flagList[index2] = false;
            colliderList[index2].StopCounter();
            colliderList[index2].cutOn = false;
            colliderList[index2].cutTimer = 0.0f;
        }
        else
        {
            flagList[index1] = false;
            colliderList[index1].StopCounter();
            colliderList[index1].cutOn = false;
            colliderList[index1].cutTimer = 0.0f;
            flagList[index3] = false;
            colliderList[index3].StopCounter();
            colliderList[index3].cutOn = false;
            colliderList[index3].cutTimer = 0.0f;
        }

    }

    private void Update()
    {
        for(int i = 0; i < 8; i++)
        {
            flagList[i] = colliderList[i].cutOn;
        }

        if ((flagList[6] || flagList[7]) && (!flagList[0] && !flagList[1] && !flagList[2] && !flagList[3] && !flagList[4] && !flagList[5]))
        {
            if (flagList[6] && !flagList[7])
            {
                gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(1, 1) * (3), new Vector2(0, 0), ForceMode2D.Impulse);
            }
            else if (!flagList[6] && flagList[7])
            {
                gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(-1, 1) * (3), new Vector2(0, 0), ForceMode2D.Impulse);
            }
            else
            {
                float r = Random.Range(-1, 1);
                gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(r, 1) * (3), new Vector2(0, 0), ForceMode2D.Impulse);
            }
            for (int i = 0; i < 8; i++)
            {
                flagList[i] = false;
                colliderList[i].StopCounter();
                colliderList[i].cutOn = false;
                colliderList[i].cutTimer = 0.0f;
            }
            Destroy(gameObject, destructionTimer);
        }
        else if ((flagList[6] || flagList[7]) && (flagList[0] || flagList[1] || flagList[2] || flagList[3] || flagList[4] || flagList[5]))
        {
            for (int i = 0; i < 8; i++)
            {
                flagList[i] = false;
                colliderList[i].StopCounter();
                colliderList[i].cutOn = false;
                colliderList[i].cutTimer = 0.0f;
            }
        }


        if (((flagList[0] || flagList[1] || flagList[2]) && (flagList[3] || flagList[4] || flagList[5])) && (!flagList[6] || !flagList[7])) {
            switch (flagList[0], flagList[1], flagList[2])
            {
                case (true, true, true):
                    FindLargestTimerIndex(0, 1, 2);
                    break;

                case (true, true, false):
                    FindLargestTimerIndex(0, 1, 2);
                    break;

                case (true, false, true):
                    FindLargestTimerIndex(0, 1, 2);
                    break;

                case (false, true, true):
                    FindLargestTimerIndex(0, 1, 2);
                    break;

                default:
                    break;
            }
            switch (flagList[3], flagList[4], flagList[5])
            {
                case (true, true, true):
                    FindLargestTimerIndex(3, 4, 5);
                    break;

                case (true, true, false):
                    FindLargestTimerIndex(3, 4, 5);
                    break;

                case (true, false, true):
                    FindLargestTimerIndex(3, 4, 5);
                    break;

                case (false, true, true):
                    FindLargestTimerIndex(3, 4, 5);
                    break;

                default:
                    break;
            }
            Slice();
        }
    }

    private void Slice()
    {
        float timerDifference = 0;
        GameObject leftPiece = null;
        GameObject rightPiece = null;

        if (woodType) // Birch wood parts
        {
            switch (flagList[0], flagList[1], flagList[2], flagList[3], flagList[4], flagList[5])
            {
                case (true, false, false, true, false, false):
                    // Cut from top left to bottom left
                    leftPiece = Instantiate(birchPartList[0], transform.position, transform.rotation);
                    rightPiece = Instantiate(birchPartList[1], transform.position, transform.rotation);
                    timerDifference = colliderList[0].cutTimer - colliderList[3].cutTimer;
                    break;

                case (true, false, false, false, true, false):
                    // Cut from top left to bottom mid
                    leftPiece = Instantiate(birchPartList[2], transform.position, transform.rotation);
                    rightPiece = Instantiate(birchPartList[3], transform.position, transform.rotation);
                    timerDifference = colliderList[0].cutTimer - colliderList[4].cutTimer;
                    break;

                case (true, false, false, false, false, true):
                    // Cut from top left to bottom right
                    leftPiece = Instantiate(birchPartList[2], transform.position, transform.rotation);
                    rightPiece = Instantiate(birchPartList[3], transform.position, transform.rotation);
                    timerDifference = colliderList[0].cutTimer - colliderList[5].cutTimer;
                    break;

                case (false, true, false, true, false, false):
                    // Cut from top mid to bottom left
                    leftPiece = Instantiate(birchPartList[6], transform.position, transform.rotation);
                    rightPiece = Instantiate(birchPartList[7], transform.position, transform.rotation);
                    timerDifference = colliderList[1].cutTimer - colliderList[3].cutTimer;
                    break;

                case (false, true, false, false, true, false):
                    // Cut from top mid to bottom mid
                    leftPiece = Instantiate(birchPartList[4], transform.position, transform.rotation);
                    rightPiece = Instantiate(birchPartList[5], transform.position, transform.rotation);
                    timerDifference = colliderList[1].cutTimer - colliderList[4].cutTimer;
                    break;

                case (false, true, false, false, false, true):
                    // Cut from top mid to bottom right
                    leftPiece = Instantiate(birchPartList[2], transform.position, transform.rotation);
                    rightPiece = Instantiate(birchPartList[3], transform.position, transform.rotation);
                    timerDifference = colliderList[1].cutTimer - colliderList[5].cutTimer;
                    break;

                case (false, false, true, true, false, false):
                    // Cut from top right to bottom left
                    leftPiece = Instantiate(birchPartList[6], transform.position, transform.rotation);
                    rightPiece = Instantiate(birchPartList[7], transform.position, transform.rotation);
                    timerDifference = colliderList[2].cutTimer - colliderList[3].cutTimer;
                    break;

                case (false, false, true, false, true, false):
                    // Cut from top right to bottom mid
                    leftPiece = Instantiate(birchPartList[6], transform.position, transform.rotation);
                    rightPiece = Instantiate(birchPartList[7], transform.position, transform.rotation);
                    timerDifference = colliderList[2].cutTimer - colliderList[4].cutTimer;
                    break;

                case (false, false, true, false, false, true):
                    // Cut from top right to bottom right
                    leftPiece = Instantiate(birchPartList[8], transform.position, transform.rotation);
                    rightPiece = Instantiate(birchPartList[9], transform.position, transform.rotation);
                    timerDifference = colliderList[2].cutTimer - colliderList[5].cutTimer;
                    break;

                default:
                    // No cut in the given cut timeframe.
                    break;
            }
        }
        else // brown wood parts
        {
            switch (flagList[0], flagList[1], flagList[2], flagList[3], flagList[4], flagList[5])
            {
                case (true, false, false, true, false, false):
                    // Cut from top left to bottom left
                    leftPiece = Instantiate(woodPartList[0], transform.position, transform.rotation);
                    rightPiece = Instantiate(woodPartList[1], transform.position, transform.rotation);
                    timerDifference = colliderList[0].cutTimer - colliderList[3].cutTimer;
                    break;

                case (true, false, false, false, true, false):
                    // Cut from top left to bottom mid
                    leftPiece = Instantiate(woodPartList[2], transform.position, transform.rotation);
                    rightPiece = Instantiate(woodPartList[3], transform.position, transform.rotation);
                    timerDifference = colliderList[0].cutTimer - colliderList[4].cutTimer;
                    break;

                case (true, false, false, false, false, true):
                    // Cut from top left to bottom right
                    leftPiece = Instantiate(woodPartList[2], transform.position, transform.rotation);
                    rightPiece = Instantiate(woodPartList[3], transform.position, transform.rotation);
                    timerDifference = colliderList[0].cutTimer - colliderList[5].cutTimer;
                    break;

                case (false, true, false, true, false, false):
                    // Cut from top mid to bottom left
                    leftPiece = Instantiate(woodPartList[6], transform.position, transform.rotation);
                    rightPiece = Instantiate(woodPartList[7], transform.position, transform.rotation);
                    timerDifference = colliderList[1].cutTimer - colliderList[3].cutTimer;
                    break;

                case (false, true, false, false, true, false):
                    // Cut from top mid to bottom mid
                    leftPiece = Instantiate(woodPartList[4], transform.position, transform.rotation);
                    rightPiece = Instantiate(woodPartList[5], transform.position, transform.rotation);
                    timerDifference = colliderList[1].cutTimer - colliderList[4].cutTimer;
                    break;

                case (false, true, false, false, false, true):
                    // Cut from top mid to bottom right
                    leftPiece = Instantiate(woodPartList[2], transform.position, transform.rotation);
                    rightPiece = Instantiate(woodPartList[3], transform.position, transform.rotation);
                    timerDifference = colliderList[1].cutTimer - colliderList[5].cutTimer;
                    break;

                case (false, false, true, true, false, false):
                    // Cut from top right to bottom left
                    leftPiece = Instantiate(woodPartList[6], transform.position, transform.rotation);
                    rightPiece = Instantiate(woodPartList[7], transform.position, transform.rotation);
                    timerDifference = colliderList[2].cutTimer - colliderList[3].cutTimer;
                    break;

                case (false, false, true, false, true, false):
                    // Cut from top right to bottom mid
                    leftPiece = Instantiate(woodPartList[6], transform.position, transform.rotation);
                    rightPiece = Instantiate(woodPartList[7], transform.position, transform.rotation);
                    timerDifference = colliderList[2].cutTimer - colliderList[4].cutTimer;
                    break;

                case (false, false, true, false, false, true):
                    // Cut from top right to bottom right
                    leftPiece = Instantiate(woodPartList[8], transform.position, transform.rotation);
                    rightPiece = Instantiate(woodPartList[9], transform.position, transform.rotation);
                    timerDifference = colliderList[2].cutTimer - colliderList[5].cutTimer;
                    break;

                default:
                    // No cut in the given cut timeframe.
                    break;
            }
        }

        leftPiece.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(-1, 1) * (1 / (timerDifference * 5 + 0.1f)), new Vector2(0, 0), ForceMode2D.Impulse);
        rightPiece.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(1, 1) * (1 / (timerDifference * 5 + 0.1f)), new Vector2(0, 0), ForceMode2D.Impulse);

        Destroy(leftPiece, destructionTimer);
        Destroy(rightPiece, destructionTimer);

        Destroy(gameObject);



    }
    

}
