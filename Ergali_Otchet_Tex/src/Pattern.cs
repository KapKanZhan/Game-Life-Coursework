using UnityEngine;

[CreateAssetMenu(menuName = "Game of Life/Pattern")]
public class Pattern : ScriptableObject
{
    public int width = 20;
    public int height = 20;

    public Vector2Int[] cells;

    public Vector2Int GetCenter()
    {
        if (cells == null || cells.Length == 0) {
            return Vector2Int.zero;
        }

        Vector2Int min = Vector2Int.zero;
        Vector2Int max = Vector2Int.zero;

        for (int i = 0; i < cells.Length; i++)
        {
            Vector2Int cell = cells[i];
            min.x = Mathf.Min(min.x, cell.x);
            min.y = Mathf.Min(min.y, cell.y);
            max.x = Mathf.Max(max.x, cell.x);
            max.y = Mathf.Max(max.y, cell.y);
        }

        return (min + max) / 2;
    }

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

    public void AddCell(int x, int y)
    {
        Vector2Int newCell = new Vector2Int(x, y);
        if (cells == null)
        {
            cells = new Vector2Int[] { newCell };
        }
        else
        {
            Vector2Int[] newCells = new Vector2Int[cells.Length + 1];
            for (int i = 0; i < cells.Length; i++)
            {
                newCells[i] = cells[i];
            }
            newCells[cells.Length] = newCell;
            cells = newCells;
        }
    }

}
