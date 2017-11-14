using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScriptThomas : MonoBehaviour {

    public bool isCheckpoint1 = false;
    public bool isCheckpoint2 = false;
    public GameObject Waypoint1;
    public GameObject Waypoint2; 

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter (Collider other)
    {
       if (other.gameObject == RaceManagerThomas.instance.Racer1)
        {
            if (isCheckpoint1 == true)
            {
                Debug.Log("racer1");
                RaceManagerThomas.instance.CheckpointHitRacer1(); 
                isCheckpoint1 = false;
                Waypoint1.SetActive(false); 
            } 
        }

       if (other.gameObject == RaceManagerThomas.instance.Racer2)
        {
            if (isCheckpoint2 == true)
            {
                Debug.Log("racer2");
                RaceManagerThomas.instance.CheckpointHitRacer2(); 
                isCheckpoint2 = false;
                Waypoint2.SetActive(false);
            }
        }
    }

    public void Racer1Active ()
    {
        isCheckpoint1 = true;
        Waypoint1.SetActive(true); 
    }

    public void Racer2Active ()
    {
        isCheckpoint2 = true;
        Waypoint2.SetActive(true);  
    }
}
