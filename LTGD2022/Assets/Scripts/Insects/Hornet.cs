using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Hornet : Insect
{
    [Header("Hornet Setup")]
    [SerializeField] float speedModifier = 0.5f;
    [SerializeField] float hasteDuration = 1f;

    float timer = 0;

    private void Update()
    {
        HasteAbility();
    }

    void HasteAbility()
    {
        timer += Time.deltaTime;
        if (timer >= hasteDuration)
        {
            var insects = FindObjectsOfType<Insect>();
            foreach (var insect in insects)
            {
                if (Vector3.Distance(transform.position, insect.transform.position) <= auraRadius)
                {
                    insect.ActivateHaste(speedModifier, hasteDuration);
                }
            }
            timer = 0f;
        }
    }
}
