using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    public Slider mySlider;

    public void Awake()
    {
        mySlider.GetComponent<Slider>().value = Mole.difficulty;
    }
}
