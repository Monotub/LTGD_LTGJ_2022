using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
public class Insect : MonoBehaviour
{
    [Header("Insect Setup")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] int essenceValue = 10;

    List<Transform> waypoints;
    Animator anim;
    NavMeshAgent navAgent;


    private void Start()
    {
        waypoints = FindObjectOfType<Path>().GetPath();
        anim = GetComponentInChildren<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(NavAlongPath());
        anim.SetTrigger("Running");

        navAgent.speed = moveSpeed;
    }

    IEnumerator NavAlongPath()
    {
        var wait = new WaitForEndOfFrame();
        for (int i = 0; i < waypoints.Count; i++)
        {
            while(Vector3.Distance(transform.position, waypoints[i].position) > 0.1f)
            {
                navAgent.SetDestination(waypoints[i].position);
                yield return wait;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "End Point")
        {
            Debug.Log($"{gameObject.name} reached the end!");
            Destroy(gameObject);
        }
    }


}
