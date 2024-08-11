using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : Placeable
{
    [SerializeField] private PlantSO plantData;
    [SerializeField] private SpriteRenderer spriteRend;

    private bool grown = false;
    private float growthProgress;

    // ---------- Unity messages

    private void Update()
    {
        if (grown == false)
        {
            growthProgress += Time.deltaTime;

            if (growthProgress >= plantData.growthTime)
                grown = true;

            UpdateVisuals();
        }
    }

    // ---------- public methods

    public void Init(PlantSO _plantData)
    {
        if (_plantData != null)
            plantData = _plantData;

        if (plantData == null)
        {
            Debug.LogError("There is no plant SO assigned to plant", gameObject);
            return;
        }

        spriteRend.sprite = plantData.growthStagesSprites[0];
        growthProgress = 0f;
        grown = false;
    }

    // ---------- private methods

    private void UpdateVisuals()
    {
        if (grown)
            spriteRend.sprite = plantData.growthStagesSprites[plantData.growthStagesSprites.Length - 1];
        else
        {
            int index = Mathf.FloorToInt(growthProgress / plantData.growthTime * (plantData.growthStagesSprites.Length - 1));
            spriteRend.sprite = plantData.growthStagesSprites[index];
        }
    }
}
