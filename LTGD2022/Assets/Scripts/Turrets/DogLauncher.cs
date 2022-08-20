using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLauncher : Turret
{
    [Header("Dog Launcher Setup")]
    [SerializeField] GameObject rotor; // Rotate on X axis for up/down movement
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] int projectileDamage = 10;
    [SerializeField] float projectileSpeed = 5;

    float fireDelay;


    private new void Start()
    {
        base.Start();
        fireDelay = fireRate;
    }

    private new void Update()
    {
        base.Update();
        if (currentTarget != null && canFire && currentTarget.GetComponent<Insect>().targetable)
            FireAtWill();
    }

    void FireAtWill()
    {
        fireDelay -= Time.deltaTime;

        if (fireDelay <= 0)
        {
            var tmp = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            tmp.GetComponent<DogProjectile>().SetParameters(currentTarget, projectileDamage, projectileSpeed);
            fireDelay = fireRate;
        }
    }
}
