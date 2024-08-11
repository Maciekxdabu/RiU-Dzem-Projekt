using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Placeable : MonoBehaviour
{
    public enum Type
    {
        plant,
        other
    }

    [SerializeField] private Type _type;
    public Type type { get { return _type; } }
    [SerializeField] private Vector2Int size = Vector2Int.one;//we look at left-down corner as the start position (x,y)

    protected GridInformation gridInfo;
    protected Vector3Int[] tilesTaken;

    // ---------- public static method

    public static Placeable SpawnPlaceable(GameObject prefab, GridInformation _gridInfo, Vector3Int gridPos)
    {
        //check if prefab has Placeable Component
        if (prefab.TryGetComponent<Placeable>(out Placeable placeable))
        {
            //find target cells (check if they are empty)
            List<Vector3Int> cells = new List<Vector3Int>();
            for (int x = gridPos.x; x < gridPos.x + placeable.size.x; x++)
            {
                for (int y = gridPos.y; y < gridPos.y + placeable.size.y; y++)
                {
                    if (_gridInfo.GetPositionProperty<Object>(gridPos, FarmController.OCCUPANT_NAME, null) != null)//scratch object spawning if any target cell is taken
                    {
                        Debug.Log("This spot is taken: " + gridPos.ToString());
                        cells.Clear();
                        return null;
                    }
                    cells.Add(new Vector3Int(x, y, 0));
                }
            }

            //instantiate prefab
            Placeable obj = Instantiate(prefab, _gridInfo.transform).GetComponent<Placeable>();
            obj.transform.position = _gridInfo.GetComponent<Grid>().GetCellCenterWorld(gridPos);
            obj.gridInfo = _gridInfo;

            //register cells
            obj.tilesTaken = cells.ToArray();
            foreach (Vector3Int cell in obj.tilesTaken)
            {
                _gridInfo.SetPositionProperty(cell, FarmController.OCCUPANT_NAME, (Object)obj);
            }

            return obj;
        }

        return null;
    }

    // ---------- public methods

    public void Remove()
    {
        //unregister tiles
        foreach (Vector3Int gridPos in tilesTaken)
        {
            gridInfo.SetPositionProperty(gridPos, FarmController.OCCUPANT_NAME, (Object)null);
        }

        //destroy object
        Destroy(gameObject);
    }
}
