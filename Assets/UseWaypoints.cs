using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UseWaypoints : MonoBehaviour
{
    NavMeshAgent meshAgent;
    Waypoint_Setter waypoint_Setter;
    Vector2 target;
    int waypoint_counter = 0;
    public float distance = 0;
    bool go = false;    

    // Start is called before the first frame update
    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        waypoint_Setter = FindObjectOfType<Waypoint_Setter>();
        target = new Vector2(transform.position.x, transform.position.z);        
    }

    public void GoToWaypoints()
    {
        go = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (go)
        {
            if (waypoint_Setter.waypoints.Count > 0)
            {
                target = new Vector2(waypoint_Setter.waypoints[0].x, waypoint_Setter.waypoints[0].z);
                Vector2 position = new Vector2(transform.position.x, transform.position.z);
                distance = Vector2.Distance(position, target);

                if (distance > 0.01)
                {
                    meshAgent.SetDestination(waypoint_Setter.waypoints[0]);
                }
                else
                {
                    waypoint_Setter.waypoints.RemoveAt(0);
                    GameObject wp = waypoint_Setter.wps[0];
                    waypoint_Setter.wps.RemoveAt(0);
                    Destroy(wp);
                }
            }
            else
            {
                go = false;
            }
        }
    }
}
