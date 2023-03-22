using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [Header ("Features")]
    public List<Sprite> pattern = new List<Sprite>();
    public List<Color> primaryColor = new List<Color>();
    public List<Color> secondaryColor = new List<Color>();


    public SpriteRenderer primaryColorRenderer, patternRenderer;

    private void Start()
    {
        ShuffleFeatures();
        RefreshFish();
    }

    private void ShuffleFeatures()
    {
        primaryColor = primaryColor.OrderBy(i => Guid.NewGuid()).ToList();
        secondaryColor = secondaryColor.OrderBy(i => Guid.NewGuid()).ToList();
        pattern = pattern.OrderBy(i => Guid.NewGuid()).ToList();
    }

    private void RefreshFish()
    {
        primaryColorRenderer.color = primaryColor[0];
        patternRenderer.color = secondaryColor[0];
        patternRenderer.sprite = pattern[0];
    }
}
