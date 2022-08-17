using UnityEngine;
using System;
using TMPro;


public class UIManager : MonoBehaviour
{
    [Header("General Setup")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] GameObject contextScreen;
    [SerializeField] Animator transitionAnim;

    [Header("Level Complete Screen")]
    [SerializeField] TMP_Text levelHeaderText;
    [SerializeField] GameObject levelRestartBtn;
    [SerializeField] GameObject levelContinueBtn;
    [SerializeField] TMP_Text partyTotalText;
    [SerializeField] TMP_Text partySurvived;
    [SerializeField] TMP_Text essenceStart;
    [SerializeField] TMP_Text essenceBonus;
    [SerializeField] TMP_Text essenceLost;
    [SerializeField] TMP_Text essenceTotal;

    Magic magic;

    bool optionsOpen = false;


    private void Awake()
    {
        magic = FindObjectOfType<Magic>();
    }

    private void OnEnable()
    {
        GameManager.PauseGame += TogglePauseScreen;
        GameManager.LevelCompleted += ToggleCompleteScreen;
        GameManager.StartTransition += OnStartTransition;
    }

    private void OnDisable()
    {
        GameManager.PauseGame -= TogglePauseScreen;
        GameManager.LevelCompleted -= ToggleCompleteScreen;
        GameManager.StartTransition -= OnStartTransition;
    }

    private void Update()
    {
        MonitorSpells();
    }

    void OnStartTransition()
    {
        transitionAnim.SetTrigger("End");
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

    public void ToggleCompleteScreen(bool passed)
    {
        levelCompletePanel.SetActive(true);
        if (passed)
        {
            levelHeaderText.text = "Level Completed";
            levelContinueBtn.SetActive(true);
            levelRestartBtn.SetActive(false);
        }
        else
        {
            levelHeaderText.text = "Level Failed";
            levelContinueBtn.SetActive(false);
            levelRestartBtn.SetActive(true);
        }

        partyTotalText.text = GameManager.Instance.partySize.ToString();
        partySurvived.text = GameManager.Instance.partyLived.ToString();
        essenceStart.text = GameManager.Instance.startingEssence.ToString();
        essenceBonus.text = GameManager.Instance.bonusEssence.ToString();
        essenceLost.text = GameManager.Instance.lostEssence.ToString();
        essenceTotal.text = ((GameManager.Instance.startingEssence + GameManager.Instance.bonusEssence)
            - GameManager.Instance.lostEssence).ToString();
    }

    void MonitorSpells()
    {
        if (magic.isSpellSelected)
            contextScreen.SetActive(true);
        else
            contextScreen.SetActive(false);
    }
}
