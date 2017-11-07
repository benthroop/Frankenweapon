using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationDisplay : MonoBehaviour {
	public GameObject RotationCore;

	public float CarPoints;

	public GameObject PreviousCheckPoint;
	public GameObject NextCheckPoint;

	public float CheckPointPoints;

	public int LapNumberInt;
	public Text LapNumberText;
	public Sprite PlaceDisplayFirst;
	public Sprite PlaceDisplaySecond;
	public Sprite PlaceDisplayThird;

	public GameObject MainCamera;

	void Start () {
		MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		CarPoints = (LapNumberInt * 100) + (CheckPointPoints);

		LapNumberText.text = LapNumberInt.ToString();

		LookAtCamera ();

	}

	void LookAtCamera() {
		RotationCore.transform.LookAt(MainCamera.transform);
	}

}
