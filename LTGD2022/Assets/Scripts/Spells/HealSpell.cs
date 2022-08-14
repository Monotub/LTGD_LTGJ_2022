using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : MonoBehaviour
{
    [SerializeField] float healAmount = 4f;
    [SerializeField] float healRadius = 3f;

    private void Start()
    {
        var insects = FindObjectsOfType<Health>();
        foreach (var insect in insects)
        {
            if (Vector3.Distance(transform.position, insect.transform.position) <= healRadius)
                insect.ProcessHeal(healAmount);
        }
        
    }
}
