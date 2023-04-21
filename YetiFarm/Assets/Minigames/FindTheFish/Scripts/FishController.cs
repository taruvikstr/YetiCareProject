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
    
    [SerializeField] private ParticleSystem bubbleParticle;
    [SerializeField] private SpriteRenderer primaryColorRenderer, patternRenderer;

    GameObject spawnParent;

    private Fish_GameManager gameManager;
    private AudioManager audioManager;

    private Dictionary<int, Transform> draggingFish = new Dictionary<int, Transform>();
    private Vector3 offset;
    private float previousXpos;
    [SerializeField] private LayerMask movableLayers;

    public bool isDragged = false;
    public bool returned = true;
    public bool chosenFish = false;

    [SerializeField] private float speed;

    private bool flipped;
    private string fishName;

    private void Start()
    {
        fishName = gameObject.name;
        gameManager = FindObjectOfType<Fish_GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        spawnParent = transform.parent.gameObject;

        if (transform.localScale.x == -1) flipped = true;
        else flipped = false;

        StartCoroutine(ChangeFishSortingLayer(transform.parent.GetComponent<SpriteRenderer>().sortingLayerName, gameObject, 0f));

        speed = UnityEngine.Random.Range(0.05f, 1f);

        ShuffleFeatures();
        RefreshFish();
    }

    private void Update()
    {
        //Basic movements, when not being dragged
        if (!isDragged && returned) Movement();
        else if (!isDragged && !returned) MoveFishBackToSea();

        //Touch dragging of the fish
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
                        GameObject grabbed;
                        int touchID = touch.fingerId;
                        if (!draggingFish.ContainsKey(touchID))
                        {
                            Transform dragging = hit.transform;
                            grabbed = dragging.transform.gameObject;
                            dragging = grabbed.transform;
                            if(grabbed.name == fishName)
                            {
                                audioManager.PlaySound("FishGrab");
                                isDragged = true;
                                returned = false;
                                bubbleParticle.Play();
                                StartCoroutine(ChangeFishSortingLayer("Dragged", grabbed.gameObject, 0f)); //Puts the fish on top layer
                                offset = dragging.position - Camera.main.ScreenToWorldPoint(touch.position);
                                draggingFish.Add(touchID, dragging);
                            }
                        }
                    }

                    break;

                case TouchPhase.Moved:

                    // touch is moving across screen
                    if (draggingFish.ContainsKey(touch.fingerId))
                    {
                        Transform dragging = draggingFish[touch.fingerId];

                        if (dragging)
                        {
                            dragging.position = Camera.main.ScreenToWorldPoint(touch.position) + offset;
                            returned = false;
                        }

                    }
                    break;

                case TouchPhase.Ended:
                    if (draggingFish.ContainsKey(touch.fingerId))
                    {
                        StartCoroutine(ChangeFishSortingLayer(spawnParent.GetComponent<SpriteRenderer>().sortingLayerName, gameObject, 2f));
                        draggingFish.Remove(touch.fingerId);
                        isDragged = false;
                        
                    }
                    break;
            }
        }

        //When fish get's back to it's original position, bubbles stop and get's flagged as returned
        if (transform.position == spawnParent.transform.position && !returned) 
        {
            returned = true;
            StopBubbleParticles();
        }

        if(!returned && isDragged)
        {
            if (previousXpos - transform.position.x < 0) // Dragging right flip sprite
            {
                transform.localScale = new Vector3(-1, 1, 1);
                flipped = true;
            }
            else if (previousXpos - transform.position.x > 0) // Dragging left flip srite
            {
                transform.localScale = new Vector3(1, 1, 1);
                flipped = false;
            }

            previousXpos = transform.position.x;
        }
        else if(!returned && !isDragged) // Returning back to water after drag
        {
            if (spawnParent.transform.position.x - transform.position.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                flipped = false;
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                flipped = true;
            }
        }
 
    }


    public IEnumerator ChangeFishSortingLayer(string layerName, GameObject fishInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (fishInstance != null)
        {
            SpriteRenderer[] fishRenderers = fishInstance.transform.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer renderer in fishRenderers)
            {
                renderer.sortingLayerName = layerName;
            }
        }

    }

    private void ShuffleFeatures()
    {
        primaryColor = primaryColor.OrderBy(i => Guid.NewGuid()).ToList();
        secondaryColor = secondaryColor.OrderBy(i => Guid.NewGuid()).ToList();
        int random = UnityEngine.Random.Range(0, gameManager.patternAmount);

        Sprite randomPattern = pattern[random];
        pattern.RemoveAt(random);
        pattern.Insert(0, randomPattern);
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

    public void StartBubbleParticles()
    {
        bubbleParticle.Play();
    }

    public void StopBubbleParticles()
    {
        bubbleParticle.Stop();
    }
}
