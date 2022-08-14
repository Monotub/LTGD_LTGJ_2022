using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteSpell : MonoBehaviour
{
    [SerializeField] float hasteAmount = 1f;
    [SerializeField] float hasteRadius = 3f;

    private void Start()
    {
        var insects = FindObjectsOfType<Insect>();
        foreach (var insect in insects)
        {
            if (Vector3.Distance(transform.position, insect.transform.position) <= hasteRadius)
                insect.ModifyMoveSpeed(hasteAmount);
        }
    }
}
