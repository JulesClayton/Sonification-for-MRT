using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnvironmentData : MonoBehaviour
{
    public string[] dataTags = new string[] { "rad", "temp", "gas" };//fill with the tags used for all the different data types we need to capture
    public Dictionary<string, List<float>> data = new Dictionary<string, List<float>>();
    Dictionary<string, GameObject[]> dataSources = new Dictionary<string, GameObject[]>();

    public List<float> testdata = new List<float>();

    // Start is called before the first frame update
    void Start()
    {

        foreach(string tag in dataTags)
        {
            dataSources.Add(tag, GameObject.FindGameObjectsWithTag(tag));
            List<float> dataValues = new List<float>();
            foreach(GameObject dataSource in dataSources[tag])
            {
                //dataValues.Add(dataSource.GetComponent<EnvironmentData>().data_level);
                //testdata.Add(dataSource.GetComponent<EnvironmentData>().data_level);
                dataValues.Add(0);
                testdata.Add(0);
            }
            data.Add(tag, dataValues);
        }

        //foreach (string key in data.Keys)
            //Debug.Log(key);
    }

    // Update is called once per frame
    void Update()
    {
        //each update populate the data dict with all the data values
        foreach (string tag in dataTags)
        {
            List<float> dataValues = new List<float>();
            foreach (GameObject dataSource in dataSources[tag])
            {                
                //dataValues.Add(dataSource.GetComponent<EnvironmentData>().data_level);
                float maxData = dataSource.transform.localScale.x / 2;
                Vector3 closestPointOnAvatar = gameObject.GetComponent<Collider>().ClosestPointOnBounds(dataSource.transform.position);

                float data = (maxData) - Vector3.Distance(closestPointOnAvatar, dataSource.transform.position);

                data /= maxData;
                dataValues.Add(data);
            }
            data[tag] = dataValues;
        }

        //process the data values to get the parameters needed for sonification. For each data type the values experienced by the avatar is stored in an array in the dict using the tag for that data type as a key.
        float highestData;
        foreach(string key in data.Keys)
        {
            highestData = 0f;
            for(int i = 0; i < data[key].Count; i++)
            {
                if (data[key][i] < 0)
                    data[key][i] = 0;
                if (data[key][i] > highestData)
                    highestData = data[key][i];
            }

            data[key].Insert(0, highestData);
        }

        //TODO add data collection related to the robots

        //for testing purposes data values will be updated in the inspecter. Make sure to set the size of the array in the inspector to the right size.

        if (data.Keys.Count < 3)
        {
            foreach (string key in data.Keys)
                Debug.Log(key);
        }

        testdata[0] = data["rad"][0];
        testdata[1] = data["temp"][0];
        testdata[2] = data["gas"][0];
    }
}
