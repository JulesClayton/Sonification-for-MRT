using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WwisePriorities : MonoBehaviour
{
    public CalcPriorities riskPriorities;

    bool isRadMedPriority = false;
    bool isRadHighPriority = false;
    bool isRadTopPriority = false;
    bool isTempMedPriority = false;
    bool isTempHighPriority = false;
    bool isTempTopPriority = false;
    bool isGasMedPriority = false;
    bool isGasHighPriority = false;
    bool isGasTopPriority = false;
    bool highPriority = false;
    bool highPriorityCheck;
    int radCurrentValue;
    int radPreviousValue = 0;
    int tempCurrentValue;
    int tempPreviousValue = 0;
    int gasCurrentValue;
    int gasPreviousValue = 0;
    bool radRising = true;
    bool tempRising = true;
    bool gasRising = true;

    [SerializeField] float radPriorityLevel;
    [SerializeField] float tempPriorityLevel;
    [SerializeField] float gasPriorityLevel;

    [Header("Radiation")]
    [SerializeField] float radMedPriority = 0.5f;
    [SerializeField] float radHighPriority = 0.9f;
    [SerializeField] float radTopPriority = 0.95f;

    [Header("Temperature")]
    [SerializeField] float tempMedPriority = 0.5f;

    [SerializeField] float tempHighPriority = 0.9f;

    [SerializeField] float tempTopPriority = 0.95f;

    [Header("Gas")]
    [SerializeField] float gasMedPriority = 0.5f;

    [SerializeField] float gasHighPriority = 0.9f;

    [SerializeField] float gasTopPriority = 0.95f;    

    Coroutine medRadPriorityState;
    Coroutine medTempPriorityState;
    Coroutine medGasPriorityState;
    WwiseScanMode scanMode;
    WwiseStateHandler wshRef;
    int robotID;

    void Start()
    {
       //scanMode = FindObjectOfType<WwiseScanMode>();
        robotID = gameObject.GetInstanceID();
        wshRef = gameObject.GetComponentInParent(typeof(WwiseStateHandler)) as WwiseStateHandler;
    }

    // Update is called once per frame
    void Update()
    {
        //float data from priority calculations
        radPriorityLevel = riskPriorities.priorities["rad"];
        tempPriorityLevel = riskPriorities.priorities["temp"];
        gasPriorityLevel = riskPriorities.priorities["gas"];

        //assign data to RTPCs
        AkSoundEngine.SetRTPCValue("RadPriority", radPriorityLevel, gameObject);
        AkSoundEngine.SetRTPCValue("TempPriority", tempPriorityLevel, gameObject);
        AkSoundEngine.SetRTPCValue("GasPriority", gasPriorityLevel, gameObject);

        //handle priority states and post medium priority alert events
        PriorityAlerts();

        //post high priority alert events
        PlayHighPriorityAlert();

        //set medium priority alert switch depending on data type and direction a data level is moving
        DataLevelsUpOrDown();
    }

    void PriorityAlerts()
    {
        MedRadAlert();
        HighRadAlert();
        MedTempAlert();
        HighTempAlert();
        MedGasAlert();
        HighGasAlert();
    }


    void MedRadAlert()
    {
        if ((radPriorityLevel > (radMedPriority - 0.001f) && radPriorityLevel < (radMedPriority + 0.001f)) && !isRadMedPriority)
        {
            isRadMedPriority = true;
            wshRef.setMedPriority(robotID, isRadMedPriority);
        
            if (radRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "RadUp", gameObject);
            }
            if (!radRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "RadDown", gameObject);
            }
            medRadPriorityState = StartCoroutine(medRadPrioritySequence());
            AkSoundEngine.PostEvent("MedPriority_Play", gameObject);
        }
    }
    void HighRadAlert()
    {
        if ((radPriorityLevel >= radHighPriority) && !isRadHighPriority)
        {
            isRadHighPriority = true;
            //scanMode.HandleDropDown(0);
            highPriority = true;

            AkSoundEngine.SetState("RadPriorities", "High");

            if ((radPriorityLevel >= radTopPriority) && !isRadTopPriority)
            {
                isRadTopPriority = true;
                AkSoundEngine.SetState("RadPriorities", "Top");
            }
            else if ((radPriorityLevel < radTopPriority) && isRadTopPriority)
            {
                isRadTopPriority = false;
                AkSoundEngine.SetState("RadPriorities", "High");
            }
        }

        if ((radPriorityLevel < radHighPriority) && isRadHighPriority)
        {
            isRadHighPriority = false;
            AkSoundEngine.SetState("RadPriorities", "Normal");
        }
    }
    void MedTempAlert()
    {
        if ((tempPriorityLevel > (tempMedPriority - 0.001f) && tempPriorityLevel < (tempMedPriority + 0.001f)) && !isTempMedPriority)
        {
            isTempMedPriority = true;
            wshRef.setMedPriority(robotID, isTempMedPriority);

            if (tempRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "TempUp", gameObject);
            }
            if (!tempRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "TempDown", gameObject);
            }
            medTempPriorityState = StartCoroutine(medTempPrioritySequence());
            AkSoundEngine.PostEvent("MedPriority_Play", gameObject);
        }
    }
    void HighTempAlert()
    {
        if ((tempPriorityLevel >= tempHighPriority) && !isTempHighPriority)
        {
            isTempHighPriority = true;
            highPriority = true;
            //scanMode.HandleDropDown(0);
            AkSoundEngine.SetState("TempPriorities", "High");

            if ((tempPriorityLevel >= tempTopPriority) && !isTempTopPriority)
            {
                isTempTopPriority = true;
                AkSoundEngine.SetState("TempPriorities", "Top");
            }
            else if ((tempPriorityLevel < tempTopPriority) && isTempTopPriority)
            {
                isTempTopPriority = false;
                AkSoundEngine.SetState("TempPriorities", "High");
            }
        }
        if ((tempPriorityLevel < tempHighPriority) && isTempHighPriority)
        {
            isTempHighPriority = false;
            AkSoundEngine.SetState("TempPriorities", "Normal");
        }
    }
    void MedGasAlert()
    {
        if ((gasPriorityLevel > (gasMedPriority - 0.001f) && gasPriorityLevel < (gasMedPriority + 0.001f)) && !isGasMedPriority)
        {
            isGasMedPriority = true;
            wshRef.setMedPriority(robotID, isGasMedPriority);

            if (gasRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "GasUp", gameObject);
            }
            if (!gasRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "GasDown", gameObject);
            }
            medGasPriorityState = StartCoroutine(medGasPrioritySequence());
            AkSoundEngine.PostEvent("MedPriority_Play", gameObject);
        }
    }
    void HighGasAlert()
    {
        if ((gasPriorityLevel >= gasHighPriority) && !isGasHighPriority)
        {
            isGasHighPriority = true;
            highPriority = true;
            //scanMode.HandleDropDown(0);
            AkSoundEngine.SetState("GasPriorities", "High");

            if ((gasPriorityLevel >= gasTopPriority) && !isGasTopPriority)
            {
                isGasTopPriority = true;
                AkSoundEngine.SetState("GasPriorities", "Top");
            }
            else if ((gasPriorityLevel < gasTopPriority) && isGasTopPriority)
            {
                isGasTopPriority = false;
                AkSoundEngine.SetState("GasPriorities", "High");
            }
        }

        if ((gasPriorityLevel < gasHighPriority) && isGasHighPriority)
        {
            isGasHighPriority = false;
            AkSoundEngine.SetState("GasPriorities", "Normal");
        }
    }
    void PlayHighPriorityAlert()
    {
        if (!isRadHighPriority && !isTempHighPriority && !isGasHighPriority)
        {
            highPriority = false;
        }
        if (highPriority != highPriorityCheck)
        {
            highPriorityCheck = highPriority;
            wshRef.setHighPriority(robotID, highPriority);

            if (highPriority)
            {
                AkSoundEngine.PostEvent("HighPriority_Play", gameObject);
            }
            if (!highPriority)
            {
                AkSoundEngine.PostEvent("HighPriority_Stop", gameObject);
            }
        }
    }
    void DataLevelsUpOrDown()
    {
        radCurrentValue = (int)(radPriorityLevel * 10f);
        if(radCurrentValue > radPreviousValue)
        {
            radPreviousValue = radCurrentValue;
            radRising = true;
        }
        if(radCurrentValue < radPreviousValue)
        {
            radPreviousValue = radCurrentValue;
            radRising = false;
        }
        tempCurrentValue = (int)(tempPriorityLevel * 10f);
        if(tempCurrentValue > tempPreviousValue)
        {
            tempPreviousValue = tempCurrentValue;
            tempRising = true;
        }
        if(tempCurrentValue < tempPreviousValue)
        {
            tempPreviousValue = tempCurrentValue;
            tempRising = false;
        }
        gasCurrentValue = (int)(gasPriorityLevel * 10f);
        if (gasCurrentValue > gasPreviousValue)
        {
            gasPreviousValue = gasCurrentValue;
            gasRising = true;
        }
        if (gasCurrentValue < gasPreviousValue)
        {
            gasPreviousValue = gasCurrentValue;
            gasRising = false;
        }
    }
    //coroutines automating the switch to a medium priority state so the alerts can be heard clearly
    IEnumerator medRadPrioritySequence()
    {
        AkSoundEngine.SetState("RadPriorities", "Med");
        yield return new WaitForSeconds(1);
        AkSoundEngine.SetState("RadPriorities", "Normal");
        isRadMedPriority = false;
        wshRef.setMedPriority(robotID, isRadMedPriority);

    }
    IEnumerator medTempPrioritySequence()
    {
        AkSoundEngine.SetState("TempPriorities", "Med");
        yield return new WaitForSeconds(1);
        AkSoundEngine.SetState("TempPriorities", "Normal");
        isTempMedPriority = false;
        wshRef.setMedPriority(robotID, isTempMedPriority);
    }
    IEnumerator medGasPrioritySequence()
    {
        AkSoundEngine.SetState("GasPriorities", "Med");
        yield return new WaitForSeconds(1);
        AkSoundEngine.SetState("GasPriorities", "Normal");
        isGasMedPriority = false;
        wshRef.setMedPriority(robotID, isGasMedPriority);

    }
}
