using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour {

    public GameObject LapCompleteTrigger;
    public GameObject HalfwayTrigger;

    public static string Minute;
    public static string Second;
    public static string Milli;

    public float lap;
    public float totalLaps;

    public GameObject BestTime;
    public GameObject Lap;
    public GameObject Place;

    public GameObject WinText;
    public GameObject ResetButton;

    public int p1;
    public int p2;


    void OnTriggerEnter (Collider collider)
    {
        lap++;
        if (UIManager.SecondCount <= 9)
        {
            Second = "0" + UIManager.SecondCount;
        }
        else
        {
            Second = "" + UIManager.SecondCount;
        }

        Milli = "" + UIManager.MilliDisplay;
        Minute = "" + UIManager.MinuteCount;

        BestTime.GetComponent<Text>().text = "BestTime" + Minute + ":" + Second + "." + Milli;

        Lap.GetComponent<Text>().text = "Lap: " + lap + "/" + totalLaps;

        UIManager.MinuteCount = 0;
        UIManager.SecondCount = 0;
        UIManager.MilliCount = 0;

        HalfwayTrigger.SetActive(true);
        LapCompleteTrigger.SetActive(false);

        if (collider.tag == "Player1")
        {
            p1++;
        }
        if (collider.tag == "Player2")
        {
            p2++;
        }
        
        if (p1 > p2)
        {
            Place.GetComponent<Text>().text = "Player 1 is winning";
        }
        else
        {
            Place.GetComponent<Text>().text = "Player 2 is winning";
        }

        if (lap > totalLaps)
        {
            initWin();
        }
    }

    public void initWin()
    {
        WinText.SetActive(true);
        ResetButton.SetActive(true);
        if (p1 > p2)
        {
            WinText.GetComponent<Text>().text = "Player 1 is Winner";
        }
        else
        {
            WinText.GetComponent<Text>().text = "Player 2 is the Winner";
        }
    }

}
