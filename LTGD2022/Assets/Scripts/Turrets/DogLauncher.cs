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
    float rotorRotationMax = -25f;

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
            StartCoroutine(RotateRotor());
            //var tmp = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            //tmp.GetComponent<DogProjectile>().SetParameters(currentTarget, projectileDamage, projectileSpeed);
            fireDelay = fireRate;
        }
    }

    IEnumerator RotateRotor()
    {
        var rot = rotor.transform.localRotation;
        rot.x = rotorRotationMax;
        Vector3 fireRot = new Vector3(rot.x, 0, 0);

        var tmp = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        tmp.GetComponent<DogProjectile>().SetParameters(currentTarget, projectileDamage, projectileSpeed);
        yield return new WaitForSeconds(0.25f);
        rot.x = 0;

    }
}
