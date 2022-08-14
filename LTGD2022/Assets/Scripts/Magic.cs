using System;
using UnityEngine;
using UnityEngine.UI;


public class Magic : MonoBehaviour
{
    [SerializeField] GameObject[] spellHighlights;
    [SerializeField] GameObject[] spellPrefab;
    [SerializeField] int manaRegenFactor = 5;

    public bool spellSelected { get; private set; }
    public int currentMana { get; private set; }
    public int maxMana { get; private set; }

    Camera cam;
    
    float regenTimer = 0f;
    int[] spellCost = new int[6]
    {
        100,
        75,
        50,
        50,
        50,
        50
    };
    int spellIndex;


    private void Start()
    {
        maxMana = 200;
        currentMana = maxMana;
        cam = Camera.main;
        spellSelected = false;
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
        spellSelected = true;
    }

    void ClearSelectedSpell()
    {
        foreach (var spell in spellHighlights)
            spell.SetActive(false);
        spellSelected = false;
    }

    void CastSelectedSpell()
    {
        if (!spellSelected) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //if(hit.transform.name == "Terrain")
                //{
                    if (spellCost[spellIndex] <= currentMana &&
                        spellHighlights[spellIndex].GetComponentInParent<SpellSlot>().CanCast())
                    {
                        Instantiate(spellPrefab[spellIndex], hit.point, Quaternion.identity);
                        // TODO: Uncomment this!! Commented out for testing!
                        //currentMana -= spellCost[spellIndex];
                        //StartRecastTimer(spellIndex);
                    }
                //}
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
}
