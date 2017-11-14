using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour

{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object Entered Trigger.");
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Object is within trigger.");
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Object has exited trigger.");
    }
}