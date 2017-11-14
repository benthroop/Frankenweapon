using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Laps : MonoBehaviour {
    // These Static Variables are accessed in "checkpoint" Script
    public Transform[] checkPointArray;
    public Transform[] Player2checkPointArray;

    public static Transform[] Player1checkpointA;
    public static Transform[] Player2checkpointA;

    public static int Player1currentCheckpoint = 0;
    public static int Player2currentCheckpoint = 0;

    public static  int Player1currentLap = 0;
    public static int Player2currentLap = 0;

    public int PlayerOnePos;
    public int PlayerTwoPos;

    public Vector3 startPos;
    public int Player1Lap;
    public int Player2Lap;

   public Text PlayerOneLap;
   public Text PlayerTwoLap;
   public Text PlayerOnePosui;
   public Text PlayerTwoPosui;
   public Text WinnerText;

    public UnityEvent WinSound;


    void Start()
    {
        startPos = transform.position;
        Player1currentCheckpoint = 0;
        Player1currentLap = 0;
        Player2currentCheckpoint = 0;
        Player2currentLap = 0;

    }

    void Update()
    {

         if(Player1Lap>=4&& PlayerOnePos ==1)
        {
            GameObject.Find("Panel").GetComponent<Image>().enabled = true;
            WinnerText.text = "Player One Wins!";
             WinSound.Invoke();
        }
            
         else if (Player2Lap>=4&& PlayerTwoPos ==1)
        {
            GameObject.Find("Panel").GetComponent<Image>().enabled = true;
            WinnerText.text = "Player Two Wins!";
            WinSound.Invoke();


        }



        Player1Lap = Player1currentLap;
        Player1checkpointA = checkPointArray;

        Player2Lap = Player2currentLap;
        Player2checkpointA = Player2checkPointArray;


        Debug.Log(Player2currentCheckpoint);
        Debug.Log(Player1currentCheckpoint);

        if(Player2currentCheckpoint<Player1currentCheckpoint)
        {
            PlayerOnePos = 1;
            PlayerTwoPos = 2;

        }
        else if(Player1currentCheckpoint<Player2currentCheckpoint)
        {
            PlayerOnePos = 2;
            PlayerTwoPos = 1;
        }


        PlayerOneLap.text = "P1 Lap: " + Player1currentLap.ToString();
        PlayerTwoLap.text = "P2 Lap: " + Player2currentLap.ToString();
        PlayerOnePosui.text = "P1 Pos: " + PlayerOnePos.ToString();
        PlayerTwoPosui.text = "P2 Pos: " + PlayerTwoPos.ToString();

    }


}
