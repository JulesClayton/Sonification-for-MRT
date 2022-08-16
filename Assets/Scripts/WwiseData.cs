using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//template script for accessing environment data
public class WwiseData : MonoBehaviour
{
    public GetEnvironmentData getEnvironmentData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        float test2 = getEnvironmentData.data["rad"][0];
    }
}
