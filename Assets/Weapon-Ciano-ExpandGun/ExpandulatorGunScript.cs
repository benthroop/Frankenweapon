using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ExpandulatorGunScript : Weapon {

	public UnityEvent FiringPrimary;
	public UnityEvent StoppingPrimary;
	public Transform muzzlePoint;
	public GameObject LastShotObject;
	public float ExpansionRate = 10f;
	public float Range = 10f;
	public float FluxTime = 10f;
	public float FluxRubberyStats = 10f;

	private bool isPrimaryFiring = false;
	private LineRenderer LaserLine;

	public ParticleSystem FlashParticlesBarrel;
	public ParticleSystem FlashParticlesImpact;
	public GameObject UnstableEffect;

	//--------------------------------------------------

	public UnityEvent FiringSecondary;
	public UnityEvent StoppingSecondary;
	private bool isSecondaryFiring = false;
	public GameObject GranadePoint; 
	public float NeededChargeTime = 2f;
	public float CurrentCharge = 2f;
	public GameObject ExpandoGranade;


	public Camera PlayerView;

	public override void PrimaryFireStart(){
		isPrimaryFiring = true;
		FiringPrimary.Invoke ();
		LaserLine.enabled = true;
	}

	public override void PrimaryFireEnd() {
		isPrimaryFiring = false;
		LaserLine.enabled = false;
		StoppingPrimary.Invoke ();
		LastShotObject = null;

	}

	public override void SecondaryFireStart(){
		isSecondaryFiring = true;
		FiringSecondary.Invoke ();
		StoppingSecondary.Invoke ();
	}

	public override void SecondaryFireEnd() {
		isSecondaryFiring = false;
		CurrentCharge = NeededChargeTime;
	}

	public void Start() {
		PlayerView = this.playerCamera;
		LaserLine = GetComponent<LineRenderer> ();
		CurrentCharge = NeededChargeTime;
	}

	public void Update() {
		if (isPrimaryFiring == true) {
			FlashParticlesBarrel.Emit (1);
			RaycastHit HitInfo;

			Vector3 RayOrigin = PlayerView.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0.3f));
			LaserLine.SetPosition (0, muzzlePoint.position);
			LaserLine.SetPosition (1, RayOrigin + (PlayerView.transform.forward * Range));

			if (Physics.Raycast (PlayerView.transform.position, PlayerView.transform.forward, out HitInfo, Range)) {
				LaserLine.SetPosition (1, HitInfo.point);

				FlashParticlesImpact.Emit (1);
				FlashParticlesImpact.transform.position = HitInfo.point;
				FlashParticlesImpact.startSize = 0.36f;
				if (HitInfo.rigidbody != null) {
					
					LastShotObject = HitInfo.transform.gameObject;

					if (HitInfo.transform.gameObject.GetComponent<Stabalization>() != null && HitInfo.transform.gameObject.GetComponent<WeaponManager>() == null) {
						Stabalization StableStatus = LastShotObject.GetComponent<Stabalization> ();
						FlashParticlesImpact.startSize = 0.36f * (1 + HitInfo.transform.localScale.x);
						HitInfo.rigidbody.mass = HitInfo.rigidbody.mass + (ExpansionRate * 10);

						if (StableStatus.Unstable == true && StableStatus.UnstableEffect == null) {
							GameObject ActiveUnstableEffect = Instantiate (UnstableEffect, LastShotObject.transform.position, LastShotObject.transform.rotation, LastShotObject.transform) as GameObject;
							StableStatus.UnstableEffect = ActiveUnstableEffect;
						}

						HitInfo.transform.DOPunchScale (new Vector3 (ExpansionRate, ExpansionRate, ExpansionRate), FluxTime, 2, FluxRubberyStats);
					} else if (HitInfo.transform.gameObject.GetComponent<Stabalization>() == null && HitInfo.transform.gameObject.GetComponent<WeaponManager>() == null){
						HitInfo.transform.gameObject.AddComponent<Stabalization> ();
						Stabalization StableStatus = LastShotObject.GetComponent<Stabalization> ();
						StableStatus.BornSize = LastShotObject.transform.localScale.x;
					}
				}
			}
		} 

		if (isSecondaryFiring == true) {
			if (NeededChargeTime <= CurrentCharge) {
				CurrentCharge = 0;
				GameObject ExpandBoom = Instantiate (ExpandoGranade, GranadePoint.transform.position, GranadePoint.transform.rotation) as GameObject;
				ExpandBoom.GetComponent<Rigidbody> ().AddForce (transform.forward * 750f);

			} else {
				CurrentCharge = CurrentCharge + Time.deltaTime;
			}
		}
	}
}

