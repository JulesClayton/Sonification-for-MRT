using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//template script for accessing environment data
public class WwiseRobots : MonoBehaviour
{   
    public GetEnvironmentData scanData;//drag the objects from the hierarchy, the objects must have a GetEnvironmentData script on them
    public CalcPriorities riskPriorities;

    //for temperature
    float tempBongFreq;
    Coroutine tempBongTrigger;
    //for gas
    bool highGas = false;

    //for > 0 triggers as alternative to trigger colliders
    float dataSum;
    bool hazardFound = false;
    

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
        AkSoundEngine.PostEvent("HazardFound", gameObject);

        
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

        //lerp temperature level into time scale for coroutine
        tempBongFreq = Mathf.Lerp(5f, 0.5f, tempScanLevel);

        //set switch for high gas threshold
        GasThresholdCheck();

        //for > 0 triggers
        dataSum = radScanLevel + tempScanLevel + gasScanLevel;
        InsideSphereCheck();
    }
    
    //trigger Wwise events when robot collides with certain object tag
    
    void InsideSphereCheck()
    {
        if(dataSum <= Mathf.Epsilon && !hazardFound)
        {
            Debug.Log("no hazard found");
            hazardFound = true;
        }
        if(dataSum > Mathf.Epsilon && hazardFound == true)
        {
            Debug.Log("hazard found");
            hazardFound = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("rad"))
        { 
            AkSoundEngine.PostEvent("Rad_Play", gameObject);
        }
        if (other.gameObject.CompareTag("temp"))
        {   
            tempBongTrigger = StartCoroutine(RepeatTempBongs());
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
            StopCoroutine(tempBongTrigger);
            AkSoundEngine.PostEvent("TempBong_Stop", gameObject);
            AkSoundEngine.PostEvent("Temp_Stop", gameObject);
        }
        if (other.gameObject.CompareTag("gas"))
        { 
            AkSoundEngine.PostEvent("Gas_Stop", gameObject);
        }

    }
    //GAS
    void GasThresholdCheck()
    {
        if (gasScanLevel >= 0.8f && !highGas)
        {
            AkSoundEngine.SetSwitch("GasThreshold", "High", gameObject);
            Debug.Log("Gas is high");
            highGas = true;
        }
        if ((gasScanLevel < 0.8f) && (highGas == true))
        {
            AkSoundEngine.SetSwitch("GasThreshold", "Low", gameObject);
            Debug.Log("Gas is low");
            highGas = false;
        }
    }

    //TEMPERATURE
    //Coroutine tying the temperature sfx's rhythmic duration to temperature level
    IEnumerator RepeatTempBongs()
    {
        while(true)
        {
        AkSoundEngine.PostEvent("TempBong_Play", gameObject);
        yield return new WaitForSeconds(tempBongFreq);
        }
    }
}

//need a public GetEnvironmentData and declaration name