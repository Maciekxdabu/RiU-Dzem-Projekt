using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantController : Placeable
{
    [SerializeField] private PlantSO plantData;
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private int maxEaters = 3;
    [SerializeField] private Slider healthSlider = null;

    private bool grown = false;
    private float growthProgress;
    private float health = 1f;
    private int eaters = 0;

    // ---------- Unity messages

    private void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (grown == false && FarmController.Instance.IsTileWatered(leftDowntile))
        {
            growthProgress += Time.deltaTime;

            if (growthProgress >= plantData.growthTime)
            {
                FarmController.Instance.DrainSoil(leftDowntile);
                grown = true;
            }

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

    public (int, PlantSO) GatherPlant()
    {
        if (grown)
        {
            Invoke(nameof(Remove), 0f);
            return (Mathf.RoundToInt(plantData.Gather() * (health / maxHealth)), plantData);
        }
        else
            return (0, null);
    }

    public bool CanBeEaten()
    {
        return eaters < maxEaters;
    }

    public bool ReserveForEating()
    {
        if (CanBeEaten())
        {
            eaters++;
            return true;
        }
        else
            return false;
    }

    public void EatPlant(float amountEaten)
    {
        health -= amountEaten;
        UpdateHealthBar();

        if (health <= 0f)
        {
            OnPlantDeath();
        }
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

    private void UpdateHealthBar()
    {
        healthSlider.value = health / maxHealth;
    }

    private void OnPlantDeath()
    {
        Invoke(nameof(Remove), 0f);
    }
}
