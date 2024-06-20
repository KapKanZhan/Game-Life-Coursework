    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Tilemap currentState;
    [SerializeField] private Tilemap nextState;
    //[SerializeField] private Tilemap BorderTilemap; // ������ �� Tilemap ��� ����
    //[SerializeField] private Tile borderTile; // ������ �� ���� ��� �������
    [SerializeField] private Tile aliveTile;
    [SerializeField] private Tile deadTile;
    [SerializeField] private Pattern pattern;
    //[SerializeField] private Pattern pattern;
    [SerializeField] private float updateInterval = 0.05f;

    public bool test = false;

    public HashSet<Vector3Int> aliveCells;
    private HashSet<Vector3Int> cellsToCheck;

    public int population { get; private set; }
    public int iterations { get; private set; }
    public float time { get; private set; }

    BorderDraw border;



    private void Awake()
    {
        aliveCells = new HashSet<Vector3Int>();
        cellsToCheck = new HashSet<Vector3Int>();
        border = GetComponent<BorderDraw>();
    }

    private void Start()
    {
        if (!test)
        {
            if (!EditorStartMap.EditorActive)
            {
                if (!RandomButton.RandomActive)
                {
                    pattern = LevelTransition.MyPattern;
                }
                else
                {
                    pattern = RandomButton.MyPattern;
                }
            }
            else
            {
                pattern = EditorStartMap.MyPattern;
            }

        }
        
        SetPattern(pattern);
        //DrawBorders();
    }


    private void SetPattern(Pattern pattern)
    {
        Clear(); // ������� �������� ��������� �������� ����

        Vector2Int center = pattern.GetCenter(); // ��������� ������ �������� ��� ����������� ���������� �� ������� ����

        for (int i = 0; i < pattern.cells.Length; i++) // ������� ���� ������ � ��������
        {
            Vector3Int cell = (Vector3Int)(pattern.cells[i]); // �������������� ������� ������ �� �������� � ������� �� ������� ����
            currentState.SetTile(cell, aliveTile); // ��������� ����� ��� ����� ������ �� ������� Tilemap
            aliveCells.Add(cell); // ���������� ������ � ������ ����� ������
            
            
        }
        
        population = aliveCells.Count; // ���������� ����� ����� ������ � ���������
    }

    private void Clear()
    {
        aliveCells.Clear(); // ������� ������ ����� ������
        cellsToCheck.Clear(); // ������� ������ ������ ��� ��������
        currentState.ClearAllTiles(); // ������� ���� ������ �� ������� Tilemap
        nextState.ClearAllTiles(); // ������� ���� ������ �� ��������� Tilemap
        population = 0; // ����� ���������� ����� ������
        iterations = 0; // ����� ���������� ��������
        time = 0f; // ����� ������� ���������
    }

    private void OnEnable()
    {
        StartCoroutine(Simulate());
    }

    private IEnumerator Simulate()
    {
        var interval = new WaitForSeconds(updateInterval); // �������� ����� ������ ����� ���������
        yield return interval; // ������ ����� ����� ������� �����

        while (enabled) // ���� ��������� �������, ��������� ���������
        {
            UpdateState(); // ���������� ��������� �������� ����

            population = aliveCells.Count; // ���������� ���������� ����� ������
            iterations++; // ���������� ���������� �������� ���������
            time += updateInterval; // ���������� ������ ������� ���������

            yield return interval; // �������� �� ���������� ���������� ���������
        }
    }

    private void UpdateState()
    {
        cellsToCheck.Clear(); // ������� ������ ������ ��� ��������

        // �������� ��� ������, ������� ����� ���������
        foreach (Vector3Int cell in aliveCells)
        {
            //Debug.Log(cell);
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    cellsToCheck.Add(cell + new Vector3Int(x, y));
                }
            }
        }

        // ������� ������ � ��������� ���������
        foreach (Vector3Int cell in cellsToCheck)
        {
            
            int neighbors = CountNeighbors(cell); // ������� ������� ������
            
            bool alive = IsAlive(cell); // ��������, ���� �� ������


            if (!alive && neighbors == 3)
            {
                nextState.SetTile(cell, aliveTile);// ��������� ������, ���� ����� 3 ����� ������
                aliveCells.Add(cell); //��������� ����� ����� ������
            }
            else if (alive && (neighbors < 2 || neighbors > 3))
            {
                nextState.SetTile(cell, deadTile); // �������� ������ ��-�� �������� ��� �������������
                aliveCells.Remove(cell); //�������� ����� ������
            }
            else // ����������� �������� ���������, ���� ��������� �� ���������
            {
                nextState.SetTile(cell, currentState.GetTile(cell)); 
            }
        }

        // ����� �������� � ���������� ���������
        Tilemap temp = currentState;
        currentState = nextState;
        nextState = temp;
        nextState.ClearAllTiles();
    }

    private int CountNeighbors(Vector3Int cell)
    {
        int count = 0;
        int minCoordX = -1 * (border.width - 2);// ����������� ����������
        int maxCoordX = border.width - 2;// ������������ ����������
        int minCoordY = -1 * (border.height - 2);// ����������� ����������
        int maxCoordY = border.height - 2;// ������������ ����������
        

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue; // ���������� ���� ������

                int neighborX = cell.x + x;
                int neighborY = cell.y + y;

                if (neighborX >= minCoordX && neighborX <= maxCoordX && neighborY >= minCoordY && neighborY <= maxCoordY)
                {
                    Vector3Int neighbor = new Vector3Int(neighborX, neighborY, cell.z);
                    if (IsAlive(neighbor))
                    {
                        count++;
                    }
                }

            }
        }

        return count;
    }

    // ���������� ������� ��� ����������� ���������� ������, ����������� ������������� ��������
    private int Mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }




    private bool IsAlive(Vector3Int cell)
    {
        return currentState.GetTile(cell) == aliveTile;
    }

}
