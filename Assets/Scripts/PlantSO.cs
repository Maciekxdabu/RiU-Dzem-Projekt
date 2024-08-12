using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Reksio Projekt/Plant", fileName = "Plant")]
public class PlantSO : ScriptableObject
{
    //general values
    [Header("General")]
    public string plantName = "Plant X";
    public GameObject plantPrefab = null;

    //growing and planting values
    [Header("Planting and Growing")]
    public List<TileBase> sowableTiles = new List<TileBase>();
    public Sprite[] growthStagesSprites = { };//first must be seeds

    [Header("Economy")]
    public float growthTime = 5f;
    [Tooltip("Range \"from to\" how many player can get per gather")]
    public Vector2Int amountGatheredRange = Vector2Int.one;

    // ---------- public methods

    public int Gather()
    {
        return Random.Range(amountGatheredRange.x, amountGatheredRange.y + 1);
    }
}
