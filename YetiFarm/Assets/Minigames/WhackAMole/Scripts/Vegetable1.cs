using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable1 : MonoBehaviour
{
    MoleGameManager mgm;
    void Start()
    {
        mgm = GameObject.FindGameObjectWithTag("Vegetable1").GetComponent<MoleGameManager>();
        mgm.vegetable1++;
    }

}
