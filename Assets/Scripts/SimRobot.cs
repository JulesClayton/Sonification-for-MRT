using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimRobot : MonoBehaviour
{
    public Vector3[] targetpoints = new Vector3[4];//set the size of this array in the inspector and specify the target points
    public int target_idx = 0;
    public float[] speed = new float[4];// = new float[] { 1, 2, 1, 1 };
    public float current_speed;
    float distance;
    NavMeshAgent meshAgent;
    UseWaypoints useWaypoints;
    // Start is called before the first frame update
    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        useWaypoints = GetComponent<UseWaypoints>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled && !useWaypoints.mappingMode)
        {
            if (meshAgent)
            {
                if (target_idx < targetpoints.Length)
                {
                    //Debug.Log((targetpoints[target_idx].x).ToString() + " " + targetpoints[target_idx].z.ToString());
                    //Debug.Log(transform.position.x.ToString() + " " + transform.position.z.ToString());
                    Vector2 target = new Vector2(targetpoints[target_idx].x, targetpoints[target_idx].z);
                    Vector2 position = new Vector2(transform.position.x, transform.position.z);
                    distance = Vector2.Distance(position, target);
                    meshAgent.speed = speed[target_idx];
                    current_speed = meshAgent.speed;

                    if (distance > 0.01)
                    {
                        meshAgent.SetDestination(targetpoints[target_idx]);
                    }
                    else
                    {
                        target_idx++;
                    }
                }
            }
            else
            {
                if (transform.position != targetpoints[target_idx])
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetpoints[target_idx], speed[target_idx] * Time.deltaTime);
                    current_speed = speed[target_idx];
                }
                else
                {
                    target_idx++;
                    if (target_idx == targetpoints.Length)
                        target_idx = 0;
                }
            }
        }
    }
}
