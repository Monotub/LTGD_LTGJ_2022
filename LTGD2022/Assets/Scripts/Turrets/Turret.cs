using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Turret : MonoBehaviour
{
    [Header("Base Turret Setup")]
    [SerializeField] Transform turretTop;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float targetAquireRate = 1f; // Temporary until proven

    protected List<Transform> insectsInRange = new List<Transform>();
    protected Transform currentTarget;
    protected bool canFire = true;


    protected void Start()
    {
        // Can possibly remove this once new script tested
        GetComponent<SphereCollider>().radius = attackRange;
        StartCoroutine(AquireTargets());
    }

    protected void Update()
    {
        if(currentTarget != null && canFire && currentTarget.GetComponent<Insect>().targetable)
        {
            turretTop.rotation = Quaternion.LookRotation(currentTarget.transform.position - turretTop.position);
        }

        CheckTargetDistance();
    }

    IEnumerator AquireTargets()
    {
        // Possibly change this to type Insect. Will have to test in engine
        List<Health> allInsectsInLevel = new List<Health>();

        while (true)
        {
            Health closestTarget = null;
            allInsectsInLevel.Clear();
            var tmpArray = FindObjectsOfType<Health>();

            for (int i = 0; i < tmpArray.Length; i++)
            {
                allInsectsInLevel.Add(tmpArray[i]);
            }

            // Assigns closest target to currentTarget. This may need to be changed if target switching is too fast.
            // Might also be able to slow down target aquisition
            foreach (var insect in allInsectsInLevel)
            {
                float distanceToInsect = Vector3.Distance(transform.position, insect.gameObject.transform.position);
                if (distanceToInsect > attackRange)
                    continue;

                if (closestTarget == null)
                    closestTarget = insect;
                else if (distanceToInsect < Vector3.Distance(transform.position, closestTarget.transform.position))
                    closestTarget = insect;
            }

            if(closestTarget != null)
                currentTarget = closestTarget.transform;
            yield return new WaitForSeconds(targetAquireRate);
        }
    }

    void CheckTargetDistance()
    {
        if (currentTarget == null) return;
        if (Vector3.Distance(transform.position, currentTarget.position) > attackRange)
            currentTarget = null;
    }

    //protected void OnTriggerEnter(Collider other)
    //{

    //    if (other.GetComponent<Insect>())
    //    {
    //        insectsInRange.Add(other.gameObject.transform);

    //        foreach (var insect in insectsInRange)
    //        {
    //            if (insect.TryGetComponent(out Rhino rhino))
    //            {
    //                currentTarget = rhino.transform;
    //                return;
    //            }
    //            else if (currentTarget == null)
    //            {
    //                currentTarget = other.gameObject.transform;
    //                return;
    //            }
    //            else currentTarget = null;
    //        }
    //    }
    //}

    //protected void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<Projectile>()) return;

    //    if (insectsInRange.Contains(other.gameObject.transform))
    //    {
    //        insectsInRange.Remove(other.gameObject.transform);
    //    }
    //    if(insectsInRange.Count > 0)
    //    {
    //        currentTarget = insectsInRange[0];
    //    }
    //    else
    //    {
    //        currentTarget = null;
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void ProcessEMP(float stunTime) => StartCoroutine(EmpDelay(stunTime));

    public IEnumerator EmpDelay(float stunTime)
    {
        var wait = new WaitForSeconds(stunTime);
        canFire = false;
        yield return wait;
        canFire = true;
    }

    public void ProcessVirus(float virusFactor, float virusDelay) => StartCoroutine(VirusDelay(virusFactor, virusDelay));

    IEnumerator VirusDelay(float virusFactor, float virusDelay)
    {
        var originalFireRate = fireRate;
        var wait = new WaitForSeconds(virusDelay);
        Debug.Log("Fire rate doubled");
        fireRate *= virusFactor;
        yield return wait;
        Debug.Log("Fire rate restored");
        fireRate = originalFireRate;
    }
}
