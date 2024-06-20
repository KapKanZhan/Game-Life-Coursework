using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{

    private Animator animPanel;
    private Animator animMainTitle;
    public GameObject PanelMenu;
    public GameObject StartTitle;
    public GameObject MainTitle;

    bool Clck = false; 

    // Start is called before the first frame update
    void Start()
    {
        animPanel = PanelMenu.GetComponent<Animator>();
        animMainTitle = MainTitle.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && Clck == false)
        {
            StartTitle.SetActive(false);
            Clck = true;
            animMainTitle.SetTrigger("IntroStart");

            animPanel.SetTrigger("StartMenu");
        }
    }
}
