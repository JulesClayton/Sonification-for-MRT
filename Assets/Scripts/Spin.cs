using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotateSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        print("delta time " + Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        //float t = 0;
        //t += Time.deltaTime;
        //Quaternion yAxis = Quaternion.Euler(0, Time.deltaTime * rotateSpeed, 0);
        //transform.rotation = new Quaternion(transform.rotation.x + yAxis.x, transform.rotation.y + yAxis.y, transform.rotation.z + yAxis.z, transform.rotation.w + yAxis.w);

        transform.Rotate(Vector3.forward, 0.5f);// rotateSpeed * Time.deltaTime);


    }
}
