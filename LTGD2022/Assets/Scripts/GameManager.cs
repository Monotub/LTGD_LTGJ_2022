using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    [Header("General Setup")]
    [SerializeField] GameStatsSO gameData;

    public GameStatsSO GameData => gameData;
    public bool gameStarted { get; private set;}
    public bool paused { get; private set;}
    public int partySize {get; private set;}
    public int partyDied { get; private set; }
    public int partyLived { get; private set; }
    public int startingEssence { get; private set; }
    public int bonusEssence { get; private set; }
    public int lostEssence { get; private set; }
    public int currentSceneIndex { get; private set; }
    
    public static GameManager Instance;
    public static event Action<bool> PauseGame;
    public static event Action<bool> LevelCompleted;
    public static event Action StartTransition;

    Magic magic;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        gameStarted = false;
        paused = false;
        magic = FindObjectOfType<Magic>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        Time.timeScale = 1;
        startingEssence = gameData.EssenceAmount;
    }

    private void OnEnable()
    {
        PartySelection.StartGame += OnStartGame;
        Health.InsectDied += OnInsectDeath;
        Insect.InsectCompleted += OnInsectCompleted;
    }

    private void OnDisable()
    {
        PartySelection.StartGame -= OnStartGame;
        Health.InsectDied -= OnInsectDeath;
        Insect.InsectCompleted -= OnInsectCompleted;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (magic.isSpellSelected)
                magic.ClearSelectedSpell();
            else
                PauseMenu();
        }

        MonitorPartySize();
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

    void OnStartGame()
    {
        gameStarted = true;
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Debug.Log("Quitting application!");
        Application.Quit();
    }

    public void SetPartySize(int amt)
    {
        partySize = amt;
    }

     void OnInsectDeath(InsectStatsSO data)
    {
        partyDied++;
        lostEssence += data.EssenceCost;
    }

    void OnInsectCompleted(int essenceValue)
    {
        partyLived++;
        gameData.EssenceAmount += essenceValue;
    }

    void MonitorPartySize()
    {
        if (!gameStarted) return;

        if(partyDied == partySize)
        {
            LevelComplete(false);
            gameStarted = false;
        }
        else if (partyLived + partyDied == partySize)
        {
            gameData.EssenceAmount += bonusEssence;
            LevelComplete(true);
            gameStarted = false;
        }
    }

    void LevelComplete(bool passed)
    {
        LevelCompleted?.Invoke(passed);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        gameData.EssenceAmount = startingEssence;
        StartTransition?.Invoke();
        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ContinueToNextLevel()
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        if (currentSceneIndex + 1 <= SceneManager.sceneCountInBuildSettings)
        {
            StartTransition?.Invoke();
            StartCoroutine(ContinueToNextLevelAfterDelay());
        }
        else
            Debug.LogWarning("Cannot load scene. No scene exists!");
    }

    IEnumerator ContinueToNextLevelAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void AddBonusEssence(int amt)
    {
        bonusEssence += amt;
    }
}
