using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WwiseScanMode : MonoBehaviour
{
    public void HandleDropDown(int whichRobot)
    {
        if (whichRobot == 0)
        {
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot1"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot2"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot3"));
        }

        if (whichRobot == 1)
        {
            AkSoundEngine.SetSwitch("ScanMode", "Deep", GameObject.Find("SimRobot1"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot2"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot3"));            
        }

        if (whichRobot == 2)
        {
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot1"));
            AkSoundEngine.SetSwitch("ScanMode", "Deep", GameObject.Find("SimRobot2"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot3"));
        }
        if (whichRobot == 3)
        {
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot1"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot2"));
            AkSoundEngine.SetSwitch("ScanMode", "Deep", GameObject.Find("SimRobot3"));
        }
        // if (val == 3)
        // {
        //     Debug.Log("SimRobot4 Active");
        // }
    }
    void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot1"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot2"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot3"));
        }

        if (Input.GetKeyDown("1"))
        {
            AkSoundEngine.SetSwitch("ScanMode", "Deep", GameObject.Find("SimRobot1"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot2"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot3"));            
        }

        if (Input.GetKeyDown("2"))
        {
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot1"));
            AkSoundEngine.SetSwitch("ScanMode", "Deep", GameObject.Find("SimRobot2"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot3"));
        }
        if (Input.GetKeyDown("3"))
        {
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot1"));
            AkSoundEngine.SetSwitch("ScanMode", "Auto", GameObject.Find("SimRobot2"));
            AkSoundEngine.SetSwitch("ScanMode", "Deep", GameObject.Find("SimRobot3"));
        }
    }
}
