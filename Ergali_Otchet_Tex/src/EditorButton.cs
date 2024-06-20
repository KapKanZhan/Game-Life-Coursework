using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorButton : MonoBehaviour
{
    private int scene = 3;
    public static bool EditorActive = false;
    // Start is called before the first frame update
    public void changeScene()
    {
        EditorActive = true;
        SceneManager.LoadScene(scene);
    }
}
