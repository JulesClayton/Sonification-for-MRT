using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class ObjectDetected : MonoBehaviour
{

    public NavMeshSurface surface;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        //turn on the mesh renderer of objects collided with
        MeshRenderer meshRenderer;
        if (other.gameObject.TryGetComponent(out meshRenderer))
        {
            if (!meshRenderer.enabled)
            {
                meshRenderer.enabled = true;
                surface.BuildNavMesh();
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
