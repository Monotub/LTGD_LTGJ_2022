using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpSpell : MonoBehaviour
{
    [SerializeField] SpellSO data;


    void Start()
    {
        var turrets = FindObjectsOfType<Turret>();

        foreach (var turret in turrets)
        {
            if(Vector3.Distance(transform.position, turret.transform.position) < data.radius)
            {
                turret.ProcessEMP(data.duration);
            }
        }
    }

}
