using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimRobot : MonoBehaviour
{
    public Vector3[] targetpoints = new Vector3[4];//set the size of this array in the inspector and specify the target points
    int target_idx = 0;
    public float[] speed = new float[4];// = new float[] { 1, 2, 1, 1 };
    public float current_speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != targetpoints[target_idx])
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
