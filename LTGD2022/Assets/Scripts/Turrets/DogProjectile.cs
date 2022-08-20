using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogProjectile : Projectile
{
    new void Start()
    {
        base.Start();
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    new void Update()
    {
        
        base.Update();
    }

    new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
