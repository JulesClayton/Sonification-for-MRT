//Get all the priorities from the other scripts and store them in a dict

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcPriorities : MonoBehaviour
{
    CalcRadRisk radRisk;
    CalcTempRisk tempRisk;
    CalcGasRisk gasRisk;
    GetEnvironmentData robotData;
    public Dictionary<string, float> priorities;
    public float MAXRADS = 10000;
    public float RADFACTOR = 2f;

    public float radPriority = 0;
    public float tempPriority = 0;
    public float gasPriority = 0;

    // Start is called before the first frame update
    void Start()
    {
        radRisk = GetComponent<CalcRadRisk>();
        tempRisk = GetComponent<CalcTempRisk>();
        gasRisk = GetComponent<CalcGasRisk>();
        robotData = GetComponent<GetEnvironmentData>();
        priorities = new Dictionary<string, float>();
        priorities["rad"] = 0;
        priorities["temp"] = 0;
        priorities["gas"] = 0;
        //Debug.Log(robotData.data["rad"][0]);
    }

    // Update is called once per frame
    void Update()
    {
        //TODO make these calcs correct and in range 0-1
        //totalRads is increased by 0-1 every frame of exposure, so can grow large quickly
        //MAXRADS needs to be fairly high to not have <1 for that bit
        //current rad exposure is 0-1 
        //Debug.Log(robotData.data["rad"][0]);
        float totalRads = radRisk.totalRads;
        if (totalRads > MAXRADS)
            totalRads = MAXRADS;        
        priorities["rad"] = ((1 - (MAXRADS - totalRads)/MAXRADS) + RADFACTOR * robotData.data["rad"][0]) /(RADFACTOR + 1);
        priorities["temp"] = tempRisk.tempRisk;
        priorities["gas"] = gasRisk.gasRisk;

        radPriority = priorities["rad"];
        tempPriority = priorities["temp"];
        gasPriority = priorities["gas"];
    }
}
