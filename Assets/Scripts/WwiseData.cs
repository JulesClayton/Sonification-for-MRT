using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//template script for accessing environment data
public class WwiseData : MonoBehaviour
{
    public GetEnvironmentData avatarData;//add one of these for each thing that interacts with the data and assign in the inspector 
    public GetEnvironmentData robotData;//drag the objects from the hierarchy, the objects must have a GetEnvironmentData script on them

    public AK.Wwise.Event RadPlay;
    public AK.Wwise.RTPC RadLevel;

    // Start is called before the first frame update
    void Start()
    {
        RadPlay.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {        
        float robotRad = robotData.data["rad"][0];
        float robotTemp = robotData.data["temp"][0];
        float robotGas = robotData.data["gas"][0];

        RadLevel.SetValue(gameObject, robotRad);

    }
}

//need a public GetEnvironmentData and declaration name