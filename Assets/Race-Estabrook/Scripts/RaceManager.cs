using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour {
    [System.Serializable]
    public class playerVehicles
    {   
        public GameObject vehicle;
        public GameObject UI;
        public List<GameObject> laps;
        public int place;
    }
    

    [SerializeField]
    List<playerVehicles> players;
    [SerializeField]
    List<GameObject> mapPoints;

    int goal = 3;
    int count = 0;
    bool hasChecked = false;

    public bool isWon= false;

    public GameObject winScreen;


    public GameObject num3, num2, num1;

    GameObject[] nums = new GameObject[3];

    float countdown = 3f;
    public bool isCountdown = true;

    void Start()
    {
        nums[0] = num3;
        nums[1] = num2;
        nums[2] = num1;
    }
    public static RaceManager instance;

    private void Awake()
    {
        
        instance = this;
        
    }
    
    void Update()
    {
        StartCountdown();

        creatPlacings();
        for(int i = 0; i < players.Count; i++)
        {
            players[i].UI.GetComponent<Text>().text = players[i].place.ToString() + "/" + players.Count.ToString();
            
        }
    }


    public void passLap(int playerNum, int count)
    {
        players[playerNum].laps[count].GetComponent<Image>().color = Color.green;
        if(players[playerNum].vehicle.GetComponent<VehicleEstabrook>().count+1 == goal)
        {
            isWon = true;
            winScreen.SetActive(true);
            Debug.Log(players[playerNum].vehicle.name + "Has won!");
            GetComponent<AudioSource>().Play();
        }
    }

    void creatPlacings()
    {
        if (players[0].vehicle.GetComponent<VehicleEstabrook>().score > players[1].vehicle.GetComponent<VehicleEstabrook>().score)
        {
            players[0].place = 1;
            players[1].place = 2;
        }
        else
        {
            players[0].place = 2;
            players[1].place = 1;
        }
    }

    void StartCountdown()
    {

        if (isCountdown)
        {
            countdown -= Time.deltaTime;
            //Debug.Log(countdown.ToString());
            if(countdown < 0)
            {
                isCountdown = false;
            }
            else
            {
                num3.GetComponent<Text>().text = countdown.ToString();

            }

        }
        else
        {
            num3.SetActive(false);
        }
        
        
        
    }
   
       
}
