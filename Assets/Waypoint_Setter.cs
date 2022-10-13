using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using TMPro;


public class Waypoint_Setter : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public GameObject waypoint;
    int waypoint_counter = 0;
    public float spawnheight = 0.35f;
    public List<RaycastHit> waypoints;
    public List<GameObject> wps;

    private void Awake()
    {
        //laserPointer.PointerIn += PointerInside;
        //laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
        waypoints = new List<RaycastHit>();
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        //Debug.Log(e.target.name + " was clicked");

        //get the hit point on the target

        //DEBUG print to console
        //spawn a waypoint pre-fab with a number
        //when nav button is clicked robot starts navigating to the waypoints
        //Debug.Log(e.hit.point);
        Vector3 spawnpoint = new Vector3(e.hit.point.x, e.hit.point.y + spawnheight, e.hit.point.z);
        waypoints.Add(e.hit);

        GameObject waypoint_obj = Instantiate(waypoint, spawnpoint, waypoint.transform.rotation);
        TextMeshProUGUI[] waypoint_num = waypoint_obj.GetComponentsInChildren<TextMeshProUGUI>();
        wps.Add(waypoint_obj);
        waypoint_num[0].text = waypoint_counter.ToString();
        waypoint_num[1].text = waypoint_counter.ToString();        
        waypoint_counter++;

        //TODO prevent unreachable waypoints
        //TODO add go button once waypoints are set
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
