using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Magic : MonoBehaviour
{
    [SerializeField] GameObject[] spellHighlights;
    [SerializeField] GameObject[] spellPrefab;
    [SerializeField] SpellSO[] spellSOs = new SpellSO[6];
    [SerializeField] int manaRegenFactor = 5;

    public int spellIndex { get; private set; }
    public bool isSpellSelected { get; private set; }
    public int currentMana { get; private set; }
    public int maxMana { get; private set; }

    Camera cam;
    ContextInformation context;

    float regenTimer = 0f;


    private void Start()
    {
        cam = Camera.main;
        maxMana = 200;
        currentMana = maxMana;
        context = FindObjectOfType<ContextInformation>(true);
        isSpellSelected = false;
    }

    private void Update()
    {
        HandleSpellInput();
        CastSelectedSpell();
        ManaRegeneration();
    }

    private void HandleSpellInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HighlightSpell(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HighlightSpell(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HighlightSpell(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HighlightSpell(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            HighlightSpell(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            HighlightSpell(5);
        }
    }

    public void HighlightSpell(int index)
    {
        if (!GameManager.Instance.gameStarted) return;

        for (int i = 0; i < spellHighlights.Length; i++)
        {
            if(i == index)
            {
                spellHighlights[i].gameObject.SetActive(true);
                spellIndex = i;
            }
            else
                spellHighlights[i].gameObject.SetActive(false);
        }
        isSpellSelected = true;
        context.UpdateContextInfo();
    }

    public void ClearSelectedSpell()
    {
        foreach (var spell in spellHighlights)
            spell.SetActive(false);
        isSpellSelected = false;
    }

    void CastSelectedSpell()
    {
        if (!isSpellSelected) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (spellSOs[spellIndex].cost <= currentMana &&
                    spellHighlights[spellIndex].GetComponentInParent<SpellSlot>().CanCast())
                {
                    Instantiate(spellPrefab[spellIndex], hit.point, Quaternion.identity);
                    currentMana -= spellSOs[spellIndex].cost;
                    StartRecastTimer(spellIndex);
                }
            }
        }
    }

    private void StartRecastTimer(int _spellIndex)
    {
        spellHighlights[_spellIndex].GetComponentInParent<SpellSlot>().StartRecastTimer();
    }

    public void ManaRegeneration()
    {
        if (currentMana >= maxMana) return;

        regenTimer += Time.deltaTime;

        if(regenTimer > 1)
        {
            currentMana += manaRegenFactor;
            regenTimer = 0;
        }
    }

    public void ModifyManaRegen(int amt)
    {
        manaRegenFactor += amt;
    }

    public int GetRegenFactor() => manaRegenFactor;

    public SpellSO GetCurrentSpellInfo() => spellSOs[spellIndex];
}
