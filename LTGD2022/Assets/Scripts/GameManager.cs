using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public bool gameStarted { get; private set;}
    public bool paused { get; private set;}
    
    public static GameManager Instance;

    public static event Action<bool> PauseGame;



    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        gameStarted = false;
        paused = false;
    }

    private void OnEnable()
    {
        PartySelection.StartGame += StartGame;
    }

    private void OnDisable()
    {
        PartySelection.StartGame -= StartGame;
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
            PauseGame?.Invoke(false);
        }
        else if (!paused)
        {
            Time.timeScale = 0;
            PauseGame?.Invoke(true);
        }
        paused = !paused;
    }

    void StartGame()
    {
        gameStarted = true;
    }
}
