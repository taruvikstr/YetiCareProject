using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenEgg : MonoBehaviour
{
    public float destructionTimer;
    private void Awake()
    {
        Destroy(gameObject, destructionTimer);
    }
}
