using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpell : MonoBehaviour
{
    [SerializeField] SpellSO data;
    [SerializeField] AudioClip clip;

    AudioSource sfx;

    private void Awake()
    {
        sfx = GetComponent<AudioSource>();
    }

    private void Start()
    {
        var insects = FindObjectsOfType<Health>();
        sfx.PlayOneShot(clip);
        foreach (var insect in insects)
        {
            if (Vector3.Distance(transform.position, insect.transform.position) <= data.radius)
                insect.ActivateDefenseBuff(data.value, data.duration);
        }
    }
}
