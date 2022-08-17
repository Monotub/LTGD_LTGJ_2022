using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : MonoBehaviour
{
    [SerializeField] SpellSO data;

    private void Start()
    {
        var insects = FindObjectsOfType<Health>();
        foreach (var insect in insects)
        {
            if (Vector3.Distance(transform.position, insect.transform.position) <= data.radius)
                insect.ProcessHeal(data.value);
        }
        
    }
}
