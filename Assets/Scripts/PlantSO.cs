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
    public float growthTime = 5f;
    public List<TileBase> sowableTiles = new List<TileBase>();
    public Sprite[] growthStagesSprites = { };//first must be seeds
}
