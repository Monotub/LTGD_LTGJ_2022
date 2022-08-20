using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;


[RequireComponent(typeof(Health))]
public class Insect : MonoBehaviour
{
    [Header("Insect Setup")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] protected float auraRadius = 5f;
    [SerializeField] protected InsectStatsSO stats;

    public InsectStatsSO Stats => stats;
    public bool targetable { get; private set; }

    public static event Action<int> InsectCompleted;

    int essenceValue;
    List<Transform> waypoints;
    Animator anim;
    NavMeshAgent navAgent;


    protected void Start()
    {
        waypoints = FindObjectOfType<Path>().GetPath();
        this.anim = GetComponentInChildren<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(NavAlongPath());
        anim.SetTrigger("Running");

        navAgent.speed = moveSpeed;
        targetable = true;
        essenceValue = stats.EssenceCost;
    }


    IEnumerator NavAlongPath()
    {
        var wait = new WaitForEndOfFrame();
        for (int i = 0; i < waypoints.Count; i++)
        {
            while(Vector3.Distance(transform.position, waypoints[i].position) > 0.1f)
            {
                if(navAgent)
                    navAgent.SetDestination(waypoints[i].position);
                yield return wait;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "End Point")
        {
            InsectCompleted?.Invoke(essenceValue);
            if (gameObject.TryGetComponent(out Ladybug ladybug))
                GameManager.Instance.AddBonusEssence(ladybug.bonusEssence);
            Destroy(gameObject);
        }
    }

    public void OnDeath()
    {
        GetComponent<Collider>().enabled = false;
        targetable = false;
        navAgent.enabled = false;
        anim.SetTrigger("Die");
        
    }

    public void ActivateHaste(float amt, float dur)
    {
        ModifyMoveSpeed(amt);
        StartCoroutine(RevertMoveSpeed(amt, dur));
    }

    public void ModifyMoveSpeed(float amt)
    {
        moveSpeed += amt;
        navAgent.speed = moveSpeed;
    }

    IEnumerator RevertMoveSpeed(float amt, float dur)
    {
        yield return new WaitForSeconds(dur);
        ModifyMoveSpeed(-amt);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, auraRadius);
    }

    public void ActivateCamo(float duration)
    {
        StartCoroutine(ProcessCamo(duration));
    }

    IEnumerator ProcessCamo(float dur)
    {
        var skin = GetComponentInChildren<SkinnedMeshRenderer>();
        skin.enabled = false;
        targetable = false;
        yield return new WaitForSeconds(dur);
        skin.enabled = true;
        targetable = true;
    }
}
