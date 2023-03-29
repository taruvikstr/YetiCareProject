using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartColliderBehavior : MonoBehaviour
{
    public float cutTimer = 0.0f;
    private float cutTimeLimit = 2.0f;
    public bool cutOn = false;
    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = CutTimer();
    }

    private void Update()
    {
        if (cutOn == true)
        {
            cutTimer += Time.deltaTime;
        }
        else
        {
            cutTimer = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator CutTimer()
    {
        cutOn = true;
        yield return new WaitForSeconds(cutTimeLimit);
        cutOn = false;
        cutTimer = 0.0f;
    }

    public void StopCounter()
    {
        StopCoroutine(coroutine);
        cutOn = false;
        cutTimer = 0.0f;
    }
}
