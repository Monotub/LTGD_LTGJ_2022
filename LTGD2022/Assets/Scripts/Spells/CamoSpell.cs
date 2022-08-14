using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamoSpell : MonoBehaviour
{
    [SerializeField] float camoDuration = 2f;
    [SerializeField] float camoRadius = 3f;

    private void Start()
    {
        var insects = FindObjectsOfType<Insect>();
        foreach (var insect in insects)
        {
            if (Vector3.Distance(transform.position, insect.transform.position) <= camoRadius)
                insect.ActivateCamo(camoDuration);
        }

    }
}
