using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;

    bool optionsOpen = false;


    public void MainStartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void MainOptionsButton()
    {
        if (optionsOpen)
        {
            optionsOpen = false;
            optionsPanel.SetActive(false);
        }
        else
        {
            optionsOpen = true;
            optionsPanel.SetActive(true);
        }
    }

    public void MainExitButton()
    {
        Debug.Log("Quitting application!");
        Application.Quit();
    }

    public void OptionsCloseButton()
    {
        optionsPanel.SetActive(false);
        optionsOpen = false;
    }
}
