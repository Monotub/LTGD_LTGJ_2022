using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Turret : MonoBehaviour
{
    [Header("Base Turret Setup")]
    [SerializeField] Transform turretTop;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float targetAquireRate = 0.5f; // Temporary until proven
    [SerializeField] GameObject empVFX;
    [SerializeField] GameObject virusVFX;

    protected List<Transform> insectsInRange = new List<Transform>();
    protected Transform currentTarget;
    protected bool canFire = true;


    protected void Start()
    {
        // Can possibly remove this once new script tested
        GetComponent<SphereCollider>().radius = attackRange;
        StartCoroutine(AquireTargets());
        empVFX.SetActive(false);
    }

    protected void Update()
    {
        if (currentTarget != null && canFire && currentTarget.GetComponent<Insect>().targetable
            && Vector3.Distance(transform.position, currentTarget.position) < attackRange)
        {
            turretTop.rotation = Quaternion.LookRotation(currentTarget.transform.position - turretTop.position);
        }

        CheckTargetDistance();
    }

    IEnumerator AquireTargets()
    {
        List<Health> allInsectsInLevel = new List<Health>();

        while (true)
        {
            Health closestTarget = null;
            allInsectsInLevel.Clear();
            var tmpArray = FindObjectsOfType<Health>();

            for (int i = 0; i < tmpArray.Length; i++)
                allInsectsInLevel.Add(tmpArray[i]);



            foreach (var insect in allInsectsInLevel)
            {
                float distanceToInsect = Vector3.Distance(transform.position, insect.gameObject.transform.position);

                if (distanceToInsect > attackRange) continue;

                if (closestTarget == null)
                    closestTarget = insect;
                else if (distanceToInsect < Vector3.Distance(transform.position, closestTarget.transform.position))
                    closestTarget = insect;
            }

            for (int i = 0; i < allInsectsInLevel.Count; i++)
            {
                if (allInsectsInLevel[i].GetComponent<Rhino>())
                    closestTarget = allInsectsInLevel[i];
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
        empVFX.SetActive(true);
 
        yield return wait;
        empVFX.SetActive(false);
        canFire = true;
    }

    public void ProcessVirus(float virusFactor, float virusDelay) => StartCoroutine(VirusDelay(virusFactor, virusDelay));

    IEnumerator VirusDelay(float virusFactor, float virusDelay)
    {
        var originalFireRate = fireRate;
        var wait = new WaitForSeconds(virusDelay);
        Debug.Log("Fire rate doubled");
        fireRate *= virusFactor;
        virusVFX.SetActive(true);
        yield return wait;
        Debug.Log("Fire rate restored");
        fireRate = originalFireRate;
        virusVFX.SetActive(false);
    }
}
