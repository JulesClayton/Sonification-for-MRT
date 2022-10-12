using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseStateHandler : MonoBehaviour
{
    WwisePriorities[] wp;

    Coroutine medTempPriorityState;
    // Start is called before the first frame update
    void Start()
    {
        wp = GetComponentsInChildren<WwisePriorities>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((wp.isTempMedPriority) && !(wp.highPriority))
        {
            Debug.Log($"robot says medium priority is {wp.isTempMedPriority}");
            //medTempPriorityState = StartCoroutine(medTempPrioritySequence());
        }
    }
    IEnumerator medTempPrioritySequence()
    {
        AkSoundEngine.SetState("TempPriorities", "Med");
        yield return new WaitForSeconds(1);
        AkSoundEngine.SetState("TempPriorities", "Normal");
    }
}
