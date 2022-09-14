using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WwiseScanMode : MonoBehaviour
{
    int whichRobot;
    void Start()
    {
        whichRobot = 0;
    }
    
    public void HandleDropDown(int whichRobot)
    {
        if (whichRobot == 0)
        {
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot1"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot2"));
        }

        if (whichRobot == 1)
        {
            AkSoundEngine.SetSwitch("ScanMode", "Deep", GameObject.Find("SimRobot1"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot2"));            
        }

        if (whichRobot == 2)
        {
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot1"));
            AkSoundEngine.SetSwitch("ScanMode", "Deep", GameObject.Find("SimRobot2"));
        }
        // if (val == 3)
        // {
        //     Debug.Log("SimRobot4 Active");
        // }
    }
}
