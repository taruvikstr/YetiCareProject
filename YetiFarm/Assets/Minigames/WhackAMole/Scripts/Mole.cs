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


    //Sprite offset of the sprite to hide it
    private Vector2 startPosition = new Vector2(0f, -4f); //Pixelsize / PixelsPerUnit
    private Vector2 endPosition = Vector2.zero;
    //How long it takes to show a mole
    private float showDuration = 0.5f;
    public float duration = 1f;

    private bool hittable = true;

    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    void Update()
    {
        
    }
    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        // Startposition
        transform.localPosition = start;
        float transition = 0f;
        while(transition < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, transition / showDuration);
            transition += Time.deltaTime;
            yield return null;
        }

        //Endposition
        transform.localPosition = end;

        //Wait for duration to pass
        yield return new WaitForSeconds(duration);

        //Hide mole
        transition = 0f;
        while (transition < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, transition / showDuration);
            transition += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = start;

    }
    private void OnMouseDown()
    {
        if (hittable)
        {
            spriteRenderer.sprite = moleHit;
            //Stop Coroutines
            StopAllCoroutines();
            StartCoroutine(QuickHide());
            hittable = false;
        }
        
    }
    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.25f);
        if (!hittable)
        {
            Hide();
        }
      
    }
    public void Hide()
    {
        transform.localPosition = startPosition;
    }
}
