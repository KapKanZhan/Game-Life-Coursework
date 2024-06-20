using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
public class TilemapGridRenderer : MonoBehaviour
{
    private Grid grid;
    private Tilemap tilemap;

    void Start()
    {
        grid = GetComponent<Grid>();
        tilemap = GetComponentInChildren<Tilemap>();
    }

    void OnDrawGizmos()
    {
        if (grid == null || tilemap == null)
            return;

        Vector3Int tilemapOrigin = tilemap.origin;
        Vector3Int tilemapSize = tilemap.size;

        for (int x = tilemapOrigin.x; x < tilemapOrigin.x + tilemapSize.x; x++)
        {
            for (int y = tilemapOrigin.y; y < tilemapOrigin.y + tilemapSize.y; y++)
            {
                Vector3 cellPosition = grid.CellToWorld(new Vector3Int(x, y, 0));
                Gizmos.color = Color.white;
                Gizmos.DrawLine(cellPosition, cellPosition + new Vector3(grid.cellSize.x, 0, 0));
                Gizmos.DrawLine(cellPosition, cellPosition + new Vector3(0, grid.cellSize.y, 0));
            }
        }

        for (int x = tilemapOrigin.x; x < tilemapOrigin.x + tilemapSize.x; x++)
        {
            Vector3 cellPosition = grid.CellToWorld(new Vector3Int(x, tilemapOrigin.y + tilemapSize.y, 0));
            Gizmos.DrawLine(cellPosition, cellPosition + new Vector3(grid.cellSize.x, 0, 0));
        }

        for (int y = tilemapOrigin.y; y < tilemapOrigin.y + tilemapSize.y; y++)
        {
            Vector3 cellPosition = grid.CellToWorld(new Vector3Int(tilemapOrigin.x + tilemapSize.x, y, 0));
            Gizmos.DrawLine(cellPosition, cellPosition + new Vector3(0, grid.cellSize.y, 0));
        }
    }
}
