using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpell : MonoBehaviour
{
    [SerializeField] float shieldAmount = 4f;
    [SerializeField] float shieldRadius = 3f;
    [SerializeField] float shieldDuration = 3f;

    private void Start()
    {
        var insects = FindObjectsOfType<Health>();
        foreach (var insect in insects)
        {
            if (Vector3.Distance(transform.position, insect.transform.position) <= shieldRadius)
                insect.ActivateDefenseBuff(shieldAmount, shieldDuration);
        }
    }
}
