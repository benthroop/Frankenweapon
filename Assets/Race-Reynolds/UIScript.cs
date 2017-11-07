using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    public GameObject p1Lap;
    public GameObject p1Position;
    public GameObject p2Lap;
    public GameObject p2Position;

    public void UpdatePosition(int playerNum, int positionNum)
    {
        switch (playerNum)
        {
            case 1:
                p1Position.GetComponent<Text>().text = "Position: " + positionNum + "/2";
                break;
            case 2:
                p2Position.GetComponent<Text>().text = "Position: " + positionNum + "/2";
                break;
            default:
                break;
        }
    }
    public void UpdateLap(int playerNum, int lapNum)
    {
        switch (playerNum)
        {
            case 1:
                p1Lap.GetComponent<Text>().text = "Lap: " + lapNum + "/3";
                break;
            case 2:
                p2Lap.GetComponent<Text>().text = "Lap: " + lapNum + "/3";
                break;
            default:
                break;
        }
    }
}
