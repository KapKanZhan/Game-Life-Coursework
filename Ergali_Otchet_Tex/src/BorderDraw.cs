using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BorderDraw : MonoBehaviour
{

    [SerializeField] private Tilemap BorderTilemap; // ������ �� Tilemap ��� ����
    [SerializeField] private Tile borderTile; // ������ �� ���� ��� �������
    [SerializeField] private Tilemap CellTilemap; // ������ �� Tilemap ��� ����
    public int width = 20; // ������
    public int height = 20; // ������
    public bool test = false;
    public bool editing = EditorButton.EditorActive;
    public GameObject ClickSize;
    // Start is called before the first frame update
    void Start()
    {
            if (!test)
            {
                if (EditorStartMap.EditorActive)
                {
                    width = EditorStartMap.MyPattern.width;
                    height = EditorStartMap.MyPattern.height;
                }
                else if(LevelTransition.LevelActive)
                {
                    width = LevelTransition.MyPattern.width;
                    height = LevelTransition.MyPattern.height;
                }
                else if(RandomButton.RandomActive)
                {
                    width = RandomButton.MyPattern.width;
                    height = RandomButton.MyPattern.height;
                }
                else
                {
                    width = 20;
                    height = 20;
                }
            }

        

        width = (width) / 2;
        height = (height) / 2;
        DrawBorders();
    }

    private void DrawBorders()
    {
        // ����������� � ������������ ���������� �� ���� X � Y
        int minX = -1 * width; // ����������� ���������� �� X
        int maxX = width; // ������������ ���������� �� X
        int minY = -1 * height; // ����������� ���������� �� Y (������ ��� ��������������)
        int maxY = height; // ������������ ���������� �� Y (������ ��� ��������������)

        // ������ �������������� ����� �������
        for (int x = minX; x <= maxX; x++)
        {
            BorderTilemap.SetTile(new Vector3Int(x, minY, 0), borderTile); // ������ �������
            BorderTilemap.SetTile(new Vector3Int(x, maxY, 0), borderTile); // ������� �������
        }

        // ������ ������������ ����� �������
        for (int y = minY; y <= maxY; y++)
        {
            BorderTilemap.SetTile(new Vector3Int(minX, y, 0), borderTile); // ����� �������
            BorderTilemap.SetTile(new Vector3Int(maxX, y, 0), borderTile); // ������ �������
        }
    }

    private void ClearBorders()
    {
        BorderTilemap.ClearAllTiles();
        CellTilemap.ClearAllTiles();
    }

    public void SetSize20x20()
    {
        ClearBorders();
        width = (20) / 2;
        height = (20) / 2;
        gameObject.GetComponent<TilemapClickHandler>().width = 20;
        gameObject.GetComponent<TilemapClickHandler>().height = 20;
        DrawBorders();
    }

    public void SetSize50x50()
    {
        ClearBorders();
        width = (50) / 2;
        height = (50) / 2;
        gameObject.GetComponent<TilemapClickHandler>().width = 50;
        gameObject.GetComponent<TilemapClickHandler>().height = 50;
        DrawBorders();
    }

    public void SetSize100x100()
    {
        ClearBorders();
        width = (100) / 2;
        height = (100) / 2;
        gameObject.GetComponent<TilemapClickHandler>().width = 100;
        gameObject.GetComponent<TilemapClickHandler>().height = 100;
        DrawBorders();
    }

}
