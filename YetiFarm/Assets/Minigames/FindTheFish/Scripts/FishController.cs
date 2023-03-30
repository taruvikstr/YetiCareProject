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

    [SerializeField] private SpriteRenderer primaryColorRenderer, patternRenderer;

    GameObject spawnParent;

    public bool isDragged = false;

    public bool chosenFish = false;

    private void Start()
    {
        spawnParent = transform.parent.gameObject;
        ShuffleFeatures();
        RefreshFish();
    }

    private void Update()
    {
        if(!isDragged)
        {
            MoveFishBackToSea();
        }
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

    private void MoveFishBackToSea() //Found item is moved to it's original position if released
    {
        Vector3 parentPos = spawnParent.transform.position;
    
        transform.position = Vector3.MoveTowards(transform.position, parentPos, 10f * Time.deltaTime);
    }
}
