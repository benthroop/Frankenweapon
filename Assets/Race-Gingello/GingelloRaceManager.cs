using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GingelloRaceManager : MonoBehaviour {
    public List<GameObject> Players;
    public Text Standing;
    public GameObject p1w, p2w, p1, p2;

    [SerializeField] float p1Val, p2Val, p1Laps, p2Laps, p1CPHit, p2CPHit;
    float p1Pos, p2Pos, p1CPFin, p2CPFin;
    
    Vector3 P1Position, P2Position;

   

    private void Start()
    {
        p1 = Players[0].transform.gameObject;
        p2 = Players[1].transform.gameObject;
    }

    public void CheckLead()
    {
       p1Pos =  Players[0].transform.gameObject.GetComponent<VehicleGingello>().distToNextCP;
       p2Pos = Players[1].transform.gameObject.GetComponent<VehicleGingello>().distToNextCP;
       p1CPFin = Players[0].transform.gameObject.GetComponent<VehicleGingello>().CheckNum;
       p2CPFin = Players[1].transform.gameObject.GetComponent<VehicleGingello>().CheckNum;
       p1Laps = Players[0].transform.gameObject.GetComponent<VehicleGingello>().CheckLap;
       p2Laps = Players[1].transform.gameObject.GetComponent<VehicleGingello>().CheckLap;
        p1CPHit = Players[0].transform.gameObject.GetComponent<VehicleGingello>().CheckFin;
        p2CPHit = Players[1].transform.gameObject.GetComponent<VehicleGingello>().CheckFin;
        p1Val = p1Pos + p1CPHit;
       p2Val = p2Pos + p2CPHit;

    }

    private void Update()
    {
    

      
          
            P1Position = new Vector3(p1.gameObject.transform.localPosition.x, p1.gameObject.transform.localPosition.y + 2, p1.gameObject.transform.localPosition.z);
            P2Position = new Vector3(p2.gameObject.transform.localPosition.x, p2.gameObject.transform.localPosition.y + 2, p2.gameObject.transform.localPosition.z);
            CheckWin();
            CheckLead();
          
            if (p1Laps > p2Laps)
            {
                Standing.text = "Green is Leading";
                Debug.Log("Green is Leading");

            }
            if (p2Laps > p1Laps)
            {

                Standing.text = "Blue is Leading";
                Debug.Log("Blue is Leading");

            }
            if (p2Laps == p1Laps && p1CPHit > p2CPHit)
            {
                Standing.text = "Green is Leading";
                Debug.Log("Green is Leading");
            }
            if (p2Laps == p1Laps && p2CPHit > p1CPHit)
            {
                Standing.text = "Blue is Leading";
                Debug.Log("Blue is Leading");
            }
            if (p2Laps == p1Laps && p1CPHit == p2CPHit && p1Val < p2Val)
            {

                Standing.text = "Green is Leading";
                Debug.Log("Green is Leading");
            }

            if (p2Laps == p1Laps && p1CPHit == p2CPHit && p2Val < p1Val)
            {

                Standing.text = "Blue is Leading";
                Debug.Log("Blue is Leading");
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                p1.gameObject.transform.position = P1Position;
                p1.gameObject.transform.rotation = Quaternion.Euler(0, p1.gameObject.transform.rotation.eulerAngles.y, 0);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                p2.gameObject.transform.position = P2Position;
                p2.gameObject.transform.rotation = Quaternion.Euler(0, p2.gameObject.transform.rotation.eulerAngles.y, 0);
            }


        }
    

    void CheckWin()
    {if(p1Laps >= 3)
        {
           p1w.SetActive(true);
           p1.GetComponent<VehicleGingello>().enabled = !p1.GetComponent<VehicleGingello>().enabled;
           p2.GetComponent<VehicleGingello>().enabled = !p2.GetComponent<VehicleGingello>().enabled;
        }
    if (p2Laps >= 3)
        {
            p2w.SetActive(true);
            p1.GetComponent<VehicleGingello>().enabled = !p1.GetComponent<VehicleGingello>().enabled;
            p2.GetComponent<VehicleGingello>().enabled = !p2.GetComponent<VehicleGingello>().enabled;

        }

    }

   








}
