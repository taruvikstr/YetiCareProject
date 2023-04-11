using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSlicer : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody2D woodRigidBody;
    private Collider2D woodCollider;
    private ParticleSystem splashParticleEffect;

    public int points = 1;

    private void Awake()
    {
        woodRigidBody = GetComponent<Rigidbody2D>();
        woodCollider = GetComponent<Collider2D>();
        splashParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    // These values come from the Blade Script
    private void Slice(Vector2 direction, Vector2 position, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore(points);

        whole.SetActive(false);
        sliced.SetActive(true);

        woodCollider.enabled = false;
        splashParticleEffect.Play();

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        Rigidbody2D[] slices = sliced.GetComponentsInChildren<Rigidbody2D>();

        foreach(Rigidbody2D slice in slices)
        {
            slice.velocity = woodRigidBody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
