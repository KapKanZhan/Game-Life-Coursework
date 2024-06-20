using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapClickHandler : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile replacementTile;
    public int width = 20; // Ширина зоны
    public int height = 20; // Высота зоны
    public GameObject EditStart; 

    private int tileX;
    private int tileY;



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

            if (IsWithinBounds(cellPosition))
            {
                ReplaceTile(cellPosition);
            }
        }
    }

    bool IsWithinBounds(Vector3Int cellPosition)
    {
        int halfWidth = width / 2;
        int halfHeight = height / 2;
        return cellPosition.x >= -halfWidth+1 && cellPosition.x <= halfWidth-1 && cellPosition.y >= -halfHeight+1 && cellPosition.y <= halfHeight-1;
    }

    public void ReplaceTile(Vector3Int cellPosition)
    {
        tilemap.SetTile(cellPosition, replacementTile);
        tileX = cellPosition.x;
        tileY = cellPosition.y;
        EditStart.GetComponent<EditorStartMap>().pattern.AddCell(tileX, tileY);
        Debug.Log("Tile replaced at: " + cellPosition);
        Debug.Log("Tile coordinates: x = " + tileX + ", y = " + tileY);
    }
}
