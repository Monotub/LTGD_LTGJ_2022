using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpell : MonoBehaviour
{
    [SerializeField] float virusRadius = 3f;
    [SerializeField] float virusFactor = 2f;
    [SerializeField] float virusDelay = 2.5f;
    

    void Start()
    {
        var turrets = FindObjectsOfType<Turret>();

        foreach (var turret in turrets)
        {
            if (Vector3.Distance(transform.position, turret.transform.position) < virusRadius)
            {
                turret.ProcessVirus(virusFactor, virusDelay);
            }
        }
    }
}
