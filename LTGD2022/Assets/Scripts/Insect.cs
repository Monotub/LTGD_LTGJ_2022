using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    List<Transform> waypoints;

    bool canMove = false;
    float minDistance = 0.01f;


    private void Start()
    {
        waypoints = FindObjectOfType<Path>().GetPath();
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position = waypoints[0].position;
            MoveAlongPath();
        }
    }

    [ContextMenu("Start Moving")]
    void StartMovement()
    {
        canMove = true;
    }

    void MoveAlongPath()
    {
        if(Vector3.Distance(transform.position, waypoints[0].position) < minDistance)
        {
            Debug.Log("Waypoint Reached");
            waypoints.RemoveAt(0);
        }
        else
        {
            transform.position += Vector3.Lerp(transform.position, waypoints[0].position, moveSpeed * Time.deltaTime);
        }
        if(waypoints.Count == 0)
            canMove = false;
    }
}
