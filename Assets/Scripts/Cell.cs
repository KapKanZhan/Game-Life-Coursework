using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour
{
    public Vector2Int Position { get; protected set; }
    public int Energy { get; protected set; }

    protected Cell(Vector2Int position, int initialEnergy)
    {
        Position = position;
        Energy = initialEnergy;
    }

    // Основной метод действия клетки на карте
    public abstract void Act(Cell[,] map, int mapWidth, int mapHeight);

    // Помощник для проверки соседних клеток
    protected bool IsCellAtPosition(Cell[,] map, int x, int y)
    {
        return x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1) && map[x, y] != null;
    }
}
