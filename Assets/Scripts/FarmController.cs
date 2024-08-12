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
    [SerializeField] private TileTransform[] watering;

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

    // ---------- public methods - working on Farm methods

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

    public void WaterSoil(Vector2 worldPos)
    {
        //get Tile data from world position
        Vector3Int gridPos = grid.WorldToCell(worldPos);
        TileBase checkedTile = tilemap.GetTile(gridPos);

        //check if there is a valid transformation for the (hoe) tool
        foreach (TileTransform t in watering)
        {
            if (checkedTile == t.from)
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
        if (plantData.sowableTiles.Contains(checkedTile) && IsTileFree(gridPos) && HUD.Instance.RemoveScore())
        {
            PlantController plant =  Placeable.SpawnPlaceable(plantData.plantPrefab, gridInfo, gridPos).GetComponent<PlantController>();
            plant.Init(plantData);
        }
    }

    public void DrainSoil(Vector3Int gridPos)
    {
        //get Tile data from cell position
        TileBase checkedTile = tilemap.GetTile(gridPos);

        foreach (TileTransform t in watering)
        {
            //basically reverse the 'from' and 'to' in watering
            if (checkedTile == t.to)
            {
                tilemap.SetTile(gridPos, t.from);
                return;
            }
        }
    }

    public void Interact(Vector2 worldPos)
    {
        //get Tile data from world position
        Vector3Int gridPos = grid.WorldToCell(worldPos);
        TileBase checkedTile = tilemap.GetTile(gridPos);

        Placeable obj = gridInfo.GetPositionProperty(gridPos, OCCUPANT_NAME, (Object)null) as Placeable;
        if (obj != null)
        {
            if (obj.type == Placeable.Type.plant)
            {
                (int amount, PlantSO plant) = (obj as PlantController).GatherPlant();
                HUD.Instance.AddScore(amount);
            }
        }
    }

    // ---------- public getter methods

    public bool IsTileWatered(Vector3Int gridPos)
    {
        //get Tile data from cell position
        TileBase checkedTile = tilemap.GetTile(gridPos);

        foreach (TileTransform t in watering)
        {
            if (checkedTile == t.to)
                return true;
        }

        return false;
    }

    public bool GetClosestFreePlant(Vector3 worldPos, out PlantController plant)
    {
        //get Tile data from world position
        Vector3Int gridPos = grid.WorldToCell(worldPos);
        TileBase checkedTile = tilemap.GetTile(gridPos);

        plant = null;

        float distance = Mathf.Infinity;
        //iterate through placed things
        foreach (Vector3Int cellPos in gridInfo.GetAllPositions(OCCUPANT_NAME))
        {
            Placeable obj = gridInfo.GetPositionProperty(cellPos, OCCUPANT_NAME, (Object)null) as Placeable;
            if (obj != null)
            {
                //check if thing is a plant
                if (obj.type == Placeable.Type.plant && Vector3.Distance(worldPos, obj.transform.position) < distance && (obj as PlantController).CanBeEaten())
                {
                    distance = Vector3.Distance(worldPos, obj.transform.position);
                    plant = obj as PlantController;
                }
            }
        }

        if (plant == null)
            return false;
        else
            return true;
    }

    // ---------- private methods

    //true = if there is no plant, object, etc. on a given tile
    private bool IsTileFree(Vector3Int gridPos)
    {
        return gridInfo.GetPositionProperty<Object>(gridPos, OCCUPANT_NAME, null) == null;
    }
}
