using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;

public class DialogueStartScript : MonoBehaviour
{

    bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            triggered = true;
        }
    }

    void OnGui()
    {
        if (triggered) //The dialogue starts.
        {
            GUI.Label(new Rect(300, 300, 500, 200), "Hello World!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //The dialogue ends.
        }
    }
}
