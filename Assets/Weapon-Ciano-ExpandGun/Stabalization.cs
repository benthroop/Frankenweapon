using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Stabalization : MonoBehaviour {
	public Rigidbody ObjectRB;
	public bool Unstable;
	public GameObject UnstableEffect;

	public float UnstableTime = 1.2f;
	public int HowManyMake = 20;
	public GameObject Pieces;
	public float HowSmaller = 5f;

	public float ExplosivePower = 250;
	public float ExplosiveLift = 0.3f;
	public float Instability;
	public float MaximumExpansion;
	public float BornSize;
	public float CurrentSize;
	
	void Start() {
		Pieces = this.gameObject;
		ObjectRB = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		CurrentSize = transform.localScale.x;
		MaximumExpansion = 3;
		Instability = CurrentSize / BornSize;

		if (Instability >= MaximumExpansion) {
			Unstable = true;
			Debug.Log ("Unstable");
		}

		if (Unstable == true) {
			UnstableEffect.GetComponent<ParticleSystem> ().startSize = CurrentSize * 15f;
			UnstableTime = UnstableTime - Time.deltaTime;
			if (UnstableTime <= 0) {
				if (GetComponent<ExpandulatorBombs> () == null) {
					MakePieces ();
					Collider[] ItemsInRange = Physics.OverlapSphere (transform.position, CurrentSize * 10f);
					Debug.Log (ItemsInRange.Length);
					foreach (Collider Coliders in ItemsInRange) {
						if (Coliders.GetComponent<Rigidbody> () != null) {
							Coliders.GetComponent<Rigidbody> ().AddExplosionForce (ExplosivePower, transform.position, CurrentSize * 4f, ExplosiveLift);
						}
					}
				}
				Destroy (this.gameObject, 0.01f);
				UnstableEffect.transform.parent = null;

			}
		} else {
			UnstableEffect = null;
		}

		if (CurrentSize <= 0.015f) {
			Destroy (this.gameObject);
		}

	}

	void MakePieces() {
		while (HowManyMake > 0) {
			Debug.Log ("MakeSmalls");

			HowManyMake--;
			GameObject NewPieces = Instantiate (Pieces, transform.position + Random.insideUnitSphere * 1.25f, transform.rotation) as GameObject;
			NewPieces.transform.localScale = new Vector3 (transform.localScale.x/HowSmaller, transform.localScale.y/HowSmaller, transform.localScale.z/HowSmaller);
			Stabalization NewPieceStability = NewPieces.GetComponent<Stabalization> ();
			NewPieces.GetComponent<Rigidbody> ().mass = 0.2f;
			NewPieceStability.Unstable = false;
			Destroy (NewPieceStability.UnstableEffect);
			NewPieceStability.BornSize = NewPieces.transform.localScale.x;
			NewPieceStability.CurrentSize = NewPieces.transform.localScale.x;

		}
	}
}
