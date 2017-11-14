using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static int MinuteCount;
    public static int SecondCount;
    public static float MilliCount;
    public static string MilliDisplay;
    public static string SecondDisplay;

    public GameObject Timer;
    public GameObject BestTime;
    public GameObject CountDown;
    public GameObject Place;
    public GameObject WinText;
    public GameObject ResetButton;

    void Start()
    {
        WinText.SetActive(false);
        ResetButton.SetActive(false);
        StartCoroutine(Countdown(3));
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {

            CountDown.GetComponent<Text>().text = "" + count;
            yield return new WaitForSeconds(1);
            count--;
        }

        
        StartGame();
    }

    void StartGame()
    {
        CountDown.SetActive(false);
    }

    void Update () {
        MilliCount += Time.deltaTime * 10;
        MilliDisplay = MilliCount.ToString("F0");

        if (MilliCount >= 10)
        {
            MilliCount = 0;
            SecondCount += 1;
        }

        if (SecondCount >= 60)
        {
            SecondCount = 0;
            MinuteCount += 1;
        }

        if (SecondCount <= 9)
        {
            SecondDisplay = "0" + SecondCount;
        }
        else
        {
            SecondDisplay = "" + SecondCount;
        }

        Timer.GetComponent<Text>().text = "Lap Time: " + MinuteCount + ":" + SecondDisplay + "." + MilliDisplay;

	}
}
