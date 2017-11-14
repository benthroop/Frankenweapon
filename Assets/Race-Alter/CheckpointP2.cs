using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointP2 : MonoBehaviour
{
    void Update()
    {
        CarReset();
    }
    void CarReset()
    {
        if(Input.GetKeyUp(KeyCode.RightAlt))
        {
            transform.rotation = new Quaternion(0,0, 0, 0);
            gameObject.transform.position = Laps.Player2checkpointA[Laps.Player2currentCheckpoint-1].transform.position;
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.transform == Laps.Player2checkpointA[Laps.Player2currentCheckpoint].transform)
        {




            //Check so we dont exceed our checkpoint quantity
            if (Laps.Player2currentCheckpoint + 1 < Laps.Player2checkpointA.Length)
            {
                //Add to currentLap if currentCheckpoint is 0
                if (Laps.Player2currentCheckpoint == 0)
                {
                    Laps.Player2currentLap++;
                    Laps.Player2currentCheckpoint++;
                }
                Laps.Player2currentCheckpoint++;

            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                Laps.Player2currentCheckpoint = 0;
            }

        }
    }

}
