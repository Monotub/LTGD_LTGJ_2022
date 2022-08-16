//using Unity.VisualScripting.Antlr3.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [Header("General Setup")]
    [SerializeField] GameStatsSO gameData;
    [SerializeField] int startingEssence = 1000;

    public GameStatsSO GameData => gameData;
    public bool gameStarted { get; private set;}
    public bool paused { get; private set;}
    
    public static GameManager Instance;
    public static event Action<bool> PauseGame;

    Magic magic;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        gameStarted = false;
        paused = false;
        magic = FindObjectOfType<Magic>();
    }

    private void Start()
    {
        gameData.EssenceAmount = startingEssence;
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
            if (magic.spellSelected)
                magic.ClearSelectedSpell();
            else
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

    public void QuitGame()
    {
        Debug.Log("Quitting application!");
        Application.Quit();
    }
}
