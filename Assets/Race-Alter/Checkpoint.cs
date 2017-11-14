using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {


    void Update()
    {
        CarReset();
    }
    void CarReset()
    {
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            transform.rotation = new Quaternion(0,0, 0, 0);
            gameObject.transform.position = Laps.Player1checkpointA[Laps.Player1currentCheckpoint-1].transform.position;
        }
    }


    void OnTriggerEnter(Collider other)
    {
 
            if (other.transform == Laps.Player1checkpointA[Laps.Player1currentCheckpoint].transform)
            {

               


                    //Check so we dont exceed our checkpoint quantity
                    if (Laps.Player1currentCheckpoint + 1 < Laps.Player1checkpointA.Length)
                    {
                    //Add to currentLap if currentCheckpoint is 0
                        if (Laps.Player1currentCheckpoint == 0)
                        {
                            Laps.Player1currentLap++;
                            Laps.Player1currentCheckpoint++;
                        }
                Laps.Player1currentCheckpoint++;
                    }
                    else
                    {
                        //If we dont have any Checkpoints left, go back to 0
                        Laps.Player1currentCheckpoint = 0;
                    }
                
            }

    }
}
