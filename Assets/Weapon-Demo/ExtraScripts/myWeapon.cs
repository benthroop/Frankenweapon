using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/* This defines the ExampleGun as "inheriting" from Weapon.cs because we put
 *                       this \/   after the semicolon. That means we can "override" the functions inside Weapon with our own details of how they actually do stuff.*/
public class myWeapon : Weapon
{

	public void Start()
	{
		light1.SetActive (false);
		light2.SetActive (false);
		light3.SetActive (false);
	}

	public float counter = 0.0f;
	/*  Overrides of Weapon.cs  */

	public override void PrimaryFireStart()
	{
		Debug.Log("EXAMPLE GUN CLASS: PRIMARY FIRE START!");
		isPrimaryFiring = true;
		currentFiringTimer = nextPrimaryFireTime;
	}

	public override void PrimaryFireEnd()
	{
		Debug.Log("EXAMPLE GUN CLASS: PRIMARY FIRE END!");
		isPrimaryFiring = false;
		transform.DOLocalRotate(Vector3.zero, .5f, RotateMode.Fast);
		transform.DOLocalMove(Vector3.zero, .5f);
	}

	public override void SecondaryFireStart()
	{
		
		isSecondaryFiring = true;
		currentFiringTimer = nextPrimaryFireTime;
	}

	public override void SecondaryFireEnd()
	{
		light1.SetActive (false);
		light2.SetActive (false);
		light3.SetActive (false);
		isSecondaryFiring = false;
		counter = 0f;
		transform.DOLocalRotate(Vector3.zero, .5f, RotateMode.Fast);
		transform.DOLocalMove(Vector3.zero, .5f);
	}

	//we could still make an override function for the Reload() and SecondaryFireStart() and SecondaryFireEnd(), but we don't need them for this example

	/* End of Overrides */


	/* Custom fields for Example Gun */
	/* These are all specific to our gun, which is basically a machine gun that fires rigidbody bullets */

	public ParticleSystem muzzleFlashParticles;
	public Transform muzzlePoint;
	public Transform muzzlePoint1;
	public Transform muzzlePoint2;
	public Transform muzzlePoint3;
	public GameObject light1;
	public GameObject light2;
	public GameObject light3;
	public Rigidbody bulletPrefab;
	public float bulletVelocity;
	public float recoilRotationAmount;
	public float recoilPositionAmount;
	public float primaryFireRepeatDelay;
	public AudioSource beep;
	public AudioSource ding;


	/* Start of custom functions for Example Gun */

	/* This is a new method that doesn't override anything in Weapon.cs
	* Notice that PrimaryFireStart is able to call it though. */
	public void FireBullet()
	{
		
		muzzleFlashParticles.Play ();
		GameObject lastBullet = Instantiate(bulletPrefab.gameObject, muzzlePoint.transform.position, muzzlePoint.transform.rotation, null);
		Rigidbody lastBulletRB = lastBullet.GetComponent<Rigidbody>();
		Debug.Log ("Rocket");
		lastBulletRB.velocity = muzzlePoint.forward * bulletVelocity;
		transform.DOPunchRotation(new Vector3(recoilRotationAmount, 0f, 0f), .25f);
		transform.DOPunchPosition(transform.forward * recoilPositionAmount, .25f);
	}

	public void FireSecondary()
	{

		muzzleFlashParticles.Play ();
		GameObject lastBullet1 = Instantiate(bulletPrefab.gameObject, muzzlePoint1.transform.position, muzzlePoint1.transform.rotation, null);
		Rigidbody lastBulletRB1 = lastBullet1.GetComponent<Rigidbody>();
		lastBulletRB1.velocity = muzzlePoint1.forward * bulletVelocity;

		GameObject lastBullet2 = Instantiate(bulletPrefab.gameObject, muzzlePoint2.transform.position, muzzlePoint2.transform.rotation, null);
		Rigidbody lastBulletRB2 = lastBullet2.GetComponent<Rigidbody>();
		lastBulletRB2.velocity = muzzlePoint2.forward * bulletVelocity;

		GameObject lastBullet3 = Instantiate(bulletPrefab.gameObject, muzzlePoint3.transform.position, muzzlePoint3.transform.rotation, null);
		Rigidbody lastBulletRB3 = lastBullet3.GetComponent<Rigidbody>();
		lastBulletRB3.velocity = muzzlePoint3.forward * bulletVelocity;

		transform.DOPunchRotation(new Vector3(recoilRotationAmount, 0f, 0f), .25f);
		transform.DOPunchPosition(transform.forward * recoilPositionAmount, .25f);
	}

	//This section is for making the gun repeat fire, again not part of Weapon.cs
	//Not every gun should repeat fire the same way. Take a gravity gun for instance. It would just start and stop.
	private bool isPrimaryFiring = false;
	private bool isSecondaryFiring = false;
	private float nextPrimaryFireTime = 0f;
	private float currentFiringTimer = 0f;

	public void Update()
	{
		if (isPrimaryFiring)
		{
			if (currentFiringTimer >= nextPrimaryFireTime)
			{
				FireBullet();
				nextPrimaryFireTime = primaryFireRepeatDelay;
				currentFiringTimer = 0f;
			}
			else
			{
				currentFiringTimer += Time.deltaTime;
			}
		}

		if (isSecondaryFiring)
		{
			counter++;
			if (counter == 60) {

				beep.Play ();
				light1.SetActive (true);
			}
			if (counter == 120) {

				beep.Play ();
				light2.SetActive (true);
			}

			if (counter == 170) {

				beep.Play ();
				light3.SetActive (true);
			}

			if (counter == 180) {

				light1.SetActive (false);
				light2.SetActive (false);
				light3.SetActive (false);
				counter = 0;
				FireSecondary ();
				ding.Play ();
			}
		}

	}


}
