using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcGasRisk : MonoBehaviour
{
    GetEnvironmentData robotData;
    InternalTemp internalTemp;
    CalcTempRisk tempRisk;
    public float gasRisk = 0;
    // Start is called before the first frame update
    void Start()
    {
        robotData = GetComponent<GetEnvironmentData>();
        internalTemp = GetComponent<InternalTemp>();
        tempRisk = GetComponent<CalcTempRisk>();
    }

    // Update is called once per frame
    void Update()
    {
        float totalGas = 0f;
        for (int gas_idx = 1; gas_idx < robotData.data["gas"].Count; gas_idx++)
        {
            totalGas += robotData.data["gas"][gas_idx];
        }

        if (totalGas > 1)
            totalGas = 1;
        gasRisk = (totalGas + tempRisk.tempRisk + robotData.data["rad"][0])/3;//internal temp as a proxy for spark chances and radiation can both ignite gas
    }
}
