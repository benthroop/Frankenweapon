using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //need to set this manually
    public int index;
    public bool incrementLap = false;

    private void OnTriggerEnter(Collider other)
    {
        //get the VehicleBase component off of the collider
        //if it is not null
            //RegisterCar(that car)
    }

    //what is the thing
    void RegisterCar(VehicleBase car)
    {
        if (incrementLap)
        {
            RaceManager.instance.IncrementLap(car);
        }
    }
}

