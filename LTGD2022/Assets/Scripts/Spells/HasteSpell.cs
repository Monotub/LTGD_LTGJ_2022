using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteSpell : MonoBehaviour
{
    [SerializeField] SpellSO data;

    private void Start()
    {
        var insects = FindObjectsOfType<Insect>();
        foreach (var insect in insects)
        {
            if (Vector3.Distance(transform.position, insect.transform.position) <= data.radius)
                insect.ActivateHaste(data.value, data.duration);
        }
    }
}
