using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationDisplay : MonoBehaviour {
	public GameObject RotationCore;

	public int CurrentPlace;

	public int MaxLap;

	public float CarPoints;

	public GameObject PreviousCheckPoint;
	public float PointsOfPrevious;
	public GameObject NextCheckPoint;
	public float DistanceBetween;

	public float CheckPointPoints;

	public int LapNumberInt;
	public Text LapNumberText;

	public Image PlaceDisplayCore;

	public GameObject VictorySpawn;

	public GameObject MainCamera;

	void Start () {
		MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		DistanceBetween = Vector3.Distance (NextCheckPoint.transform.position, this.gameObject.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		VehicleCiano CarDetails = GetComponent<VehicleCiano> (); 
		RacingRuleCiano RacingDetails = CarDetails.WorldCore.GetComponent<RacingRuleCiano> ();

		if (LapNumberInt >= 5) {
			CarPoints = CarPoints + 40;
			transform.position = VictorySpawn.transform.position;
			if (CurrentPlace == 3) {
				RacingDetails.FirstPlaceSet = true;
			} else if (CurrentPlace == 2) {
				RacingDetails.SecondPlaceSet = true;
			} else if (CurrentPlace == 1) {
				RacingDetails.ThirdPlaceSet = true;
			}
		}

		CarPoints = (LapNumberInt * 100) + (CheckPointPoints);

		LapNumberText.text = LapNumberInt.ToString() + "/" + MaxLap.ToString();

		LookAtCamera ();
		DistanceCalculator();
	}

	void LookAtCamera() {
		RotationCore.transform.LookAt(MainCamera.transform);
	}

	void DistanceCalculator() {
		float DistancePercentage = ((DistanceBetween-(Vector3.Distance (NextCheckPoint.transform.position, this.gameObject.transform.position)))/DistanceBetween);
		CheckPointPoints = PointsOfPrevious + (10*DistancePercentage);
	}
}
