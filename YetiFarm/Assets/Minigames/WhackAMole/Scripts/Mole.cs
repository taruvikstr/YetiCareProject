using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Sprite mole;
    [SerializeField] private Sprite moleHardHat;
    [SerializeField] private Sprite moleHatBroken;
    [SerializeField] private Sprite moleHit;
    [SerializeField] private Sprite moleHatHit;


    [Header("GameManager")]
    [SerializeField] private MoleGameManager gameManager;

    //Sprite offset of the sprite to hide it
    private Vector2 startPosition = new Vector2(0f, -4f); //Pixelsize / PixelsPerUnit
    private Vector2 endPosition = Vector2.zero;
    //How long it takes to show a mole
    private float showDuration = 0.5f;
    public float duration = 1f;

    private bool hittable = true;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider2D;

    private Vector2 boxOffset;
    private Vector2 boxSize;
    private Vector2 boxOffsetHidden;
    private Vector2 boxSizeHidden;

    //Mole Parameters

    public enum MoleType {Standard,HardHat,Bomb };
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

        //Wait for duration to pass
        yield return new WaitForSeconds(duration);

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
    private void OnMouseDown()
    {
        if (hittable)
        {
            switch (moleType)
            {
                case MoleType.Standard:
                    Debug.Log("normal hit");
                    spriteRenderer.sprite = moleHit;
                    gameManager.AddScore(moleIndex);
                    //Stop Coroutines
                    StopAllCoroutines();
                    StartCoroutine(QuickHide());
                    //Turn off hittable so that we cant keep tapping for score.
                    hittable = false;
                    break;
                case MoleType.HardHat:
                    if (lives == 2)
                    {
                        spriteRenderer.sprite = moleHatBroken;
                        lives--;
                    }
                    else
                    {
                        Debug.Log("hatHit");
                        spriteRenderer.sprite = moleHatHit;
                        gameManager.AddScore(moleIndex);
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
                    gameManager.GameOver(1);
                    break;
                default:
                    break;

            }

        }

    }
    private void CreateNext()
    {
        float random = Random.Range(0f, 1f);
        if (random < bombRate)
        {
            // Make a bomb.
            moleType = MoleType.Bomb;
            // The animator handles setting the sprite.
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
                spriteRenderer.sprite = moleHardHat;
                lives = 2;
            }
            else
            {
                // Create a standard one.
                moleType = MoleType.Standard;
                spriteRenderer.sprite = mole;
                lives = 1;
            }
        }
        // Mark as hittable so we can register an onclick event.
        hittable = true;
    }
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
        //Get references to the components we'll need.
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        boxCollider2D = GetComponent<BoxCollider2D>();
        //Work out collider values.
        boxOffset = boxCollider2D.offset;
        boxSize = boxCollider2D.size;
        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y / 2f);
        boxSizeHidden = new Vector2(boxSize.x, 0f);

    }
    
    public void Activate(int level)
    {
        SetLevel(level);
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    public void SetIndex(int index)
    {
        moleIndex = index;
    }
    

    //Progression difficulty
    
    public void StopGame()
    {
        hittable = false;
        StopAllCoroutines();
    }
}
