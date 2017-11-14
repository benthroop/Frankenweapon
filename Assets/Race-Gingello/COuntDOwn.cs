using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class COuntDOwn : MonoBehaviour {

    public Text CountDown;
    public float CD;
    public List<GameObject> Players;
    // Use this for initialization
    void Start () {
        Players[0].GetComponent<VehicleGingello>().enabled = !Players[0].GetComponent<VehicleGingello>().enabled;
        Players[1].GetComponent<VehicleGingello>().enabled = !Players[1].GetComponent<VehicleGingello>().enabled;
        Players[0].GetComponent<Rigidbody>().isKinematic = true;
        Players[1].GetComponent<Rigidbody>().isKinematic = true;
    }
	
	// Update is called once per frame
	void Update () {
        CDown();
        if (CD <= 0f)
        {
            Players[0].GetComponent<VehicleGingello>().enabled = true;
            Players[1].GetComponent<VehicleGingello>().enabled = true;
            Players[0].GetComponent<Rigidbody>().isKinematic = false;
            Players[1].GetComponent<Rigidbody>().isKinematic = false;
            Destroy(this.gameObject);
            Destroy(CountDown);
        }
        

	}

    void CDown()
    {

        CD -= 1* Time.deltaTime;
        CountDown.text = CD.ToString();

    }
}
