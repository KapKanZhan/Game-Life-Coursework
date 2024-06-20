using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EditorStartMap : MonoBehaviour
{

    private int scene = 1;
    public static bool EditorActive = false;
    public int width = 20;
    public int height = 20;
    public int quantityfCells = 190;
    [SerializeField] public Pattern pattern;
    public static Pattern MyPattern;
    public TextMeshProUGUI SizeText;

    void Awake()
    {
        EditorActive = false;
    }

    void Start()
    {
        
        pattern.cells = new Vector2Int[0];
    }


    public void changeScene()
    {
        EditorActive = true;
        pattern.width = width;
        pattern.height = height;
        MyPattern = pattern;
        SceneManager.LoadScene(scene);
    }

    public void changeSize20x20()
    {
        pattern.cells = new Vector2Int[0];
        width = 20;
        height = 20;
        SizeText.text = "20x20";
    }

    public void changeSize50x50()
    {
        pattern.cells = new Vector2Int[0];
        width = 50;
        height = 50;
        SizeText.text = "50x50";
    }

    public void changeSize100x100()
    {
        pattern.cells = new Vector2Int[0];
        width = 100;
        height = 100;
        SizeText.text = "100x100";
    }
}

