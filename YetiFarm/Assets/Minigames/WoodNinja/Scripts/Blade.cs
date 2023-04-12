using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider2D bladeCollider;
    private TrailRenderer bladeTrail;
    private bool slicing;
    public float minSliceVelocity;
    public float sliceForce;

    public Vector3 direction { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider2D>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }
    private void OnEnable()
    {
        StopSlicing();
    }
    private void OnDisable()
    {
        StopSlicing();
    }

    // Checking if player is holding mouse down or touching the screen to move the blade
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }


    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        newPosition.z = 0f;
        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    // Disabling blade collider,trail when not touching the screen
    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;

    }

    // when continuously slicing with the blade object updating position to worldspace.
    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }



}
