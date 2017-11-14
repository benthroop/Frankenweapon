using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class RaceManagerThomas : MonoBehaviour
{
    public GameObject Racer1;
    public GameObject Racer2;
    public GameObject[] Racer1Checkpoints;
    public GameObject[] Racer2Checkpoints;
    GameObject NextCheckpoint1;
    GameObject NextCheckpoint2; 
    int Racer1Laps = 1;
    int Racer2Laps = 1;
    int Racer1index = 0;
    int Racer2index = 0;
    Vector3 Racer1Position;
    Vector3 Racer2Position;
    public GameObject player1Text;
    public GameObject Player2Text;
    public GameObject Player1Wins;
    public GameObject Player2Wins; 
    public Text Laps1Text;
    public Text Laps2Text; 


    public static RaceManagerThomas instance; 

    // Use this for initialization
    void Start ()
    {
        instance = this;
        NextCheckpoint1 = Racer1Checkpoints[Racer1index];
        NextCheckpoint1.gameObject.GetComponent<CheckpointScriptThomas>().Racer1Active();
        NextCheckpoint2 = Racer2Checkpoints[Racer2index];
        NextCheckpoint2.gameObject.GetComponent<CheckpointScriptThomas>().Racer2Active();
        Laps1Text.text = ("Beezle Lap" + Racer1Laps);
        Laps2Text.text = ("Baelzebub Lap" + Racer2Laps);

    }
	
	// Update is called once per frame
	void Update ()
    {
        Racer1Position = NextCheckpoint1.transform.position - Racer1.transform.position;
        Racer2Position = NextCheckpoint2.transform.position - Racer2.transform.position;

       
            if (Racer1index == Racer2index && Racer1Laps == Racer2Laps)
            {
                if (Racer1Position.magnitude < Racer2Position.magnitude)
                {
                    Debug.Log("Beezle in lead");
                    player1Text.SetActive(true);
                    Player2Text.SetActive(false);
                }

                if (Racer1Position.magnitude > Racer2Position.magnitude)
                {
                    Debug.Log("Baelzebub in lead");
                    Player2Text.SetActive(true);
                    player1Text.SetActive(false);
                }
            }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); 
        }
       

        if (Racer1Laps == Racer2Laps)
        {
            if (Racer1index > Racer2index)
            {
                Debug.Log("Beezle in lead");
                player1Text.SetActive(true);
                Player2Text.SetActive(false);
            }

            if (Racer2index > Racer1index)
            {
                Debug.Log("Baelzebub in lead");
                Player2Text.SetActive(true);
                player1Text.SetActive(false);
            }
        }

        if (Racer1Laps > Racer2Laps)
        {
            Debug.Log("Beezle in lead");
            player1Text.SetActive(true);
            Player2Text.SetActive(false);
        }

        if (Racer2Laps > Racer1Laps)
        {
            Debug.Log("Baelzebub in lead");
            Player2Text.SetActive(true);
            player1Text.SetActive(false);
        }
	}

    public void CheckpointHitRacer1 ()
    {
        Racer1index++;
        CheckLapRacer1();
        NextCheckpoint1 = Racer1Checkpoints[Racer1index];
        NextCheckpoint1.gameObject.GetComponent<CheckpointScriptThomas>().Racer1Active(); 
    }

    public void CheckpointHitRacer2 ()
    {
        Racer2index++;
        CheckLapRacer2(); 
        NextCheckpoint2 = Racer2Checkpoints[Racer2index];
        NextCheckpoint2.gameObject.GetComponent<CheckpointScriptThomas>().Racer2Active(); 
    }

    void CheckLapRacer1()
    {
        if (Racer1index == Racer1Checkpoints.Length)
        {
            Racer1index = 0;
            Racer1Laps++;
            Laps1Text.text = ("Beezle Lap" + Racer1Laps);
            Win();
            Debug.Log("lap racer 1");
        }
    }

    void CheckLapRacer2()
    {
        if (Racer2index == Racer2Checkpoints.Length)
        {
            Racer2index = 0;
            Racer2Laps++;
            Laps2Text.text = ("Baelzebub Lap" + Racer2Laps);
            Win(); 
            Debug.Log("lap Racer 2");
        }
    }

    public void Reset1 ()
    {
        Racer1index = 0;
        NextCheckpoint1 = Racer1Checkpoints[Racer1index];
        NextCheckpoint1.gameObject.GetComponent<CheckpointScriptThomas>().Racer1Active();

    }

    public void Reset2()
    {
        Racer2index = 0;
        NextCheckpoint2 = Racer2Checkpoints[Racer2index];
        NextCheckpoint2.gameObject.GetComponent<CheckpointScriptThomas>().Racer2Active();
    }

    void Win ()
    {
        if(Racer1Laps == 2)
        {
            Player1Wins.SetActive(true);
            Laps1Text.text = ("RACE OVER");
            Laps2Text.text = ("RACE OVER");
            Destroy(player1Text);
            Destroy(Player2Text);

        }

        if(Racer2Laps == 4)
        {
            Player2Wins.SetActive(true);
            Laps1Text.text = ("RACE OVER");
            Laps2Text.text = ("RACE OVER");
            Destroy(player1Text);
            Destroy(Player2Text);
        }
    }


}
