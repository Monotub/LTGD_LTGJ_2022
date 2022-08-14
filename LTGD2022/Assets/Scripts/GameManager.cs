using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int manaAmount {get; private set;}

    bool paused = false;


    private void Start()
    {
        manaAmount = 200;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    public void PauseMenu()
    {
        if (paused)
        {
            Time.timeScale = 1;
        }
        else if (!paused)
        {
            Time.timeScale = 0;
        }
        paused = !paused;
    }
}
