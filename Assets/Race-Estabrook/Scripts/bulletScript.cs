using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public int playerNum = 0;

    Rigidbody colRB;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(transform.right * -1000);

    }

    // Update is called once per frame
    void Update () {
        Destroy(this.gameObject, 2f);
	}
    

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<VehicleEstabrook>() == null || col.GetComponent<VehicleEstabrook>().playerNumber != playerNum)
        {
            
            colRB = col.GetComponent<Rigidbody>();
            if(colRB != null && !col.GetComponent<VehicleEstabrook>().isShielded)
            {
                Debug.Log("Hit Target");
                colRB.AddForce(transform.up * 100000);
                colRB.velocity -= col.transform.forward * 1;
                Destroy(this.gameObject);

            }

           

        }
    }
}
