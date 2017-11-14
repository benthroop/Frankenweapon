using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    float timeLeft = 3;

    public Text Countdown;
    public GameObject[] Cars;

    public UnityEvent startrace; 

    // Use this for initialization
    void Start () {
        startrace.Invoke();

    }

    // Update is called once per frame
    void Update () {
       StartCoroutine( "RaceStart");

    }

    void Restart()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(1); 
        }
    }
    IEnumerator RaceStart()
    {
        timeLeft -= Time.deltaTime;
        Countdown.text = Mathf.Round(timeLeft).ToString();
        GameObject Car ;
        if (timeLeft < 0)
        {
            Countdown.text = "Go!";
            
            for (var i=0; i<Cars.Length; i++)
            {
                Car = Cars[i];
                Car.gameObject.GetComponent<PlayerVehicleController>().CanMove = true;

            }
            yield return new WaitForSecondsRealtime(1);
            Countdown.gameObject.SetActive(false);

        }
        yield break; 
    }
 
    

    
}
