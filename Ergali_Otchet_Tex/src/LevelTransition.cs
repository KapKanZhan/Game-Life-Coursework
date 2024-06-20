using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    private int scene = 1;
    [SerializeField] private Pattern pattern;
    public static Pattern MyPattern;
    public static bool LevelActive = false;

    void Awake()
    {
        LevelActive = false;
    }

    public void changeScene()
    {
        LevelActive = true;
        MyPattern = pattern;
        SceneManager.LoadScene(scene);
    }
}
