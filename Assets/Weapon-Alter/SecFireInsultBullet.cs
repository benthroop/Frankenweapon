using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecFireInsultBullet : MonoBehaviour {

    public float lifetime = 4f;
    public float TimeBeforeDeath;
    public GameObject InsultVisual2;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer();
        if (TimeBeforeDeath <= 1)
        {
            //Debug.Log("Reset");
            Destroy(this);
        }
    }

    void Timer()
    {
        TimeBeforeDeath -= Time.deltaTime;
        // Debug.Log(TimeBeforeDeath);
    }
    void OnCollisionEnter(Collision col)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;

        //Destroy(InsultVisual2);
    }
}
