using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBonus : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] GameStatsSO gameData;

    [Header("Bonuses")]
    [Tooltip("Bonus essence at the start of the level")]
    [SerializeField] int startEssenceBonus;

    private void Start()
    {
        gameData.EssenceAmount += startEssenceBonus;
    }
}
