using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBasket : MonoBehaviour
{
    private int baskets;
    public float leftBasketConstraintLeft;
    public float leftBasketConstraintRight;
    public float middleBasketConstraintLeft;
    public float middleBasketConstraintRight;
    public float rightBasketConstraintLeft;
    public float rightBasketConstraintRight;
    public float basketConstraint_Y;
    private string basketName;
    public TextMeshProUGUI basketPointText;
    private int basketPoints;


    private void Start()
    {
        basketName = gameObject.name;
        baskets = GameObject.Find("EggSpawnParent").GetComponent<EggSpawnManager>().basketAmount;
        if (baskets == 1)
        {
            middleBasketConstraintLeft = leftBasketConstraintLeft;
            middleBasketConstraintRight = rightBasketConstraintRight;
        }
        if (baskets == 2)
        {
            leftBasketConstraintRight = -0.6f;
            rightBasketConstraintLeft = 0.6f;
        }
        basketPointText.text = "0";
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
            pos.y = basketConstraint_Y;

            switch (basketName)
            {
                case "Basket1":
                    if (pos.x > middleBasketConstraintLeft && pos.x < middleBasketConstraintRight)
                    {
                        transform.position = pos;
                    }
                    break;

                case "Basket2Left":
                    if (pos.x > leftBasketConstraintLeft && pos.x < leftBasketConstraintRight)
                    {
                        transform.position = pos;
                    }
                    break;

                case "Basket2Right":
                    if (pos.x > rightBasketConstraintLeft && pos.x < rightBasketConstraintRight)
                    {
                        transform.position = pos;
                    }
                    break;

                case "Basket3Left":
                    if (pos.x > leftBasketConstraintLeft && pos.x < leftBasketConstraintRight)
                    {
                        transform.position = pos;
                    }
                    break;

                case "Basket3Middle":
                    if (pos.x > middleBasketConstraintLeft && pos.x < middleBasketConstraintRight)
                    {
                        transform.position = pos;
                    }
                    break;

                case "Basket3Right":
                    if (pos.x > rightBasketConstraintLeft && pos.x < rightBasketConstraintRight)
                    {
                        transform.position = pos;
                    }
                    break;

                default:
                    break;
            }
        }
    }

    public void BasketPointIncrease()
    {
        basketPoints++;
        basketPointText.text = basketPoints.ToString();
    }
}