using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Mantis : Insect
{
    [SerializeField] int regenModifier = 3;

    Magic magic;

    private void Awake()
    {
        magic = FindObjectOfType<Magic>();
    }

    private void OnEnable()
    {
        magic.ModifyManaRegen(regenModifier);
    }

    private void OnDisable()
    {
        magic.ModifyManaRegen(-regenModifier);
    }
}
