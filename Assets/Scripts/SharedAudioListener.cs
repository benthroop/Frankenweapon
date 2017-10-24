using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedAudioListener : MonoBehaviour 
{
	private List<GameObject> players;

	void Start()
	{
		players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
	}

	void LateUpdate()
	{	
		Vector3 pos = Vector3.zero;

		for (int i=0; i<players.Count; i++)
		{
			pos += players[i].transform.position;
		}

		transform.position = Vector3.Scale(pos, new Vector3(1f/players.Count, 1f/players.Count, 1f/players.Count));
		//TODO: Rotation
	}

}
