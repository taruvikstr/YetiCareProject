using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mole : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Sprite mole;
    [SerializeField] private Sprite moleHardHat;
    [SerializeField] private Sprite moleHatBroken;
    [SerializeField] private Sprite moleHit;
    [SerializeField] private Sprite moleHatHit;
    // [SerializeField] private ParticleSystem hatSparks;
    [SerializeField] public GameObject moleHands;
    [SerializeField] public GameObject vegetable;
    [SerializeField] public GameObject hat;
    [SerializeField] public GameObject brokenHat;


    [Header("GameManager")]
    [SerializeField] private MoleGameManager gameManager;
    [SerializeField] private TMPro.TextMeshPro grabTimerText;

    public AudioManager audioManager;
    //Sprite offset of the sprite to hide it
    private Vector2 startPosition = new Vector2(0f, -2f); //Pixelsize / PixelsPerUnit
    private Vector2 endPosition = Vector2.zero;
    //How long it takes to show a mole
    private float showDuration = 0.5f;
    public float duration = 1f;
    private bool hittable = true;
    private float grabAnimationDuration = 0f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider2D;

    private Vector2 boxOffset;
    private Vector2 boxSize;
    private Vector2 boxOffsetHidden;
    private Vector2 boxSizeHidden;

    public RuntimeAnimatorController GrabbingAnimation;
    public RuntimeAnimatorController bombAnimation;
    public RuntimeAnimatorController pullingAnim;

    //Mole Parameters

    public enum MoleType { Standard, HardHat, Bomb };
    private MoleType moleType;
    private float hardRate = 0.25f;
    private float bombRate = 0f;
    private int lives;
    private int moleIndex;



    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        // Startposition

        transform.localPosition = start;
        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        //Endposition
        transform.localPosition = end;
        boxCollider2D.offset = boxOffset;
        boxCollider2D.size = boxSize;
        if (moleType != MoleType.Bomb)
        {
            moleHands.SetActive(true);
        }

        //Wait for duration to pass
        yield return new WaitForSeconds(duration);
        moleHands.SetActive(false);


        /*
         * TODO - instead of hiding the mole after the duration, start a new (needs to be created) coroutine that
         * starts the destruction timer of the vegetable next to the mole IF the mole isn't a bomb and plays the animation.
         * Timer and animation speed should be dependent on difficulty value.
         * 
         * This should be implemented in the hide mole section below.
         */
        if (moleType != MoleType.Bomb && vegetable.activeInHierarchy)
        {
            switch (grabAnimationDuration)
            {
                case 1:
                    grabAnimationDuration = 4;
                    break;
                case 2:
                    grabAnimationDuration = 3;
                    break;
                case 3:
                    grabAnimationDuration = 2;
                    break;
                default:
                    break;
            }

            // Start timer for grabbing animation    
            grabTimerText.enabled = true;
           // Debug.Log(grabAnimationDuration);
            //Switch to moleGrabbing animation
            animator.runtimeAnimatorController = GrabbingAnimation;
            animator.enabled = true;
            
            while (grabAnimationDuration > 0f)
            {

                grabTimerText.text = Mathf.Round(grabAnimationDuration).ToString();
                grabAnimationDuration -= Time.deltaTime;
                if(grabAnimationDuration < 2f)
                {
                    animator.runtimeAnimatorController = pullingAnim;
                }
                yield return null;
            }

            if (vegetable.activeInHierarchy)
            {
                gameManager.vegetables -= 1;
                audioManager.PlaySound("VegePick");
            }
            animator.enabled = false;
            vegetable.SetActive(false);
            grabTimerText.enabled = false;
        }



        //Hide mole
        elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
            //Update at max framerate
            elapsed += Time.deltaTime;
            yield return null;
        }
        moleHands.SetActive(false);
        transform.localPosition = start;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;
        //If we got to the end and its still hittable then we missed it.
        if (hittable)
        {
            hittable = false;
            //We only give time penalty if it isnt a bomb
            gameManager.Missed(moleIndex, moleType != MoleType.Bomb);
        }

    }

    public void Hide()
    {
        grabTimerText.enabled = false;
        moleHands.SetActive(false);
        transform.localPosition = startPosition;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;
    }
    private IEnumerator QuickHide()
    {

        yield return new WaitForSeconds(0.25f);
        if (!hittable)
        {
            Hide();
        }

    }

    // Check if the mole is touched/clicked
    private void OnMouseDown()
    {
        if (hittable)
        {
            switch (moleType)
            {
                case MoleType.Standard:
                    //PlayClickSound when a standardmole is active
                    audioManager.PlaySound("Click");


                    moleHands.SetActive(false);
                    Debug.Log("normal hit");
                    spriteRenderer.sprite = moleHit;
                    gameManager.AddScore(moleIndex, moleType != MoleType.Bomb);
                    //Hide mole grabbing timer if mole is hit
                    grabTimerText.enabled = false;
                    //Stop Coroutines
                    StopAllCoroutines();
                    StartCoroutine(QuickHide());
                    //Turn off hittable so that we cant keep tapping for score.
                    hittable = false;
                    break;
                case MoleType.HardHat:
                    if (lives == 2)
                    {
                        Vector2 temp = new Vector2(hat.transform.position.x, hat.transform.position.y+2f);
                        gameManager.HelmetSpark(temp);
                       // brokenHat.SetActive(true);
                        hat.SetActive(false);
                        audioManager.PlaySound("HelmetHit");
                        lives--;
                    }
                    else
                    {
                        // hatSparks.Play();

                        //HatMole sound
                        audioManager.PlaySound("Click");
                        moleHands.SetActive(false);
                        Debug.Log("hatHit");
                        spriteRenderer.sprite = moleHit;
                        gameManager.AddScore(moleIndex, moleType != MoleType.Bomb);
                        //Hide mole grabbing timer if mole is hit
                        grabTimerText.enabled = false;
                        //Stop the animation
                        StopAllCoroutines();
                        StartCoroutine(QuickHide());
                        // Turn off hittable so that we cant keep tapping for score.
                        hittable = false;
                    }
                    break;
                case MoleType.Bomb:

                    //Game over, 1 for bomb.
                    Debug.Log("Bomb hit");
                    if (vegetable.activeInHierarchy)
                    {
                        gameManager.vegetables -= 1;
                    }
                    //Explosion sound
                    audioManager.PlaySound("BombHit");
                    gameManager.BombExplosion(gameObject.transform.position, moleIndex);
                    gameManager.AddScore(moleIndex, moleType != MoleType.Bomb);
                    StopAllCoroutines();
                    StartCoroutine(QuickHide());
                    StartCoroutine(VegetableActiveFalseDelay());
                    animator.enabled = false;
                    hittable = false;

                    break;
                default:
                    break;

            }

        }

    }
    //Hide vegetable delayed
    IEnumerator VegetableActiveFalseDelay()
    {
        yield return new WaitForSeconds(0.5f);
        vegetable.SetActive(false);
    }
    private void CreateNext()
    {
        float random = Random.Range(0f, 1f);
        if (random < bombRate)
        {

            // Make a bomb.
            moleType = MoleType.Bomb;
            hat.SetActive(false);
           // brokenHat.SetActive(false);
            // The animator handles setting the sprite.
            animator.runtimeAnimatorController = bombAnimation;
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
            random = Random.Range(0f, 1f);
            if (random < hardRate)
            {
                // Create a hard one.
                moleType = MoleType.HardHat;
                spriteRenderer.sprite = mole;
                hat.SetActive(true);
               // brokenHat.SetActive(false);
                lives = 2;
            }
            else
            {
                // Create a standard one.
                moleType = MoleType.Standard;
                spriteRenderer.sprite = mole;
                hat.SetActive(false);
               // brokenHat.SetActive(false);
                lives = 1;
            }
        }
        // Mark as hittable so we can register an onclick event.
        hittable = true;
    }
    // As the level progresses the game gets harder.
    private void SetLevel(int level)
    {
        //As level increases increase the bomb rate to 0.25 at level 10
        bombRate = Mathf.Min(level * 0.025f, 0.25f);
        hardRate = Mathf.Min(level * 0.025f, 1f);
        //Duration bound get quicker as we progress. No cap.
        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
        duration = Random.Range(durationMin, durationMax);
    }
    private void Awake()
    {
        gameManager.vegetables += 1;
        //Get references to the components we'll need.
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        //Work out collider values.
        boxOffset = boxCollider2D.offset;
        boxSize = boxCollider2D.size;
        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y / 2f);
        boxSizeHidden = new Vector2(boxSize.x, 0f);

    }

    public void Activate(int level)
    {
        // Getting difficultyvalue to the grabtimer from the gamemanager script
        grabAnimationDuration = gameManager.grabTimer;
        SetLevel(level);
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }
    // Used by the game manager to uniquely identify moles. 
    public void SetIndex(int index)
    {
        moleIndex = index;
    }

    // Used to freeze the game on finish.
    public void StopGame()
    {
        hittable = false;
        StopAllCoroutines();
    }
}
