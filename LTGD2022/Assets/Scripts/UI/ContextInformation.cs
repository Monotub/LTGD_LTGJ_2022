using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ContextInformation : MonoBehaviour
{
    [SerializeField] TMP_Text spellName;
    [SerializeField] TMP_Text spellDescription;
    [SerializeField] TMP_Text manaCost;
    [SerializeField] TMP_Text spellEffect;
    [SerializeField] TMP_Text spellRadius;
    [SerializeField] TMP_Text spellDuration;

    Magic magic;
    SpellSO spell;


    private void Awake()
    {
        magic = FindObjectOfType<Magic>();
    }

    private void OnEnable()
    {
        UpdateContextInfo();
    }

    public void UpdateContextInfo()
    {
        spell = magic.GetCurrentSpellInfo();

        spellName.text = spell.displayName;
        spellDescription.text = spell.description;
        manaCost.text = spell.cost.ToString();

        if (spell.value == 0)
            spellEffect.text = "-";
        else
            spellEffect.text = spell.value.ToString();

        if (spell.radius == 0)
            spellRadius.text = "-";
        else
            spellRadius.text = spell.radius.ToString();

        if (spell.duration == 0)
            spellDuration.text = "-";
        else
            spellDuration.text = spell.duration.ToString();
    }
}
