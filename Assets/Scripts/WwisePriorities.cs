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
    bool topPriority = false;
    bool topPriorityCheck;
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
    [SerializeField] float radHighPriority = 0.85f;
    [SerializeField] float radTopPriority = 1f;

    [Header("Temperature")]
    [SerializeField] float tempMedPriority = 0.5f;

    [SerializeField] float tempHighPriority = 0.85f;

    [SerializeField] float tempTopPriority = 1f;

    [Header("Gas")]
    [SerializeField] float gasMedPriority = 0.5f;

    [SerializeField] float gasHighPriority = 0.85f;

    [SerializeField] float gasTopPriority = 1f;    

    Coroutine medRadPriorityPlaying;
    Coroutine medTempPriorityPlaying;
    Coroutine medGasPriorityPlaying;
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

        //post medium priority alert events
        MedRadAlert();
        MedTempAlert();
        MedGasAlert();

        //check if hazards are a high or top priority
        HighRadPriority();
        TopRadPriority();
        HighTempPriority();
        TopTempPriority();
        HighGasPriority();
        TopGasPriority();

        //check if robot is in high priority state and post alert events
        AreAnyPrioritiesHigh();

        //check if robot is in top priority state
        AreAnyPrioritiesTop();

        //set medium priority alert switch depending on data type and direction a data level is moving
        DataLevelsUpOrDown();
    }

    void MedRadAlert()
    {
        if ((radPriorityLevel > (radMedPriority - 0.001f) && radPriorityLevel < (radMedPriority + 0.001f)) && !isRadMedPriority)
        {
            isRadMedPriority = true;
            wshRef.SetMedPriority(robotID, isRadMedPriority);
        
            if (radRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "RadUp", gameObject);
            }
            if (!radRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "RadDown", gameObject);
            }
            medRadPriorityPlaying = StartCoroutine(medRadPrioritySequence());
            AkSoundEngine.PostEvent("MedPriority_Play", gameObject);
        }
    }
    void HighRadPriority()
    {
        if ((radPriorityLevel >= radHighPriority) && !isRadHighPriority)
        {
            isRadHighPriority = true;
            //scanMode.HandleDropDown(0);
            highPriority = true;
        }
        if ((radPriorityLevel < radHighPriority) && isRadHighPriority)
        {
            isRadHighPriority = false;
        }
    }
    void TopRadPriority()
    {
        if ((radPriorityLevel >= radTopPriority) && !isRadTopPriority)
        {
            isRadTopPriority = true;
            topPriority = true;
            AkSoundEngine.SetState("RadPriorities", "Top");
        }
        if ((radPriorityLevel < radTopPriority) && isRadTopPriority)
        {
            isRadTopPriority = false;
            AkSoundEngine.SetState("RadPriorities", "High");
        }
    }

    void MedTempAlert()
    {
        if ((tempPriorityLevel > (tempMedPriority - 0.001f) && tempPriorityLevel < (tempMedPriority + 0.001f)) && !isTempMedPriority)
        {
            isTempMedPriority = true;
            wshRef.SetMedPriority(robotID, isTempMedPriority);

            if (tempRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "TempUp", gameObject);
            }
            if (!tempRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "TempDown", gameObject);
            }
            medTempPriorityPlaying = StartCoroutine(medTempPrioritySequence());
            AkSoundEngine.PostEvent("MedPriority_Play", gameObject);
        }
    }
    void HighTempPriority()
    {
        if ((tempPriorityLevel >= tempHighPriority) && !isTempHighPriority)
        {
            isTempHighPriority = true;
            highPriority = true;
            //scanMode.HandleDropDown(0);
        }
        if ((tempPriorityLevel < tempHighPriority) && isTempHighPriority)
        {
            isTempHighPriority = false;
        }
    }
    void TopTempPriority()
    {
        if ((tempPriorityLevel >= tempTopPriority) && !isTempTopPriority)
        {
            isTempTopPriority = true;
            topPriority = true;
            AkSoundEngine.SetState("TempPriorities", "Top");
        }
        if ((tempPriorityLevel < tempTopPriority) && isTempTopPriority)
        {
            isTempTopPriority = false;
            AkSoundEngine.SetState("TempPriorities", "High");
        }
    }
    void MedGasAlert()
    {
        if ((gasPriorityLevel > (gasMedPriority - 0.001f) && gasPriorityLevel < (gasMedPriority + 0.001f)) && !isGasMedPriority)
        {
            isGasMedPriority = true;
            wshRef.SetMedPriority(robotID, isGasMedPriority);

            if (gasRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "GasUp", gameObject);
            }
            if (!gasRising)
            {
                AkSoundEngine.SetSwitch("MediumPriorityAlert", "GasDown", gameObject);
            }
            medGasPriorityPlaying = StartCoroutine(medGasPrioritySequence());
            AkSoundEngine.PostEvent("MedPriority_Play", gameObject);
        }
    }
    void HighGasPriority()
    {
        if ((gasPriorityLevel >= gasHighPriority) && !isGasHighPriority)
        {
            isGasHighPriority = true;
            highPriority = true;
            //scanMode.HandleDropDown(0);
        }
        if ((gasPriorityLevel < gasHighPriority) && isGasHighPriority)
        {
            isGasHighPriority = false;
        }
    }
    void TopGasPriority()
    {
        if ((gasPriorityLevel >= gasTopPriority) && !isGasTopPriority)
        {
            isGasTopPriority = true;
            AkSoundEngine.SetState("GasPriorities", "Top");
        }
        if ((gasPriorityLevel < gasTopPriority) && isGasTopPriority)
        {
            isGasTopPriority = false;
            AkSoundEngine.SetState("GasPriorities", "High");
        }
    }


    void AreAnyPrioritiesHigh()
    {
        if (!isRadHighPriority && !isTempHighPriority && !isGasHighPriority)
        {
            highPriority = false;
        }
        if (highPriority != highPriorityCheck)
        {
            highPriorityCheck = highPriority;
            wshRef.SetHighPriority(robotID, highPriority);

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
    void AreAnyPrioritiesTop()
    {
        if (!isRadTopPriority && !isTempTopPriority && !isGasTopPriority)
        {
            topPriority = false;
        }
        if (topPriority != topPriorityCheck)
        {
            topPriorityCheck = topPriority;
            wshRef.SetTopPriority(robotID, topPriority);
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
    //coroutines preventing alerts being triggered too often when float fluctuates around medium priority threshold
    IEnumerator medRadPrioritySequence()
    {
        yield return new WaitForSeconds(2);
        isRadMedPriority = false;
        wshRef.SetMedPriority(robotID, isRadMedPriority);
    }
    IEnumerator medTempPrioritySequence()
    {
        yield return new WaitForSeconds(2);
        isTempMedPriority = false;
        wshRef.SetMedPriority(robotID, isTempMedPriority);
    }
    IEnumerator medGasPrioritySequence()
    {
        yield return new WaitForSeconds(2);
        isGasMedPriority = false;
        wshRef.SetMedPriority(robotID, isGasMedPriority);
    }
}
