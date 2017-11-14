using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RacingRuleCiano : MonoBehaviour {
	public bool RaceOnGoing = false;
    public bool RaceOver = false;
    public GameObject OverText;

	public GameObject InRaceCameraSpot;

	public GameObject FirstPlace;
	public GameObject SecondPlace;
	public GameObject ThirdPlace;

	public List<GameObject> RacersList;

	public Sprite PlaceDisplayFirst;
	public Sprite PlaceDisplaySecond;
	public Sprite PlaceDisplayThird;
	public Sprite PlaceDisplayNone;

	public GameObject RaceResultCameraSpot;

	public GameObject FirstPlaceSpawn;
	public bool FirstPlaceSet = false;
	public GameObject SecondPlaceSpawn;
	public bool SecondPlaceSet = false;
	public GameObject ThirdPlaceSpawn;
	public bool ThirdPlaceSet = false;

	void Start () {
		foreach (GameObject Racer in RacersList) {
			if (Racer.GetComponent<InformationDisplay> () == null) {
				Racer.AddComponent<InformationDisplay> ();
			}

            if (RacersList.Count == 1) {
                SecondPlaceSet = true;
                ThirdPlaceSet = true;
            } else if (RacersList.Count == 1) {
                ThirdPlaceSet = true;
            }
        }
	}

	// Update is called once per frame
	void Update () {
		PlaceCheck ();

		if (FirstPlaceSet == true && SecondPlaceSet == true && ThirdPlaceSet == true) {
			GameObject Camera = GameObject.FindGameObjectWithTag ("MainCamera");
			Camera.transform.position = RaceResultCameraSpot.transform.position;
		} else {
			GameObject Camera = GameObject.FindGameObjectWithTag ("MainCamera");
			Camera.transform.position = InRaceCameraSpot.transform.position;
            RaceOver = true;
		}

        if (RaceOver == true) {
            OverText.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

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
				RacerDetails.CurrentPlace = 3;
				RacerDetails.VictorySpawn = FirstPlaceSpawn;


				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplayFirst;
			} else if (RacerDetails.CarPoints >= SecondPlaceInt && RacerDetails.CarPoints < FirstPlaceInt) {
				ThirdPlace = SecondPlace;
				SecondPlace = Racer;
				SecondPlaceInt = RacerDetails.CarPoints;
				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplaySecond;
				RacerDetails.CurrentPlace = 2;
				RacerDetails.VictorySpawn = SecondPlaceSpawn;

			} else if (RacerDetails.CarPoints >= ThirdPlaceInt && RacerDetails.CarPoints < SecondPlaceInt && RacersList.Count >= 3) {
				ThirdPlace = Racer;
				ThirdPlaceInt = RacerDetails.CarPoints;
				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplayThird;
				RacerDetails.CurrentPlace = 1;
				RacerDetails.VictorySpawn = ThirdPlaceSpawn;

			} else {
				RacerDetails.PlaceDisplayCore.sprite = PlaceDisplayNone;
				RacerDetails.CurrentPlace = 0;
				RacerDetails.VictorySpawn = null;
			}
		}
	}
}
