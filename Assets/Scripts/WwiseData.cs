using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//template script for accessing environment data
public class WwiseData : MonoBehaviour
{
    public GetEnvironmentData avatarData;//add one of these for each thing that interacts with the data and assign in the inspector 
    public GetEnvironmentData robot1Data;//drag the objects from the hierarchy, the objects must have a GetEnvironmentData script on them
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        float test2 = avatarData.data["rad"][0];
    }
}
