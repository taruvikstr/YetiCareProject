using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private ParticleSystem splashParticleEffect;
    private void Awake()
    {
        splashParticleEffect = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            splashParticleEffect.Play();
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
