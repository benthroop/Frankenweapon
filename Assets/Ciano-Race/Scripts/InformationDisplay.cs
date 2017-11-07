using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationDisplay : MonoBehaviour {
	public GameObject RotationCore;

	public float CarPoints;

	public GameObject PreviousCheckPoint;
	public float PointsOfPrevious;
	public GameObject NextCheckPoint;
	public float DistanceBetween;

	public float CheckPointPoints;

	public int LapNumberInt;
	public Text LapNumberText;

	public Image PlaceDisplayCore;

	public GameObject MainCamera;

	void Start () {
		MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		CarPoints = (LapNumberInt * 100) + (CheckPointPoints);

		LapNumberText.text = LapNumberInt.ToString();

		LookAtCamera ();
		DistanceCalculator();
	}

	void LookAtCamera() {
		RotationCore.transform.LookAt(MainCamera.transform);
	}

	void DistanceCalculator() {
		float DistancePercentage = (Vector3.Distance (NextCheckPoint.transform.position, this.gameObject.transform.position)/DistanceBetween);
		CheckPointPoints = PointsOfPrevious + (10*DistancePercentage);
	}
}
