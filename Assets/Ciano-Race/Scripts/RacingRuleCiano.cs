using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingRuleCiano : MonoBehaviour {

	public int CurrentLap;
	public GameObject FirstPlace;
	public GameObject SecondPlace;
	public GameObject ThirdPlace;

	public List<GameObject> RacersList;

	public Sprite PlaceDisplayFirst;
	public Sprite PlaceDisplaySecond;
	public Sprite PlaceDisplayThird;
	public Sprite PlaceDisplayNone;

	void Start () {
		foreach (GameObject Racer in RacersList) {
			if (Racer.GetComponent<InformationDisplay> () == null) {
				Racer.AddComponent<InformationDisplay> ();
			}
		}
	}

	// Update is called once per frame
	void Update () {
		PlaceCheck ();
	}

	void PlaceCheck() {
		float FirstPlaceInt = 0;
		float SecondPlaceInt = 0;
		float ThirdPlaceInt = 0;

		foreach (GameObject Racer in RacersList) {
			InformationDisplay RacerDetails = Racer.GetComponent<InformationDisplay> ();
			if (RacerDetails.CarPoints >= FirstPlaceInt) {
				ThirdPlace = SecondPlace;
				SecondPlace = FirstPlace;
				FirstPlace = Racer;
				FirstPlaceInt = RacerDetails.CarPoints;
				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplayFirst;
			} else if (RacerDetails.CarPoints >= SecondPlaceInt && RacerDetails.CarPoints < FirstPlaceInt) {
				ThirdPlace = SecondPlace;
				SecondPlace = Racer;
				SecondPlaceInt = RacerDetails.CarPoints;
				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplaySecond;
			} else if (RacerDetails.CarPoints >= ThirdPlaceInt && RacerDetails.CarPoints < SecondPlaceInt) {
				ThirdPlace = Racer;
				ThirdPlaceInt = RacerDetails.CarPoints;
				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplayThird;
			} else {
				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplayNone;
			}
		}
	}
}
