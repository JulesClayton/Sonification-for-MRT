using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcRadRisk : MonoBehaviour
{
    GetEnvironmentData robotData;
    public float totalRads = 0;
    // Start is called before the first frame update
    void Start()
    {
        robotData = GetComponent<GetEnvironmentData>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int rad_idx = 1; rad_idx < robotData.data["rad"].Count; rad_idx++)
        {
            totalRads += robotData.data["rad"][rad_idx];
        }
    }
}
