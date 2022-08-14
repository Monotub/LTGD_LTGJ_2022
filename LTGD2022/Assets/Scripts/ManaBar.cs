using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ManaBar : MonoBehaviour
{
    [SerializeField] TMP_Text manaText;
    [SerializeField] Image manaBar;

    Magic magic;


    private void Awake()
    {
        magic = FindObjectOfType<Magic>();
    }

    void Update()
    {
        manaText.text = magic.currentMana.ToString();
        manaBar.fillAmount = (float)magic.currentMana / (float)magic.maxMana;
    }
}
