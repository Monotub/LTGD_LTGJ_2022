using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [Header("Default Turret Setup")]
    [SerializeField] Transform turretTop;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float fireRate;

    protected List<Transform> insectsInRange = new List<Transform>();
    protected Transform currentTarget;

    protected void Start()
    {
        GetComponent<SphereCollider>().radius = attackRange;
    }

    protected void Update()
    {
        if(currentTarget != null)
        {
            turretTop.rotation = Quaternion.LookRotation(currentTarget.transform.position - turretTop.position);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<Insect>())
        {
            insectsInRange.Add(other.gameObject.transform);

            foreach (var insect in insectsInRange)
            {
                if (insect.TryGetComponent(out Rhino rhino))
                {
                    currentTarget = rhino.transform;
                    return;
                }
                else if (currentTarget == null)
                {
                    currentTarget = other.gameObject.transform;
                    return;
                }
                else currentTarget = null;
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Projectile>()) return;

        if (insectsInRange.Contains(other.gameObject.transform))
        {
            insectsInRange.Remove(other.gameObject.transform);
        }
        if(insectsInRange.Count > 0)
        {
            currentTarget = insectsInRange[0];
        }
        else
        {
            currentTarget = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
