using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid), typeof(GridInformation))]
public class FarmController : MonoBehaviour
{
    [System.Serializable]
    public class TileTransform
    {
        public TileBase from;
        public TileBase to;
    }

    [SerializeField] private Tilemap tilemap;
    [Header("Tiles transformations")]
    [SerializeField] private TileTransform[] tilling;
    [SerializeField] private GameObject plantPrefab;

    //references
    private Grid grid;
    private GridInformation gridInfo;

    //grid information values
    public static string OCCUPANT_NAME = "Occupant";//Placeable type value (a reference to the object on given tile/tiles)

    //singleton
    private static FarmController _instance;
    public static FarmController Instance { get { return _instance; } }

    // ---------- Unity messages

    private void Awake()
    {
        _instance = this;

        grid = GetComponent<Grid>();
        gridInfo = GetComponent<GridInformation>();
    }

    // ---------- public methods

    public void TillSoil(Vector2 worldPos)
    {
        //get Tile data from world position
        Vector3Int gridPos = grid.WorldToCell(worldPos);
        TileBase checkedTile = tilemap.GetTile(gridPos);

        //check if there is a valid transformation for the (hoe) tool
        foreach (TileTransform t in tilling)
        {
            if (checkedTile == t.from && IsTileFree(gridPos))
            {
                tilemap.SetTile(gridPos, t.to);
                return;
            }
        }
    }

    public void SowSoil(Vector2 worldPos, PlantSO plantData)
    {
        //get Tile data from world position
        Vector3Int gridPos = grid.WorldToCell(worldPos);
        TileBase checkedTile = tilemap.GetTile(gridPos);

        //check if the given plant can be sown on the given tile
        if (plantData.sowableTiles.Contains(checkedTile) && IsTileFree(gridPos))
        {
            PlantController plant =  Placeable.SpawnPlaceable(plantData.plantPrefab, gridInfo, gridPos).GetComponent<PlantController>();
            plant.Init(plantData);
        }
    }

    // ---------- private methods

    //true = if there is no plant, object, etc. on a given tile
    private bool IsTileFree(Vector3Int gridPos)
    {
        return gridInfo.GetPositionProperty<Object>(gridPos, OCCUPANT_NAME, null) == null;
    }
}
