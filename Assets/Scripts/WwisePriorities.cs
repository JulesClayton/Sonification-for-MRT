using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //priority event and state methods
        RadPriorityAlerts();
        TempPriorityAlerts();
        GasPriorityAlerts();
    }

    void RadPriorityAlerts()
    {
        if ((radPriorityLevel > (radMedPriority - 0.001f) && radPriorityLevel < (radMedPriority + 0.001f)) && !isRadMedPriority)
        {
            isRadMedPriority = true;
            medRadPriorityState = StartCoroutine(medRadPrioritySequence());
            AkSoundEngine.PostEvent("Rad_Priority_Med_Alert", gameObject);
        }

        if ((radPriorityLevel >= radHighPriority) && !isRadHighPriority)
        {
            isRadHighPriority = true;
            AkSoundEngine.SetState("RadPriorities", "High");
            AkSoundEngine.PostEvent("Rad_Priority_High_Alert_Play", gameObject);

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
            AkSoundEngine.PostEvent("Rad_Priority_High_Alert_Stop", gameObject);
        }
    }
    void TempPriorityAlerts()
    {
        if ((tempPriorityLevel > (tempMedPriority - 0.001f) && tempPriorityLevel < (tempMedPriority + 0.001f)) && !isTempMedPriority)
        {
            isTempMedPriority = true;
            medTempPriorityState = StartCoroutine(medTempPrioritySequence());
            AkSoundEngine.PostEvent("Temp_Priority_Med_Alert", gameObject);
        }
        if ((tempPriorityLevel >= tempHighPriority) && !isTempHighPriority)
        {
            isTempHighPriority = true;
            AkSoundEngine.SetState("TempPriorities", "High");
            AkSoundEngine.PostEvent("Temp_Priority_High_Alert_Play", gameObject);

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
            AkSoundEngine.PostEvent("Temp_Priority_High_Alert_Stop", gameObject);
        }
    }
    void GasPriorityAlerts()
    {
        if ((gasPriorityLevel > (gasMedPriority - 0.001f) && gasPriorityLevel < (gasMedPriority + 0.001f)) && !isGasMedPriority)
        {
            isGasMedPriority = true;
            medGasPriorityState = StartCoroutine(medGasPrioritySequence());
            AkSoundEngine.PostEvent("Gas_Priority_Med_Alert", gameObject);
        }

        if ((gasPriorityLevel >= gasHighPriority) && !isGasHighPriority)
        {
            isGasHighPriority = true;
            AkSoundEngine.SetState("GasPriorities", "High");
            AkSoundEngine.PostEvent("Gas_Priority_High_Alert_Play", gameObject);

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
            AkSoundEngine.PostEvent("Gas_Priority_High_Alert_Stop", gameObject);
        }
    }

    //coroutines automating the switch to a medium priority state so the alerts can be heard clearly
    IEnumerator medRadPrioritySequence()
    {
        AkSoundEngine.SetState("RadPriorities", "Med");
        yield return new WaitForSeconds(1);
        AkSoundEngine.SetState("RadPriorities", "Normal");
        isRadMedPriority = false;
    }
    IEnumerator medTempPrioritySequence()
    {
        AkSoundEngine.SetState("TempPriorities", "Med");
        yield return new WaitForSeconds(1);
        AkSoundEngine.SetState("TempPriorities", "Normal");
        isTempMedPriority = false;
    }
    IEnumerator medGasPrioritySequence()
    {
        AkSoundEngine.SetState("GasPriorities", "Med");
        yield return new WaitForSeconds(1);
        AkSoundEngine.SetState("GasPriorities", "Normal");
        isGasMedPriority = false;
    }
}
