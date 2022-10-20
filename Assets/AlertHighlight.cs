using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertHighlight : MonoBehaviour
{
    //call this function from the script were alerts are sounded and call it when an alert starts. Pass it Color.<alert colour> depending on the alert type.
    //use FindObjectOfType<AlertHighlight>().< function > ();

    public void PriorityAlert(Color color)
    {        
        Outline outline = gameObject.GetComponent<Outline>();
        outline.OutlineColor = color;//set the colour according to the alert type
        outline.enabled = true;
    }

    //when the robot is safe call this function
    public void RobotSafe()
    {
        Outline outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }
   
}
