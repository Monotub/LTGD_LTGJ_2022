using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpSpell : MonoBehaviour
{
    [SerializeField] float stunTime = 2.5f;
    [SerializeField] float stunRadius = 3f;


    void Start()
    {
        var turrets = FindObjectsOfType<Turret>();

        foreach (var turret in turrets)
        {
            if(Vector3.Distance(transform.position, turret.transform.position) < stunRadius)
            {
                turret.ProcessEMP(stunTime);
            }
        }
    }

}
