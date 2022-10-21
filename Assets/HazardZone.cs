using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class HazardZone : MonoBehaviour
{
    NavMeshModifierVolume volume;
    NavMeshSurface surface;
    public int area = 2;

    public void SetHazard()
    {
        volume.area = area;
        surface.BuildNavMesh();
    }

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<NavMeshModifierVolume>();
        surface = GetComponentInParent<NavMeshSurface>();
        //Debug.Log("area " + volume.area.ToString());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
