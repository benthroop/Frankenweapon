using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingRuleCiano : MonoBehaviour {
	public GameObject FirstPlace;
	public GameObject SecondPlace;
	public GameObject ThirdPlace;

	public List<GameObject> RacersList;

	public Sprite PlaceDisplayFirst;
	public Sprite PlaceDisplaySecond;
	public Sprite PlaceDisplayThird;
	public Sprite PlaceDisplayNone;

	public GameObject FirstPlaceSpawn;
	public GameObject SecondPlaceSpawn;
	public GameObject ThirdPlaceSpawn;

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
				RacerDetails.VictorySpawn = FirstPlaceSpawn;

				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplayFirst;
			} else if (RacerDetails.CarPoints >= SecondPlaceInt && RacerDetails.CarPoints < FirstPlaceInt) {
				ThirdPlace = SecondPlace;
				SecondPlace = Racer;
				SecondPlaceInt = RacerDetails.CarPoints;
				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplaySecond;
				RacerDetails.VictorySpawn = SecondPlaceSpawn;

			} else if (RacerDetails.CarPoints >= ThirdPlaceInt && RacerDetails.CarPoints < SecondPlaceInt && RacersList.Count >= 3) {
				ThirdPlace = Racer;
				ThirdPlaceInt = RacerDetails.CarPoints;
				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplayThird;
				RacerDetails.VictorySpawn = ThirdPlaceSpawn;

			} else {
				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplayNone;
				RacerDetails.VictorySpawn = null;
			}
		}
	}
}
