using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnvironmentData : MonoBehaviour
{
    public string[] dataTags;//fill with the tags used for all the different data types we need to capture
    Dictionary<string, List<float>> data = new Dictionary<string, List<float>>();
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
                dataValues.Add(dataSource.GetComponent<EnvironmentData>().data_level);
                testdata.Add(dataSource.GetComponent<EnvironmentData>().data_level);
            }
            data.Add(tag, dataValues);
        }
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
                dataValues.Add(dataSource.GetComponent<EnvironmentData>().data_level);                
            }
            data[tag] = dataValues;
        }

        //process the data values to get the parameters needed for sonification. For each data type the values experienced by the avatar is stored in an array in the dict using the tag for that data type as a key.
        foreach(string key in data.Keys)
        {
            for(int i = 0; i < data[key].Count; i++)
            {
                if (data[key][i] < 0)
                    data[key][i] = 0;
            }
        }

        //TODO add data collection related to the robots

        //for testing purposes data values will be updated in the inspecter. Make sure to set the size of the array in the inspector to the right size.
        testdata[0] = data["rad"][0];
        testdata[1] = data["rad"][1];
        testdata[2] = data["temp"][0];
    }
}
