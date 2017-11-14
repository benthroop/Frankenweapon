using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncLapScript : MonoBehaviour {

    public GameObject LapCompleteTrigger;
    public GameObject HalfwayTrigger;

    void OnTriggerEnter ()
    {
        LapCompleteTrigger.SetActive(true);
        HalfwayTrigger.SetActive(false);
    }
}
