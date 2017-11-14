using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Car : MonoBehaviour {

   public GameObject Menu;
   public Text winText;
   public Text Lap;
   public Text pos;
   public int currentWaypoint;
   public int currentLap;
   public Transform lastWaypoint;
   public int nbWaypoint; //Set the amount of Waypoints

   private static int WAYPOINT_VALUE = 100;
   private static int LAP_VALUE = 10000;
   private int cpt_waypoint = 0;

   // Use this for initialization
   public void Initialize()
   {
      currentWaypoint = 0;
      currentLap = 0;
      cpt_waypoint = 0;
   }

   public void OnTriggerEnter(Collider other)
   {
      string otherTag = other.gameObject.tag;
      currentWaypoint = System.Convert.ToInt32(otherTag);
      if (currentWaypoint == 1 && cpt_waypoint == nbWaypoint)
      { // completed a lap, so increase currentLap;
         currentLap++;
         cpt_waypoint = 0;
      }
      if (cpt_waypoint == currentWaypoint - 1)
      {
         lastWaypoint = other.transform;
         cpt_waypoint++;
      }
   }

   public float GetDistance()
   {
      return (transform.position - lastWaypoint.position).magnitude + currentWaypoint * WAYPOINT_VALUE + currentLap * LAP_VALUE;
   }

   public int GetCarPosition(Car[] allCars)
   {
      float distance = GetDistance();
      int position = 1;
      foreach (Car car in allCars)
      {
         if (car.GetDistance() > distance)
            position++;
      }
      pos.text = position.ToString();
      return position;
   }

void Start()
{

      Menu.SetActive(false);
   }

void Update () {
      

      Lap.text = (currentLap + 1).ToString();
if(currentLap == 4)
{

         winText.text = gameObject.name.ToString() + " Wins!";
         Menu.SetActive(true);


}
   }
}
