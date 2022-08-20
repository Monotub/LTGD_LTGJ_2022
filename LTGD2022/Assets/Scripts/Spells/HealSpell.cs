using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealSpell : MonoBehaviour
{
    [SerializeField] SpellSO data;
    [SerializeField] AudioClip spellClip;

    AudioSource sfx;

    private void Awake()
    {
        sfx = GetComponent<AudioSource>();
    }

    private void Start()
    {
        var insects = FindObjectsOfType<Health>();

        sfx.PlayOneShot(spellClip);
        
        foreach (var insect in insects)
        {
            if (Vector3.Distance(transform.position, insect.transform.position) <= data.radius)
                insect.ProcessHeal(data.value);
        }
        
    }
}
