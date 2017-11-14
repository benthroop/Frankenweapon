using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPoint : MonoBehaviour {
    public GameObject reset1;
    public GameObject reset2; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == RaceManagerThomas.instance.Racer1)
        {
            other.gameObject.transform.position = reset1.transform.position;
            other.gameObject.transform.rotation = reset1.transform.rotation; 
        }
    }
}
