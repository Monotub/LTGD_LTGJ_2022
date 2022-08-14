using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class RapidTurret : Turret
{
    [Header("Rapid Fire Setup")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform leftBarrel;
    [SerializeField] Transform rightBarrel;
    [SerializeField] int damage;
    [SerializeField] float projectileSpeed;

    float fireDelay;
    bool leftFire = true;
    

    private new void Start()
    {
        base.Start();
        fireDelay = fireRate;
    }
    
    private new void Update()
    {
        base.Update();
        if(currentTarget != null && canFire && currentTarget.GetComponent<Insect>().targetable)
            FireAtWill();
    }

    void FireAtWill()
    {
        fireDelay -= Time.deltaTime;
        var spawnPos = Vector3.zero;

        if(fireDelay <= 0)
        {
            if (leftFire)
            {
                spawnPos = leftBarrel.position;
                leftFire = false;
            }
            else
            {
                spawnPos = rightBarrel.position;
                leftFire = true;
            }
            var tmp = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            tmp.GetComponent<Projectile>().SetParameters(currentTarget, damage, projectileSpeed);
            fireDelay = fireRate;
        }
    }
}
