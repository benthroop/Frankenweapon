using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalScript : MonoBehaviour {

	public GameObject TeleportSpot;
    public UnityEvent ExitPortal; 
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.GetComponent<PlayerVehicleController> () != null) {
			col.gameObject.transform.rotation = TeleportSpot.transform.rotation;
			col.gameObject.transform.position = TeleportSpot.transform.position;

		}

		if (col.gameObject.GetComponent<InformationDisplay> () != null) {
            ExitPortal.Invoke();

			InformationDisplay DisplayInfo = col.gameObject.GetComponent<InformationDisplay> ();

			DisplayInfo.LapNumberInt++;
		}
	}
}
