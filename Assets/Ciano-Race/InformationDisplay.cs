using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationDisplay : MonoBehaviour {
	public GameObject RotationCore;
	public int LapNumberInt;
	public Text LapNumberText;
	public Sprite PlaceDisplayFirst;
	public Sprite PlaceDisplaySecond;

	public GameObject MainCamera;

	void Start () {
		MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		
		LapNumberText.text = LapNumberInt.ToString();

		LookAtCamera ();

	}

	void LookAtCamera() {
		RotationCore.transform.LookAt(MainCamera.transform);
	}

}
