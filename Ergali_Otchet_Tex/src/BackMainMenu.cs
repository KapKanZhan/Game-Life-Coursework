using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMainMenu : MonoBehaviour
{
    public int scene;

    public void changeScene()
    {
        RandomButton.RandomActive = false;
        SceneManager.LoadScene(scene);
    }
}
