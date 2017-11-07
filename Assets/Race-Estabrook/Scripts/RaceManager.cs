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
       
    }
    

    [SerializeField]
    List<playerVehicles> players;
    [SerializeField]
    List<GameObject> mapPoints; 
    
    int count = 0;
    bool hasChecked = false;

    public bool isWon= false;

    public static RaceManager instance;
    private void Awake()
    {
        
        instance = this;
        
    }
    
    void Update()
    {
        for(int i = 0; i < players.Count; i++)
        {
            players[i].UI.GetComponent<Text>().text = players[i].vehicle.GetComponent<VehicleEstabrook>().score.ToString();
            
        }
    }


    public void passLap(int playerNum, int count)
    {
        players[playerNum].laps[count].GetComponent<Image>().color = Color.green;
        if(players[playerNum].laps.Count == count)
        {
            isWon = true;
            Debug.Log(players[playerNum].vehicle.name + "Has won!");
        }
    }
       
}
