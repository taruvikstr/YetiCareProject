using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private ParticleSystem splashParticleEffect;
    private AudioManager audioManager;
    
    private void Awake()
    {
        splashParticleEffect = GetComponentInChildren<ParticleSystem>();
        
    }
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            splashParticleEffect.Play();
            audioManager.PlaySound("Rock");
            FindObjectOfType<GameManager>().EndGame(true);
            FindObjectOfType<GameManager>().FlashScreen();

        }
    }
}
