using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcTempRisk : MonoBehaviour
{
    GetEnvironmentData robotData;
    InternalTemp internalTemp;
    public float tempRisk = 0;
    //TODO set the constant to something sensible
    private static float TEMPCONSTANT = 0.1f;//abstract away the current temp calculation with a constant

    // Start is called before the first frame update
    void Start()
    {
        robotData = GetComponent<GetEnvironmentData>();
        internalTemp = GetComponent<InternalTemp>();
    }

    // Update is called once per frame
    void Update()
    {
        tempRisk = internalTemp.int_temp + (TEMPCONSTANT * robotData.data["temp"][0]);
        if (tempRisk > 1)
            tempRisk = 1;
    }
}
