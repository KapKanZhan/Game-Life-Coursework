using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RandomButton : MonoBehaviour
{
    private int scene = 1;
    public static bool RandomActive = false;
    public int width = 20;
    public int height = 20;
    public int quantityfCells = 190;
    [SerializeField] private Pattern pattern;
    public static Pattern MyPattern;
    public TextMeshProUGUI SizeText;

    void Awake()
    {
        RandomActive = false;
    }

    public void changeScene()
    {
        pattern.cells = new Vector2Int[0];
        RandomActive = true;
        pattern.width = width;
        pattern.height = height;
        pattern.GenerateRandomPattern(quantityfCells, (-1 * (width/2 - 1)), width/2 - 1, (-1 * (height/2 - 1)), height/2 - 1);
        MyPattern = pattern;
        SceneManager.LoadScene(scene);
    }

    public void changeSize20x20()
    {
        width = 20;
        height = 20;
        quantityfCells = 45;
        SizeText.text = "20x20";
    }

    public void changeSize50x50()
    {
        width = 50;
        height = 50;
        quantityfCells = 260;
        SizeText.text = "50x50";
    }

    public void changeSize100x100()
    {
        width = 100;
        height = 100;
        quantityfCells = 1100;
        SizeText.text = "100x100";
    }
}
