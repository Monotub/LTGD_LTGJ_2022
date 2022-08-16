using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnDelay = 1f;

    private void OnEnable()
    {
        PartySelection.PartySelected += SpawnParty;
    }

    private void OnDisable()
    {
        PartySelection.PartySelected -= SpawnParty;
    }

    void SpawnParty(InsectStatsSO[] activeParty)
    {
        StartCoroutine(SpawnInsectsOnDelay(activeParty));
    }

    IEnumerator SpawnInsectsOnDelay(InsectStatsSO[] party)
    {
        var wait = new WaitForSeconds(spawnDelay);

        foreach (var insect in party)
        {
            if(insect != null)
            {
                var tmp = Instantiate(insect.Prefab, spawnPoint.position, Quaternion.identity);
                yield return wait;
            }
        }

    }
}
