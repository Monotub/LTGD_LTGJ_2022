using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Hornet : Insect
{
    [SerializeField] float speedModifier = 0.5f;

    private void OnEnable()
    {
        var insects = FindObjectsOfType<Insect>();

        foreach (var insect in insects)
        {
            if(Vector3.Distance(transform.position, insect.transform.position) <= auraRadius)
            {
                insect.ModifyMoveSpeed(speedModifier);
            }
        }
    }

    private void OnDisable()
    {
        var insects = FindObjectsOfType<Insect>();

        foreach (var insect in insects)
        {
            if (Vector3.Distance(transform.position, insect.transform.position) <= auraRadius)
            {
                insect.ModifyMoveSpeed(-speedModifier);
            }
        }
    }
}
