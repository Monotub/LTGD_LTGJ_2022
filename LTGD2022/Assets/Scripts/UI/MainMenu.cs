using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [Header("Main Setup")]
    [SerializeField] GameObject optionsPanel;
    [SerializeField] Animator transitionAnim;
    [SerializeField] GameStatsSO gameData;

    [Header("Audio Setup")]
    [SerializeField] AudioClip hoverClip;
    [SerializeField] AudioClip clickClip;
    [SerializeField] AudioClip quitClip;
    [SerializeField] AudioClip startClip;

    AudioSource sfx;

    bool optionsOpen = false;
    int startingEssence = 200;

    private void Awake()
    {
        sfx = GetComponent<AudioSource>();
    }

    public void MainStartButton()
    {
        gameData.EssenceAmount = startingEssence;
        transitionAnim.SetTrigger("End");
        sfx.PlayOneShot(startClip);
        StartCoroutine(LoadFirstLevel());
    }

    IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    public void MainOptionsButton()
    {
        sfx.PlayOneShot(clickClip);
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
        sfx.PlayOneShot(quitClip);
        Debug.Log("Quitting application!");
        Application.Quit();
    }

    public void OptionsCloseButton()
    {
        sfx.PlayOneShot(clickClip);
        optionsPanel.SetActive(false);
        optionsOpen = false;
    }
}
