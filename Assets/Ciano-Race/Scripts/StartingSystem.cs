using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSystem : MonoBehaviour {
	public bool RaceStarted = false;
    public bool PrepStarted = false;
    public GameObject StartButton;

	public float TimeCount;
	public GameObject VisualCore;
	public GameObject Visual1;
	public GameObject Visual2;
	public GameObject Visual3;
	public GameObject GoVisual;

    // Update is called once per frame
    void Update()
    {
        RacingRuleCiano RuleInfo = this.gameObject.GetComponent<RacingRuleCiano>();

        if (RaceStarted == true)
        {
            RuleInfo.RaceOnGoing = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && PrepStarted == false) {
            Destroy(RuleInfo.RacersList[3]);
            RuleInfo.RacersList.Remove(RuleInfo.RacersList[3]);
            RuleInfo.ThirdPlaceSet = true;
            Destroy(RuleInfo.RacersList[2]);
            RuleInfo.RacersList.Remove(RuleInfo.RacersList[2]);
            //RuleInfo.SecondPlaceSet = true;
            StartButton.SetActive(false);
            PrepStarted = true;
        } else if (Input.GetKeyDown(KeyCode.Alpha3) && PrepStarted == false) {
            Destroy(RuleInfo.RacersList[3]);
            RuleInfo.RacersList.Remove(RuleInfo.RacersList[3]);
            //RuleInfo.ThirdPlaceSet = true;
            StartButton.SetActive(false);
            PrepStarted = true;
        } else if (Input.GetKeyDown(KeyCode.Alpha4) && PrepStarted == false) {
            StartButton.SetActive(false);
            PrepStarted = true;
        }

        if (PrepStarted == true)
        {
            if (TimeCount >= 3.5)
            {
                RaceStarted = true;
            }
            else
            {
                TimeCount = TimeCount + Time.deltaTime;
            }

            if (TimeCount >= 1)
            {
                Visual1.SetActive(true);
            }

            if (TimeCount >= 2)
            {
                Visual2.SetActive(true);
            }

            if (TimeCount >= 3)
            {
                Visual3.SetActive(true);
            }

            if (TimeCount >= 3.25f)
            {
                GoVisual.SetActive(true);
            }

            if (TimeCount >= 3.5f)
            {
                VisualCore.SetActive(false);
            }
        }
    }
}
