using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseStateHandler : MonoBehaviour
{
    //normal = 0, medium = 1, high = 2, top = 3
    int currentPriority = 0;
    List<int> topPriorityRobots = new List<int>();
    List<int> highPriorityRobots = new List<int>();
    List<int> medPriorityRobots = new List<int>();

    Coroutine medPriorityState;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void SetTopPriority(int gameObjectID,  bool setTop)
    {
        TopListHandler(gameObjectID, setTop);

        if ((topPriorityRobots.Count > 0) && (currentPriority < 3))
        {
            currentPriority = 3;
            Debug.Log("set top priority state");
        }
        if ((topPriorityRobots.Count == 0) && (currentPriority == 3))
        {
            currentPriority = 2;
            Debug.Log("set high priority state");
        }
    }
    public void SetHighPriority(int gameObjectID,  bool setHigh)
    {
        HighListHandler(gameObjectID, setHigh);

        if ((highPriorityRobots.Count > 0) && (currentPriority < 2))
        {
            currentPriority = 2;
            Debug.Log("set high priority state");
            AkSoundEngine.SetState("RadPriorities", "High");
        }
        if (highPriorityRobots.Count == 0)
        {
            currentPriority = 0;
            Debug.Log("set normal priority state");
            AkSoundEngine.SetState("RadPriorities", "Normal");
        }
        // Debug.Log($"High priority list: ");
        // for(int i=0;i<highPriorityRobots.Count;i++)
        // {
        //     Debug.Log(highPriorityRobots[i]);
        // }
    }
    public void SetMedPriority(int gameObjectID,  bool setMed)
    {
        MediumListHandler(gameObjectID, setMed);

        if ((medPriorityRobots.Count > 0) && (currentPriority == 0))
        {
            currentPriority = 1;
            Debug.Log("set medium priority state");
            AkSoundEngine.SetState("RadPriorities", "Med");
            //medPriorityState = StartCoroutine(medPriorityCoroutine());
        }
        if ((medPriorityRobots.Count == 0) && (currentPriority == 1))
        {
            currentPriority = 0;
            Debug.Log("set normal priority state");
            AkSoundEngine.SetState("RadPriorities", "Normal");
        }
    }

    //coroutine automating the switch to a medium priority state so that alerts can be heard clearly
    IEnumerator medPriorityCoroutine()
    {
        currentPriority = 1;
        Debug.Log("set medium priority state");
        AkSoundEngine.SetState("RadPriorities", "Med");
        yield return new WaitForSeconds(2);
        currentPriority = 0;
        Debug.Log("set normal priority state");
        AkSoundEngine.SetState("RadPriorities", "Normal");
    }


    //update lists when robots' priority statuses change
    void TopListHandler(int gameObjectID, bool setTop)
    {
        if (setTop && !(topPriorityRobots.Contains(gameObjectID)))
        {
            topPriorityRobots.Add(gameObjectID);
        }
        if (!setTop)
        {
            topPriorityRobots.RemoveAll(x => x == gameObjectID);
        }
    }
    void HighListHandler(int gameObjectID, bool setHigh)
    {
        if (setHigh && !(highPriorityRobots.Contains(gameObjectID)))
        {
            highPriorityRobots.Add(gameObjectID);
        }
        if (!setHigh)
        {
            highPriorityRobots.RemoveAll(x => x == gameObjectID);
        }
    }
    void MediumListHandler(int gameObjectID, bool setMed)
    {
        if (setMed && !(medPriorityRobots.Contains(gameObjectID)))
        {
            medPriorityRobots.Add(gameObjectID);
        }
        if (!setMed)
        {
            medPriorityRobots.RemoveAll(x => x == gameObjectID);
        }
    }    
}
