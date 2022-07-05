using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class data
{
    public List<int> vs = new List<int>();
    public List<int> us = new List<int>();

    public data()
    {
        vs.Add(1);
        vs.Add(2);
        us.Add(4);
        us.Add(5);
    }
}

public class RHSerDict : MonoBehaviour
{
    Dictionary<string, string> ssd = new Dictionary<string, string> { { "A", "B" } };
    Dictionary<string, string> ssd2 = new Dictionary<string, string> { { "C", "D" } };
    
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, Dictionary<string, string>> complxdict = new Dictionary<string, Dictionary<string, string>>() { { "1st", ssd }, { "2nd", ssd2 } };
        /*var jsonssd = JsonConvert.SerializeObject(ssd);
        print(ssd2["C"]);
        ssd2 = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonssd);
        print(ssd2["A"]);*/

        using (StreamWriter file = File.CreateText(@"G:\Unity_ws\dictserialisationfiles\test2.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, complxdict);//TODO this works as I expect, need to test deserialisation from the file
        }

        using (StreamReader file = File.OpenText(@"G:\Unity_ws\dictserialisationfiles\test2.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            Dictionary<string, Dictionary<string, string>> complxdict2 = (Dictionary<string, Dictionary<string, string>>)serializer.Deserialize(file, typeof(Dictionary<string, Dictionary<string, string>>));
            print(complxdict2["1st"]["A"]);
        }

        data d = new data();        
        Dictionary<string, data> myclassdict = new Dictionary<string, data>() { { "A", d } };
        //print(myclassdict["A"].vs[0]);
        //print(JsonConvert.SerializeObject(myclassdict));
        
        using (StreamWriter file = File.CreateText(@"G:\Unity_ws\dictserialisationfiles\test3.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, myclassdict);//TODO this works as I expect, need to test deserialisation from the file
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
