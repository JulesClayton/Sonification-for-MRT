using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentData : MonoBehaviour
{
    /* The aim is to have the script constantly generate a value depending on the proximity of the avatar.
     * Another script will run on the avatar to grab the data values from the data source scripts.
     */

    Collider avatar_collider;
    Collider env_collider;
    private float maxData;
    public float data_level;
    // Start is called before the first frame update
    void Start()
    {
        env_collider = GetComponent<Collider>();
        avatar_collider = FindObjectOfType<CapsuleCollider>();//ensure there is only 1 capsule collider in the env - either the test capsule or the player if VR is enabled
    }

    // Update is called once per frame
    void Update()
    {
        
       
        maxData = env_collider.transform.localScale.x / 2;
        Vector3 closestPointOnAvatar = avatar_collider.ClosestPointOnBounds(env_collider.transform.position);

        float data = (maxData) - Vector3.Distance(closestPointOnAvatar, env_collider.transform.position);             
                   
        data /= maxData;
        data_level = data;
    }
   
}
