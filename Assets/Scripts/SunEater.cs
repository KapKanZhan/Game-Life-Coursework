using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunEater : Cell
{
    public SunEater(Vector2Int position) : base(position, 0) { }

    public override void Act(Cell[,] map, int mapWidth, int mapHeight)
    {
        // Солнцеядные просто получают фиксированное количество энергии
        Energy += 10;
    }
}
