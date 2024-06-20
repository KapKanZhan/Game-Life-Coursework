using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraAdjuster : MonoBehaviour
{
    public Tilemap map; // Ссылка на Tilemap
    BorderDraw border;
    public float minCameraSize = 30f; // Минимальный размер камеры
    private void Awake()
    {
        border = GetComponent<BorderDraw>();
    }

    void Start()
    {
        AdjustCamera();
    }

    public void AdjustCamera()
    {
        Camera cam = Camera.main;

        float tileHeight = map.cellSize.y; // Высота тайла
        float requiredHeight = (border.height*2) * tileHeight; // Необходимая видимая высота
        //cam.orthographicSize = requiredHeight / 2; // Установка orthographicSize

        float requiredWidth = (border.width*2) * map.cellSize.x; // Необходимая видимая ширина
        float screenWidth = requiredWidth / (2 * cam.aspect); // Рассчитываем нужный размер камеры исходя из ширины
        //cam.orthographicSize = Mathf.Max(cam.orthographicSize, screenWidth);
        cam.orthographicSize = Mathf.Max(minCameraSize, Mathf.Max(requiredHeight, screenWidth));
        // Позиционирование камеры по центру Tilemap
        cam.transform.position = new Vector3(0,
        0,
        -10);
    }
}
