using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    public GameObject p1Lap;
    public GameObject p1Position;
    public GameObject p2Lap;
    public GameObject p2Position;
    public GameObject p1win, p2win;
    public Camera cam, wincam1, wincam2;
    public GameObject[] startsequence = new GameObject[4];
    int timer = 0;
    void Start()
    {
        wincam1.enabled = false;
        wincam2.enabled = false;
        cam.enabled = true;
    }
    private void Update()
    {
        timer++;
        if (timer == 40)
        {
            startsequence[0].SetActive(true);
        }
        else if (timer == 80)
        {
            startsequence[0].SetActive(false);
            startsequence[1].SetActive(true);
        }
        else if (timer == 120)
        {
            startsequence[1].SetActive(false);
            startsequence[2].SetActive(true);
        }
        else if (timer == 160)
        {
            startsequence[2].SetActive(false);
            startsequence[3].SetActive(true);
        }
        else if (timer == 200)
        {
            startsequence[3].SetActive(false);
        }
    }
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

        if (lapNum == 4)
        {
            switch (playerNum)
            {
                case 1:
                    wincam1.enabled = true;
                    cam.enabled = false;
                    p1win.SetActive(true);
                    break;
                case 2:
                    wincam2.enabled = true;
                    cam.enabled = false;
                    p2win.SetActive(true);
                    break;
                default:
                    break;
            }
        }
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
