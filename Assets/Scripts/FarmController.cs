using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
public class FarmController : MonoBehaviour
{
    [System.Serializable]
    public class TileTransform
    {
        public TileBase A;
        public TileBase B;
    }

    [SerializeField] private Tilemap tilemap;
    [Header("Tiles transformations")]
    [SerializeField] private TileTransform[] tilling;

    private Grid grid;

    private static FarmController _instance;
    public static FarmController Instance { get { return _instance; } }

    // ---------- Unity messages

    private void Awake()
    {
        _instance = this;

        grid = GetComponent<Grid>();
    }

    // ---------- public methods

    public void TillSoil(Vector2 worldPos)
    {
        Vector3Int gridPos = grid.WorldToCell(worldPos);

        TileBase checkedTile = tilemap.GetTile(gridPos);
        foreach (TileTransform t in tilling)
        {
            if (checkedTile == t.A)
            {
                tilemap.SetTile(gridPos, t.B);
            }
        }
    }
}
