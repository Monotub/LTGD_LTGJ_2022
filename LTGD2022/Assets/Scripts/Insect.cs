using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Insect : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    List<Transform> waypoints;
    Animator anim;
    bool isMoving = false;


    private void Start()
    {
        waypoints = FindObjectOfType<Path>().GetPath();
        anim = GetComponentInChildren<Animator>();
    }

    [ContextMenu("Start Moving")]
    void StartPathMovement() => StartCoroutine(MoveAlongPath());

    IEnumerator MoveAlongPath()
    {
        int nextWaypoint = 0;
        WaitForSeconds wait = new WaitForSeconds(moveSpeed);

        anim.SetBool("Moving", true);

        while(transform.position != waypoints[waypoints.Count - 1].position)
        {
            transform.rotation = Quaternion.LookRotation(waypoints[nextWaypoint].position - transform.position, Vector3.up);

            transform.DOMove(waypoints[nextWaypoint].position, moveSpeed)
                .SetEase(Ease.Linear);

            nextWaypoint++;
            yield return wait;
        }
        Debug.Log("End Reached");
        anim.SetBool("Moving", false);
    }
}
