using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraAdjuster : MonoBehaviour
{
    public Tilemap map; // ������ �� Tilemap
    BorderDraw border;
    public float minCameraSize = 30f; // ����������� ������ ������
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

        float tileHeight = map.cellSize.y; // ������ �����
        float requiredHeight = (border.height*2) * tileHeight; // ����������� ������� ������
        //cam.orthographicSize = requiredHeight / 2; // ��������� orthographicSize

        float requiredWidth = (border.width*2) * map.cellSize.x; // ����������� ������� ������
        float screenWidth = requiredWidth / (2 * cam.aspect); // ������������ ������ ������ ������ ������ �� ������
        //cam.orthographicSize = Mathf.Max(cam.orthographicSize, screenWidth);
        cam.orthographicSize = Mathf.Max(minCameraSize, Mathf.Max(requiredHeight, screenWidth));
        // ���������������� ������ �� ������ Tilemap
        cam.transform.position = new Vector3(0,
        0,
        -10);
    }
}
