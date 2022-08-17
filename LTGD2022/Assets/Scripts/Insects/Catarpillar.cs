using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[SelectionBase]
public class Catarpillar : Insect
{
    [Header("Caterpillar Setup")]
    [SerializeField] float defenseBonus = 1f;
    [SerializeField] float duration = 1f;
    float timer = 0;

    private void Update()
    {
        DefenseAbility();
    }

    void DefenseAbility()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            var insects = FindObjectsOfType<Health>();
            foreach (var insect in insects)
            {
                if (Vector3.Distance(transform.position, insect.transform.position) <= auraRadius)
                {
                    insect.ActivateDefenseBuff(defenseBonus, duration);
                }
            }
            timer = 0f;
        }
    }
}
