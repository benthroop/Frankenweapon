using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    //Handle on Cars - when we need to query cars or manipulate cars, these are the cars to talk to
    [SerializeField] List<VehicleBase> carList;
    [SerializeField] List<int> carLapCountList;
    [SerializeField] List<Transform> startingPoints;

    public int totalLapsInRace = 3;

    //This is a singleton and can be talked to from anywhere via RaceManager.instance
    public static RaceManager instance;
    private void Awake()
    {
        instance = this;
    }

    /* How do we know the standings
     * 
     * Get all the cars
     * For each car, get the last checkpoint hit
     * Sort the cars by checkpoint
     * If more than one car is on the same checkpoint, measure the distance to the next checkpoint
     * and sort by that.
     */

    /*  Starting - position cars on the starting line, count down, enable control */
    void StartSequence()
    {
        //put cars in the right place
        //disable controls (somehow)

        //trigger a countdown

        //enable controls
        //play some kind of start sequence, audio, UI, etc

        
    }

    public void IncrementLap(VehicleBase car)
    {
        
    }
     
     /*  Win Condition - who completes the target number of laps first
     *  Win Sequence - once the win condition is met, make the winner feel nice
     *  
     *  UI
     *  Standing/Who's Winning - Lap counter per player 
     *  Action Icon - What powerup do I have
     *  Boost Meter - on vehicle
     */



}
