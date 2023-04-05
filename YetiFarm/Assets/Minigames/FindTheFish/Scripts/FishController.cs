using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [Header ("Features")]
    public List<Sprite> pattern = new List<Sprite>();
    public List<Color> primaryColor = new List<Color>();
    public List<Color> secondaryColor = new List<Color>();

    [SerializeField] private SpriteRenderer primaryColorRenderer, patternRenderer;

    GameObject spawnParent;

    private Fish_GameManager gameManager;

    public bool isDragged = false;
    public bool returned = true;
    public bool chosenFish = false;

    [SerializeField] private float speed;

    private bool flipped;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Fish_GameManager>();

        //while(gameManager.patternAmount < pattern.Count)
        //{
        //    tähän patternien poisto loppupäästä
        //}

        spawnParent = transform.parent.gameObject;

        if (transform.localScale.x == -1) flipped = true;
        else flipped = false;

        speed = UnityEngine.Random.Range(0.05f, 1f);

        ShuffleFeatures();
        RefreshFish();
    }

    private void Update()
    {
        if (transform.position == spawnParent.transform.position)
        {
            returned = true;
        }

        if (!isDragged && returned) Movement();
        else if (!isDragged && !returned) MoveFishBackToSea();
 
    }

    private void ShuffleFeatures()
    {
        primaryColor = primaryColor.OrderBy(i => Guid.NewGuid()).ToList();
        secondaryColor = secondaryColor.OrderBy(i => Guid.NewGuid()).ToList();
        pattern = pattern.OrderBy(i => Guid.NewGuid()).ToList();
    }

    private void RefreshFish()
    {
        primaryColorRenderer.color = primaryColor[0];
        patternRenderer.color = secondaryColor[0];
        patternRenderer.sprite = pattern[0];
    }

    private void MoveFishBackToSea() //Found item is moved to it's original position if released
    {
        Vector3 parentPos = spawnParent.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, parentPos, 10f * Time.deltaTime);
    }

    private void Movement()
    {
        if (!flipped) transform.Translate(Vector3.left * (speed * Time.deltaTime));
        else transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water")) //Colliders on the edges of water area have this tag
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        if (!flipped)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            flipped = true;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            flipped = false;
        }

    }
}
