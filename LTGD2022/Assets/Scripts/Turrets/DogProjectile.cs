using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogProjectile : Projectile
{
    [Header("Dog Projectile")]
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float horizLaunchFactor = 5;
    [SerializeField] float vertLaunchFactor = 5;
    [SerializeField] float explosionRadius = 4f;

    Vector3 startScale = new Vector3(0.3f, 0.3f, 0.3f);
    Vector3 endScale = new Vector3(0.8f, 0.8f, 0.8f);
    Vector3 impactPoint;
    Rigidbody rb;

    AudioSource explAudio;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    new void Start()
    {
        base.Start();
        transform.localScale = startScale;
        rb.AddForce(Vector3.up * vertLaunchFactor, ForceMode.Impulse);
        rb.AddForce(transform.forward * horizLaunchFactor, ForceMode.Impulse);
        explAudio = explosionPrefab.GetComponent<AudioSource>();
    }

    new void Update()
    {
        moveTime += Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(target - transform.position);
        transform.localScale = Vector3.Lerp(startScale, endScale, moveTime * 2.5f);
    }

    new void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Terrain" || other.tag == "Insect")
        {
            Debug.Log($"Collided with: {other.gameObject.name}");
            impactPoint = transform.position;
            DealSplashDamage();
        }
    }

    void DealSplashDamage()
    {
        var insects = FindObjectsOfType<Health>();
        Vector3 explosionLoc = new Vector3(impactPoint.x, impactPoint.y + 1, impactPoint.z);

        foreach(var unit in insects)
        {
            var distToImpact = Vector3.Distance(unit.gameObject.transform.position, impactPoint);
            if (distToImpact <= explosionRadius && !unit.isDead)
            {

                GetComponentInChildren<MeshRenderer>().enabled = false;
                Instantiate(explosionPrefab, explosionLoc, Quaternion.identity);
                
                if(distToImpact <= explosionRadius * 0.3f)
                    unit.ProcessHit(damage);
                else if (distToImpact <= explosionRadius * 0.6f)
                    unit.ProcessHit(damage - 3);
                else
                    unit.ProcessHit(damage - 6);

                Destroy(gameObject, 0.1f);
            }
        }
    }
}
