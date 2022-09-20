//simulate the changing internal temp of the robot

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalTemp : MonoBehaviour
{
    public float int_temp = 0f;//temp measured in range 0-1ish, with base temp set as 0 and high temp 1
    SimRobot simRobot;
    GetEnvironmentData robotData;
    //TODO change these to statics once we are 100% happy with the values - left public to allow tweaknig in the inspector
    public float HEATING_FACTOR = 0.001f;//multiplier to calculate the change in temperature due to speed
    public float HEAT_THRESHOLD = 0.5f;//determines whether to increase or decrease heat - if speed is above the threshold temp goes up
    public float TEMPCONSTANT = 0.005f;//abstract away the current temp calculation with a constant

    // Start is called before the first frame update
    void Start()
    {
        simRobot = GetComponent<SimRobot>();
        robotData = GetComponent<GetEnvironmentData>();
    }

    // Update is called once per frame
    void Update()
    {
        //change internal temp based on motor speed
        if (int_temp + HEATING_FACTOR * (simRobot.current_speed - HEAT_THRESHOLD) >= 0)
            int_temp += HEATING_FACTOR * (simRobot.current_speed - HEAT_THRESHOLD);
        else
            int_temp = 0;
        
        //TODO add a temp change based on env temp
        int_temp += TEMPCONSTANT * (robotData.data["temp"][0] - int_temp);

        if (int_temp > 1)
            int_temp = 1;
        else if (int_temp < 0)
            int_temp = 0;
    }
}
