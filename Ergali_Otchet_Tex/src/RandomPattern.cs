using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPattern : MonoBehaviour
{
    public Vector2Int[] cells;

    public void GenerateRandomPattern(int numberOfCells, int minX, int maxX, int minY, int maxY)
    {
        cells = new Vector2Int[numberOfCells];
        for (int i = 0; i < numberOfCells; i++)
        {
            int x = Random.Range(minX, maxX + 1);
            int y = Random.Range(minY, maxY + 1);
            cells[i] = new Vector2Int(x, y);
        }
    }
}
