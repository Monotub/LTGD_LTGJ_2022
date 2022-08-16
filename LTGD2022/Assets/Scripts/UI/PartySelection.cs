using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PartySelection : MonoBehaviour
{
    [Header("General Setup")]
    [SerializeField] GameStatsSO GameData;
    [SerializeField] int startingEssence = 250;
    [SerializeField] GameObject partyWindows;

    [Header("Insect Info")]
    [SerializeField] Image portrait;
    [SerializeField] TMP_Text insectName;
    [SerializeField] TMP_Text abilityText;
    [SerializeField] TMP_Text descText;
    [SerializeField] TMP_Text essenceCost;
    [SerializeField] TMP_Text currentEssence;

    [Header("Party Screen")]
    [SerializeField] Sprite defaultImage;
    [SerializeField] Image[] partySlots = new Image[9];

    InsectStatsSO[] activeParty = new InsectStatsSO[9];
    InsectStatsSO currentSelection;

    public static event Action<InsectStatsSO[]> PartySelected;
    public static event Action StartGame;


    private void Update()
    {
        currentEssence.text = GameManager.Instance.GameData.EssenceAmount.ToString();
    }

    public void PopulateInfo(InsectStatsSO stats)
    {
        currentSelection = stats;
        portrait.sprite = stats.Portrait;
        insectName.text = stats.DisplayName;
        abilityText.text = stats.AbilityDesc;
        descText.text = stats.Description;
        essenceCost.text = stats.EssenceCost.ToString();
    }

    public void AddInsect()
    {
        if (currentSelection.EssenceCost > GameData.EssenceAmount) return;

        for (int i = 0; i < activeParty.Length; i++)
        {
            if (activeParty[i] == null)
            {
                GameData.EssenceAmount -= currentSelection.EssenceCost;
                activeParty[i] = currentSelection;
                partySlots[i].sprite = currentSelection.Portrait;
                return;
            }
        }
    }

    void ClearSelection()
    {
        currentSelection = null;
        portrait.sprite = null;
        insectName.text = "";
        abilityText.text = "";
        descText.text = "";
        essenceCost.text = 0.ToString();
    }

    public void AcceptParty()
    {
        int partySize = 0;

        if (activeParty[0] == null) return;

        for (int i = 0; i < activeParty.Length; i++)
        {
            if (activeParty[i] != null)
                partySize++;
        }

        PartySelected?.Invoke(activeParty);
        GameManager.Instance.SetPartySize(partySize);
        StartGame?.Invoke();
        partyWindows.SetActive(false);
    }

    public void ResetParty()
    {
        for (int i = 0; i < activeParty.Length; i++)
        {
            if(activeParty[i] != null)
                GameData.EssenceAmount += activeParty[i].EssenceCost;

            activeParty[i] = null;
            partySlots[i].sprite = defaultImage;
        }
    }
}
