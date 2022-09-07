using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//template script for accessing environment data
public class WwiseRobots : MonoBehaviour
{   
    public GetEnvironmentData robotData;//drag the objects from the hierarchy, the objects must have a GetEnvironmentData script on them
    
    [Header("Wwise Events")]
    public AK.Wwise.Event RadPlay;
    public AK.Wwise.Event RadStop;
    public AK.Wwise.Event TempPlay;
    public AK.Wwise.Event TempStop;
    public AK.Wwise.Event GasPlay;
    public AK.Wwise.Event GasStop;
    
    [Header("Wwise RTPCs")]
    public AK.Wwise.RTPC RadLevel;
    public AK.Wwise.RTPC TempLevel;
    public AK.Wwise.RTPC GasLevel;

    [Header("Robot Stats")]
    public float robotRad;
    public float robotTemp;
    public float robotGas;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {        
        robotRad = robotData.data["rad"][0];
        robotTemp = robotData.data["temp"][0];
        robotGas = robotData.data["gas"][0];

        RadLevel.SetValue(gameObject, robotRad);
        TempLevel.SetValue(gameObject, robotTemp);
        GasLevel.SetValue(gameObject, robotGas);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("rad"))
        { 
            RadPlay.Post(gameObject);
        }
        if (other.gameObject.CompareTag("temp"))
        { 
            TempPlay.Post(gameObject);
        }
        if (other.gameObject.CompareTag("gas"))
        { 
            GasPlay.Post(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("rad"))
        { 
            RadStop.Post(gameObject);
        }
        if (other.gameObject.CompareTag("temp"))
        { 
            TempStop.Post(gameObject);
        }
        if (other.gameObject.CompareTag("gas"))
        { 
            GasStop.Post(gameObject);
        }
    }
}

//need a public GetEnvironmentData and declaration name