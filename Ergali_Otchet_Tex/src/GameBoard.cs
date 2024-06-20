    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Tilemap currentState;
    [SerializeField] private Tilemap nextState;
    //[SerializeField] private Tilemap BorderTilemap; // Ссылка на Tilemap для игры
    //[SerializeField] private Tile borderTile; // Ссылка на тайл для границы
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
        Clear(); // Очистка текущего состояния игрового поля

        Vector2Int center = pattern.GetCenter(); // Получение центра паттерна для правильного размещения на игровом поле

        for (int i = 0; i < pattern.cells.Length; i++) // Перебор всех клеток в паттерне
        {
            Vector3Int cell = (Vector3Int)(pattern.cells[i]); // Преобразование позиции клетки из паттерна в позицию на игровом поле
            currentState.SetTile(cell, aliveTile); // Установка тайла для живой клетки на текущем Tilemap
            aliveCells.Add(cell); // Добавление клетки в список живых клеток
            
            
        }
        
        population = aliveCells.Count; // Обновление числа живых клеток в популяции
    }

    private void Clear()
    {
        aliveCells.Clear(); // Очистка списка живых клеток
        cellsToCheck.Clear(); // Очистка списка клеток для проверки
        currentState.ClearAllTiles(); // Очистка всех тайлов на текущем Tilemap
        nextState.ClearAllTiles(); // Очистка всех тайлов на следующем Tilemap
        population = 0; // Сброс количества живых клеток
        iterations = 0; // Сброс количества итераций
        time = 0f; // Сброс времени симуляции
    }

    private void OnEnable()
    {
        StartCoroutine(Simulate());
    }

    private IEnumerator Simulate()
    {
        var interval = new WaitForSeconds(updateInterval); // Задержка между каждым шагом симуляции
        yield return interval; // Первая пауза перед началом цикла

        while (enabled) // Пока компонент активен, выполнять симуляцию
        {
            UpdateState(); // Обновление состояния игрового поля

            population = aliveCells.Count; // Обновление количества живых клеток
            iterations++; // Увеличение количества итераций симуляции
            time += updateInterval; // Обновление общего времени симуляции

            yield return interval; // Ожидание до следующего обновления состояния
        }
    }

    private void UpdateState()
    {
        cellsToCheck.Clear(); // Очистка списка клеток для проверки

        // Собираем все клетки, которые нужно проверить
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

        // Переход клеток в следующее состояние
        foreach (Vector3Int cell in cellsToCheck)
        {
            
            int neighbors = CountNeighbors(cell); // Подсчет соседей клетки
            
            bool alive = IsAlive(cell); // Проверка, жива ли клетка


            if (!alive && neighbors == 3)
            {
                nextState.SetTile(cell, aliveTile);// Оживление клетки, если рядом 3 живых соседа
                aliveCells.Add(cell); //Добавлени новой живой клетки
            }
            else if (alive && (neighbors < 2 || neighbors > 3))
            {
                nextState.SetTile(cell, deadTile); // Умирание клетки из-за изоляции или перенаселения
                aliveCells.Remove(cell); //Удаление живой клетки
            }
            else // Копирование текущего состояния, если изменения не требуются
            {
                nextState.SetTile(cell, currentState.GetTile(cell)); 
            }
        }

        // Обмен текущего и следующего состояний
        Tilemap temp = currentState;
        currentState = nextState;
        nextState = temp;
        nextState.ClearAllTiles();
    }

    private int CountNeighbors(Vector3Int cell)
    {
        int count = 0;
        int minCoordX = -1 * (border.width - 2);// Минимальная координата
        int maxCoordX = border.width - 2;// Максимальная координата
        int minCoordY = -1 * (border.height - 2);// Минимальная координата
        int maxCoordY = border.height - 2;// Максимальная координата
        

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue; // Пропускаем саму клетку

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

    // Добавление функции для корректного вычисления модуля, учитывающей отрицательные значения
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
