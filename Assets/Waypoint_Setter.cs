using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

public class Waypoint_Setter : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public GameObject waypoint;
    int waypoint_counter = 0;
    public float spawnheight = 0.35f;
    public List<Vector3> waypoints;
    public List<GameObject> wps;
    public Button button;
    

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
        if (e.target.name == button.transform.name)
        {
            button.onClick.Invoke();
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
