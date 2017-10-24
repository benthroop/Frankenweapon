using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	public GameObject explo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag != "Player" || col.tag != "Rocket") {
			GameObject exploTemp = Instantiate (explo, gameObject.transform.position, gameObject.transform.rotation);
			Destroy (exploTemp, 2f);
			Destroy (gameObject);
		}
	}
}
