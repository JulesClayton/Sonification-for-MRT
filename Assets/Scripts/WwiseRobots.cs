using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//template script for accessing environment data
public class WwiseRobots : MonoBehaviour
{   
    public GetEnvironmentData scanData;//drag the objects from the hierarchy, the objects must have a GetEnvironmentData script on them
    public CalcPriorities riskPriorities;

    //rad variables
    bool radFound = false;

    //temp variables
    float tempBongFreq;
    bool tempFound = false;
    Coroutine tempBongTrigger;

    //gas variables
    bool highGas = false;
    bool gasFound = false;
    
    

    [Header("Robot Stats")]
    [SerializeField] float radScanLevel;
    [SerializeField] float tempScanLevel;
    [SerializeField] float gasScanLevel;
    [SerializeField] float radPriorityLevel;
    [SerializeField] float tempPriorityLevel;
    [SerializeField] float gasPriorityLevel;

    [Header("Radiation")]
    //[SerializeField] float radMedPriority = 0.5f;

    [Header("Temperature")]
    [SerializeField] float minTempDuration = 0.5f;
    [SerializeField] float maxTempDuration = 5f;
    [Header("Gas")]
    [SerializeField] float gasHighThreshold = 0.8f;


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

        //RAD
        RadHandler();
        //medium priority switch trigger
        //RadMediumPrioritySwitch();

        //TEMP
        TempHandler();
        //lerp temp level into time scale for coroutine
        tempBongFreq = Mathf.Lerp(maxTempDuration, minTempDuration, tempScanLevel);

        //GAS
        GasHandler();
        //set switch for high gas threshold
        GasThresholdCheck();

    }
    
    //trigger Wwise events when robot collides with certain object tag
    
    void RadHandler()
    {
        if (radScanLevel > Mathf.Epsilon && !radFound)
        {
            AkSoundEngine.PostEvent("Rad_Play", gameObject);
            radFound = true;
        }
        if (radScanLevel <= Mathf.Epsilon && radFound)
        {
            AkSoundEngine.PostEvent("Rad_Stop", gameObject);
            radFound = false;
        }
    }
    void TempHandler()
    {
        if (tempScanLevel > Mathf.Epsilon && !tempFound)
        {
            tempBongTrigger = StartCoroutine(RepeatTempBongs());
            AkSoundEngine.PostEvent("Temp_Play", gameObject);
            tempFound = true;
            
        }
        if (tempScanLevel <= Mathf.Epsilon && tempFound)
        {
            StopCoroutine(tempBongTrigger);
            AkSoundEngine.PostEvent("Temp_Stop", gameObject);
            tempFound = false;
        }
    }
    void GasHandler()
    {
        if (gasScanLevel > Mathf.Epsilon && !gasFound)
        {
            AkSoundEngine.PostEvent("Gas_Play", gameObject);
            gasFound = true;
        }
        if (gasScanLevel <= Mathf.Epsilon && gasFound)
        {
            AkSoundEngine.PostEvent("Gas_Stop", gameObject);
            gasFound = false;
        }
    }

    //RAD
    // void RadMediumPrioritySwitch()
    // {
    //     if (radPriorityLevel >= radMedPriority - 0.1f
    //     || Mathf.Approximately (radPriorityLevel, radMedPriority)
    //     || Mathf.Approximately (radPriorityLevel, radMedPriority + 0.1f))
    //     {
    //         Debug.Log("Play Rad Switch");
    //         AkSoundEngine.PostEvent("Rad_MedPriority_Play", gameObject);
    //     }
    // }
    // void RadMediumPrioritySwitch()
    // {
    //     static int previousValue = 0;
    //     int currentValue = int (radPriorityLevel * 4);
    //     if(currentValue != previousValue)
    //     {
    //         previousValue = currentValue;
            
    //     }
    // }
    
    //GAS
    void GasThresholdCheck()
    {
        if (gasScanLevel >= gasHighThreshold && !highGas)
        {
            AkSoundEngine.SetSwitch("GasThreshold", "High", gameObject);
            //Debug.Log("Gas is high");
            highGas = true;
        }
        if ((gasScanLevel < gasHighThreshold) && (highGas == true))
        {
            AkSoundEngine.SetSwitch("GasThreshold", "Low", gameObject);
            //Debug.Log("Gas is low");
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