using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHazardVisibility : MonoBehaviour
{
    Renderer[] rs;
    // Start is called before the first frame update
    void Start()
    {
        rs = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            foreach (Renderer r in rs)
            {
                r.enabled = !(r.enabled);

            }
        }
    }
}
