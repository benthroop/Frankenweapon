using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour {

	public float PointValue;
	public GameObject Self;
	public GameObject Next;

	public float DistanceBetween;

	// Update is called once per frame
	void Start () {
		Self = this.gameObject;
		DistanceBetween = Vector3.Distance (Next.transform.position, this.gameObject.transform.position);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.GetComponent<InformationDisplay>() != null) {
			InformationDisplay CarDetails = col.gameObject.GetComponent<InformationDisplay> ();
			CarDetails.PreviousCheckPoint = this.gameObject;
			CarDetails.NextCheckPoint = Next;
			CarDetails.PointsOfPrevious = PointValue;
			CarDetails.DistanceBetween = DistanceBetween;

			Debug.Log ("Check");
		}
	}
}
