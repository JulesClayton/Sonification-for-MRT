using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseStateHandler : MonoBehaviour
{
    WwisePriorities[] wp;

    List<int> highPriorityRobots = new List<int>();

    Coroutine medTempPriorityState;
    // Start is called before the first frame update
    void Start()
    {
        wp = GetComponentsInChildren<WwisePriorities>();
    }

    // Update is called once per frame
    void Update()
    {
        // if ((wp.isTempMedPriority) && !(wp.highPriority))
        // {
        //     Debug.Log($"robot says medium priority is {wp.isTempMedPriority}");
        //     //medTempPriorityState = StartCoroutine(medTempPrioritySequence());
        // }
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
        if (highPriorityRobots.Count > 0)
        {
            Debug.Log("set high priority state");
        }
        if (highPriorityRobots.Count == 0)
        {
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
