using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Path : MonoBehaviour
{
    [SerializeField] Waypoint waypointPrefab;
    [SerializeField] List<Transform> waypoints = new List<Transform>();


    public List<Transform> GetPath() => waypoints;

    [Button("Add Waypoint")]
    [HorizontalGroup("Split", 0.5f)]
    void AddWaypoint()
    {
        var tmp = Instantiate(waypointPrefab);
        tmp.transform.SetParent(transform);
        tmp.transform.position = tmp.transform.position + (waypoints[0].position - waypoints[1].position) /2;
        Transform endPoint = waypoints[waypoints.Count - 1];
        waypoints.RemoveAt(waypoints.Count - 1);
        waypoints.Add(tmp.transform);
        waypoints.Add(endPoint);
    }

    [Button("Remove Waypoint")]
    [HorizontalGroup("Split")]
    void RemoveWaypoint()
    {
        if (waypoints[1] != waypoints[waypoints.Count - 1])
        {
            DestroyImmediate(waypoints[1].gameObject);
            waypoints.RemoveAt(1);
        }
    }


    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < waypoints.Count-1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
