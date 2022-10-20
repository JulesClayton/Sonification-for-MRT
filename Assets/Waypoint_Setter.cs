using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class Waypoint_Setter : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public GameObject waypoint;
    public int waypoint_counter = 0;
    public float spawnheight = 0.35f;
    public List<Vector3> waypoints;
    public List<GameObject> wps;
    public Button goToWaypoints;
    public Button deselectRobot;
    public bool waypointmode = false;
    GameObject selectedRobot;
    

    private void Awake()
    {
        //laserPointer.PointerIn += PointerInside;
        //laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;

        waypoints = new List<Vector3>();  
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        //Debug.Log(e.target.name + " was clicked");

        //get the hit point on the target

        //DEBUG print to console
        //spawn a waypoint pre-fab with a number
        //when nav button is clicked robot starts navigating to the waypoints
        //Debug.Log(e.hit.point);
        if (waypointmode)
        {
            if (e.target.name == gameObject.transform.name)
            {
                NavMeshHit navMeshHit;
                NavMesh.SamplePosition(e.hit.point, out navMeshHit, 0.5f, NavMesh.AllAreas);

                //Vector3 spawnpoint = new Vector3(e.hit.point.x, e.hit.point.y + spawnheight, e.hit.point.z);
                Vector3 spawnpoint = new Vector3(navMeshHit.position.x, navMeshHit.position.y + spawnheight, navMeshHit.position.z);
                waypoints.Add(spawnpoint);

                GameObject waypoint_obj = Instantiate(waypoint, spawnpoint, waypoint.transform.rotation);
                TextMeshProUGUI[] waypoint_num = waypoint_obj.GetComponentsInChildren<TextMeshProUGUI>();
                wps.Add(waypoint_obj);
                waypoint_num[0].text = waypoint_counter.ToString();
                waypoint_num[1].text = waypoint_counter.ToString();
                waypoint_counter++;
            }
            else if(e.target.tag == "waypoint")
            {
                waypoints.Remove(e.target.position);
                wps.Remove(e.target.gameObject);
                Destroy(e.target.gameObject);
                                
            }
            else if (e.target.name == goToWaypoints.transform.name)
            {
                //goToWaypoints.onClick.Invoke();
                selectedRobot.GetComponent<UseWaypoints>().GoToWaypoints();
            }
            else if (e.target.name == deselectRobot.name)
            {
                waypointmode = false;
                selectedRobot.GetComponent<UseWaypoints>().MappingModeSet(false);
                selectedRobot.GetComponent<Outline>().enabled = false;
                selectedRobot = null;
            }
        }
        else
        {            
            if (e.target.tag == "robot")//if you point at a robot
            {
                Outline outline;
                if (e.target.TryGetComponent(out outline))
                {
                    selectedRobot = e.target.gameObject;//set so that go to waypoints can be called for the selected robot when the button is pressed                    
                    waypointmode = true;
                    UseWaypoints useWaypoints = selectedRobot.GetComponent<UseWaypoints>();
                    useWaypoints.MappingModeSet(true);
                    useWaypoints.StopRobot();
                    selectedRobot = e.target.gameObject;
                    outline.OutlineColor = Color.green;
                    outline.enabled = true;
                    goToWaypoints.gameObject.SetActive(true);
                    deselectRobot.gameObject.SetActive(true);
                }
            }
            
        }

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
