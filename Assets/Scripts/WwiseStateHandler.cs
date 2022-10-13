using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseStateHandler : MonoBehaviour
{
    //normal = 0, medium = 1, high = 2, top = 3
    int currentPriority = 0;
    List<int> highPriorityRobots = new List<int>();
    List<int> medPriorityRobots = new List<int>();

    Coroutine medTempPriorityState;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void setHighPriority(int gameObjectID,  bool setHigh)
    {
        if(setHigh && !(highPriorityRobots.Contains(gameObjectID)))
        {
            highPriorityRobots.Add(gameObjectID);
        }
        if(!setHigh)
        {
            highPriorityRobots.RemoveAll(x => x == gameObjectID);
        }
        if ((highPriorityRobots.Count > 0) && (currentPriority != 2))
        {
            currentPriority = 2;
            Debug.Log("set high priority state");
        }
        if (highPriorityRobots.Count == 0)
        {
            currentPriority = 0;
            Debug.Log("set normal priority state");
        }
        // Debug.Log($"High priority list: ");
        // for(int i=0;i<highPriorityRobots.Count;i++)
        // {
        //     Debug.Log(highPriorityRobots[i]);
        // }
    }
    public void setMedPriority(int gameObjectID,  bool setMed)
    {
        if(setMed && !(medPriorityRobots.Contains(gameObjectID)))
        {
            medPriorityRobots.Add(gameObjectID);
        }
        if(!setMed)
        {
            medPriorityRobots.RemoveAll(x => x == gameObjectID);
        }
        if ((medPriorityRobots.Count > 0) && (currentPriority == 0))
        {
            currentPriority = 1;
            Debug.Log("set medium priority state");
        }
        if ((medPriorityRobots.Count == 0) && (currentPriority < 2))
        {
            currentPriority = 0;
            Debug.Log("set normal priority state");
        }
        // Debug.Log($"High priority list: ");
        // for(int i=0;i<highPriorityRobots.Count;i++)
        // {
        //     Debug.Log(highPriorityRobots[i]);
        // }
    }


    IEnumerator medTempPrioritySequence()
    {
        AkSoundEngine.SetState("TempPriorities", "Med");
        yield return new WaitForSeconds(1);
        AkSoundEngine.SetState("TempPriorities", "Normal");
    }
}
