using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WwiseRobots : MonoBehaviour
{   
    public GetEnvironmentData scanData;//drag the objects from the hierarchy, the objects must have a GetEnvironmentData script on them

    //rad variables
    bool radFound = false;

    //temp variables
    float tempBongFreq;
    bool tempFound = false;
    
    //gas variables
    bool highGas = false;
    bool gasFound = false;
    
    //current data levels
    float radScanLevel;
    float tempScanLevel;
    float gasScanLevel;

    //tweakable thresholds
    [Header("Radiation")]

    [Header("Temperature")]
    [SerializeField] float minTempDuration = 0.5f;
    [SerializeField] float maxTempDuration = 5f;

    [Header("Gas")]
    [SerializeField] float gasHighThreshold = 0.8f;

    //coroutines
    Coroutine tempBongTrigger;


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
        
        //RAD
        RadHandler();

        //TEMP
        TempHandler();
        //lerp temp level into time scale for coroutine
        tempBongFreq = Mathf.Lerp(maxTempDuration, minTempDuration, tempScanLevel);

        //GAS
        GasHandler();
        //set switch for high gas threshold
        GasThresholdCheck();
    }

    //RAD

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

    //TEMPERATURE

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
    //coroutine mapping temperature sonification's rhythmic duration to temperature level
    IEnumerator RepeatTempBongs()
    {
        while(true)
        {
            yield return new WaitForSeconds(tempBongFreq);
            AkSoundEngine.PostEvent("TempBong_Play", gameObject);
        }
    }

    //GAS

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
    void GasThresholdCheck()
    {
        if (gasScanLevel >= gasHighThreshold && !highGas)
        {
            AkSoundEngine.SetSwitch("GasThreshold", "High", gameObject);
            highGas = true;
        }
        if ((gasScanLevel < gasHighThreshold) && (highGas == true))
        {
            AkSoundEngine.SetSwitch("GasThreshold", "Low", gameObject);
            highGas = false;
        }
    }
}