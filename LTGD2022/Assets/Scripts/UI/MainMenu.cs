using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] Animator transitionAnim;
    [SerializeField] GameStatsSO gameData;

    bool optionsOpen = false;
    int startingEssence = 200;


    public void MainStartButton()
    {
        gameData.EssenceAmount = startingEssence;
        transitionAnim.SetTrigger("End");
        StartCoroutine(LoadFirstLevel());
    }

    IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(1f);
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
