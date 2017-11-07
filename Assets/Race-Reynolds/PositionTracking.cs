using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracking : MonoBehaviour {

    public GameObject player1, player2;
    public GameObject[] checkpoints = new GameObject[21];
    public int p1Index = 20, p2Index = 20, p1Lap = 0, p2Lap = 0;
    public GameObject UI;
    public GameObject p1tracker, p2tracker;
    public Vector3 p1trackerpos, p2trackerpos;
    // Use this for initialization
    void Start () {
        p1trackerpos = p1tracker.transform.localPosition;
        p2trackerpos = p2tracker.transform.localPosition;
        for (int i = 0; i <= 20; i++)
        {
            checkpoints[i] = GameObject.Find("Checkpoint (" + i + ")");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePosition(int playerNum, int checkpointIndex)
    {
        switch(playerNum)
        {
            case 1:
                if (p1Index == 20 && checkpointIndex == 0)
                {
                    p1Index = 0;
                    p1tracker.transform.parent = checkpoints[0].transform;
                    p1tracker.transform.localPosition = p1trackerpos;
                    p1Lap++;
                    UI.GetComponent<UIScript>().UpdateLap(1, p1Lap);
                    if (p1Lap>p2Lap)
                    {
                        UI.GetComponent<UIScript>().UpdatePosition(1, 1);
                        UI.GetComponent<UIScript>().UpdatePosition(2, 2);
                    }

                }
                else if (p1Index == checkpointIndex-1)
                {
                    p1Index++;
                    p1tracker.transform.parent = checkpoints[p1Index+1].transform;
                    p1tracker.transform.localPosition = p1trackerpos;
                    if ((p1Lap > p2Lap) || (p1Lap == p2Lap && p1Index>p2Index))
                    {
                        UI.GetComponent<UIScript>().UpdatePosition(1, 1);
                        UI.GetComponent<UIScript>().UpdatePosition(2, 2);
                    }
                }
                    
                break;
            case 2:
                if (p2Index == 20 && checkpointIndex == 0)
                {
                    p2Index = 0;
                    p2tracker.transform.parent = checkpoints[0].transform;
                    p2tracker.transform.localPosition = p2trackerpos;
                    p2Lap++;
                    UI.GetComponent<UIScript>().UpdateLap(2, p2Lap);
                    if (p2Lap > p1Lap)
                    {
                        UI.GetComponent<UIScript>().UpdatePosition(2, 1);
                        UI.GetComponent<UIScript>().UpdatePosition(1, 2);
                       
                    }
                }
                else if (p2Index == checkpointIndex - 1)
                {
                    p2Index++;
                    p2tracker.transform.parent = checkpoints[p2Index+1].transform;
                    p2tracker.transform.localPosition = p2trackerpos;
                    if ((p2Lap > p1Lap) || (p2Lap == p1Lap && p2Index > p1Index))
                    {
                        UI.GetComponent<UIScript>().UpdatePosition(2, 1);
                        UI.GetComponent<UIScript>().UpdatePosition(1, 2);
                    }
                }
                break;
            default:
                break;

        }
    }
}
