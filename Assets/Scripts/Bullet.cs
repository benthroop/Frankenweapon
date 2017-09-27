using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public float lifetime = 4f;

	void Start()
	{
		Destroy(gameObject, lifetime);
	}

	void OnCollisionEnter(Collision col)
	{
		GetComponent<Renderer>().enabled = false;
		Destroy(this.gameObject);
	}
}
