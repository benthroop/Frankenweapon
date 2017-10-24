using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ExpandulatorBombs : MonoBehaviour {
	public bool Active;
	public UnityEvent Activated;

	public GameObject ExplosionNoiseEmissions;
	public ParticleSystem ExpansionEmition;
	public GameObject UnstableEffect;

	public float ExplosivePower = 20f;
	public float ExplosiveLift = 0.3f;
	public float ExplosiveRadius = 20f;

	public float BornSize;
	public float NeededSize = 2.5f;
	public float CurrentSize;

	public float ExpansionRate = 10f;
	public float Range = 10f;
	public float FluxTime = 10f;
	public float FluxRubberyStats = 10f;

	void Awake () {
		BornSize = transform.localScale.x;
	}

	void Update () {
		if (Active == true) {
			CurrentSize = transform.localScale.x;
			transform.localScale = transform.localScale + new Vector3 (Time.deltaTime / 5f, Time.deltaTime / 5f, Time.deltaTime / 5f);
		}

		if ((NeededSize*BornSize) <= CurrentSize) {
			Debug.Log ("EXPLOSION");
			Explode ();
		}
	}
	void OnCollisionEnter() {
		Active = true;
		Activated.Invoke ();
	}

	void Explode () {
		float TimeBeforeBoom = 0.25f;
		ExpansionEmition.Emit (5);
		Collider[] ItemsInRange = Physics.OverlapSphere (transform.position, ExplosiveRadius);
		foreach (Collider Coliders in ItemsInRange) {
			if (Coliders.GetComponent<Rigidbody> () != null && Coliders.GetComponent<WeaponManager>() == null) {
				if (Coliders.GetComponent <Stabalization> () == null) {
					Coliders.transform.gameObject.AddComponent<Stabalization> ();
					Stabalization StableStatus = Coliders.transform.gameObject.GetComponent<Stabalization> ();
					StableStatus.BornSize = Coliders.transform.gameObject.transform.localScale.x;
				} else {
					Stabalization StableStatus = Coliders.transform.gameObject.GetComponent<Stabalization> ();
					if (StableStatus.Unstable == true && StableStatus.UnstableEffect == null) {
						GameObject ActiveUnstableEffect = Instantiate (UnstableEffect, Coliders.transform.position, Coliders.transform.rotation, Coliders.transform) as GameObject;
						StableStatus.UnstableEffect = ActiveUnstableEffect;
					}
				}

				Coliders.GetComponent<Rigidbody> ().AddExplosionForce (ExplosivePower, transform.position, ExplosiveRadius, ExplosiveLift);
				float DistanceBetween = Vector3.Distance (transform.position, Coliders.transform.position);
				Coliders.transform.DOPunchScale (new Vector3 ((ExpansionRate * ((1-DistanceBetween/10f))), (ExpansionRate * ((1-DistanceBetween/10f))), (ExpansionRate * ((1-DistanceBetween/10f)))), FluxTime, 2, FluxRubberyStats);
				Debug.Log(((1-DistanceBetween/10f)));
			}

			GameObject Boom = Instantiate (ExplosionNoiseEmissions, transform.position, transform.rotation);
			Destroy (this.gameObject, TimeBeforeBoom);
		}

	}
}
