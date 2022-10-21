using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseStateHandler : MonoBehaviour
{
    //priorities represented as: normal = 0, medium = 1, high = 2, top = 3
    int currentPriority = 0;
    List<int> topPriorityRobots = new List<int>();
    List<int> highPriorityRobots = new List<int>();
    List<int> medPriorityRobots = new List<int>();

    public void SetTopPriority(int gameObjectID,  bool setTop)
    {
        TopListHandler(gameObjectID, setTop);

        if ((topPriorityRobots.Count > 0) && (currentPriority < 3))
        {
            currentPriority = 3;
            AkSoundEngine.SetState("Priorities", "Top");
        }
        if ((topPriorityRobots.Count == 0) && (currentPriority == 3))
        {
            currentPriority = 2;
            AkSoundEngine.SetState("Priorities", "High");
        }
    }
    public void SetHighPriority(int gameObjectID,  bool setHigh)
    {
        HighListHandler(gameObjectID, setHigh);

        if ((highPriorityRobots.Count > 0) && (currentPriority < 2))
        {
            currentPriority = 2;
            AkSoundEngine.SetState("Priorities", "High");
        }
        if (highPriorityRobots.Count == 0)
        {
            currentPriority = 0;
            AkSoundEngine.SetState("Priorities", "Normal");
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
            AkSoundEngine.SetState("Priorities", "Medium");
        }
        if ((medPriorityRobots.Count == 0) && (currentPriority == 1))
        {
            currentPriority = 0;
            AkSoundEngine.SetState("Priorities", "Normal");
        }
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
