using UnityEngine;
using System;


public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject optionsPanel;

    bool optionsOpen = false;


    private void OnEnable()
    {
        GameManager.PauseGame += TogglePauseScreen;
    }

    private void OnDisable()
    {
        GameManager.PauseGame -= TogglePauseScreen;
    }

    void TogglePauseScreen(bool paused)
    {
        if(paused) pausePanel.SetActive(true);
        else pausePanel.SetActive(false);
    }

    /* Used in a button event. Do not delete! */
    public void ToggleOptionsScreen()
    {
        if (optionsOpen)
        {
            optionsPanel.SetActive(false);
            optionsOpen = false;
        }
        else
        {
            optionsPanel.SetActive(true);
            optionsOpen = true;
        }
    }
}
