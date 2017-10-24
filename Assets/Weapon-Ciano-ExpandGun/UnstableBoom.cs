using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class UnstableBoom : MonoBehaviour {

	public UnityEvent EndBoom;
	public bool Done;
	public GameObject BoomSound;
	void Update() {
		if (transform.parent == null) {
			if (Done == false) {
				EndBoom.Invoke ();
				Done = true;
			}
		}

		if (Done == true & BoomSound.GetComponent<AudioSource> ().isPlaying == false) {
			Destroy (this.gameObject);
		}
	}
}
