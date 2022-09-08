using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//template script for accessing environment data
public class WwiseRobots : MonoBehaviour
{   
    public GetEnvironmentData scanData;//drag the objects from the hierarchy, the objects must have a GetEnvironmentData script on them
    public CalcPriorities riskPriorities;

    [Header("Robot Stats")]
    public float radScanLevel;
    public float tempScanLevel;
    public float gasScanLevel;
    public float radPriorityLevel;
    public float tempPriorityLevel;
    public float gasPriorityLevel;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        //float data from environment scans     
        radScanLevel = scanData.data["rad"][0];
        tempScanLevel = scanData.data["temp"][0];
        gasScanLevel = scanData.data["gas"][0];
        //assign data to RTPCs
        AkSoundEngine.SetRTPCValue("RadLevel", radScanLevel, gameObject);
        AkSoundEngine.SetRTPCValue("TempLevel", tempScanLevel, gameObject);
        AkSoundEngine.SetRTPCValue("GasLevel", gasScanLevel, gameObject);
        //float data from priority calculations
        radPriorityLevel = riskPriorities.priorities["rad"];
        tempPriorityLevel = riskPriorities.priorities["temp"];
        gasPriorityLevel = riskPriorities.priorities["gas"];

        //assign data to RTPCs
        AkSoundEngine.SetRTPCValue("RadPriority", radPriorityLevel, gameObject);
        AkSoundEngine.SetRTPCValue("TempPriority", tempPriorityLevel, gameObject);
        AkSoundEngine.SetRTPCValue("GasPriority", gasPriorityLevel, gameObject);
        //RadPriority.SetValue(gameObject, radPriorityLevel);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("rad"))
        { 
            AkSoundEngine.PostEvent("Rad_Play", gameObject);
        }
        if (other.gameObject.CompareTag("temp"))
        { 
            AkSoundEngine.PostEvent("Temp_Play", gameObject);
        }
        if (other.gameObject.CompareTag("gas"))
        { 
            AkSoundEngine.PostEvent("Gas_Play", gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("rad"))
        { 
            AkSoundEngine.PostEvent("Rad_Stop", gameObject);
        }
        if (other.gameObject.CompareTag("temp"))
        { 
           AkSoundEngine.PostEvent("Temp_Stop", gameObject);
        }
        if (other.gameObject.CompareTag("gas"))
        { 
            AkSoundEngine.PostEvent("Gas_Stop", gameObject);
        }
    }
}

//need a public GetEnvironmentData and declaration name