using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/* This defines the ExampleGun as "inheriting" from Weapon.cs because we put
 *                       this \/   after the semicolon. That means we can "override" the functions inside Weapon with our own details of how they actually do stuff.*/
public class ExampleGun : Weapon
{
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

	//we could still make an override function for the Reload() and SecondaryFireStart() and SecondaryFireEnd(), but we don't need them for this example

	/* End of Overrides */


	/* Custom fields for Example Gun */
	/* These are all specific to our gun, which is basically a machine gun that fires rigidbody bullets */

	public ParticleSystem muzzleFlashParticles;
	public Transform muzzlePoint;
	public Rigidbody bulletPrefab;
	public float bulletVelocity;
	public float recoilRotationAmount;
	public float recoilPositionAmount;
	public float primaryFireRepeatDelay;

	/* Start of custom functions for Example Gun */

	/* This is a new method that doesn't override anything in Weapon.cs
	 * Notice that PrimaryFireStart is able to call it though. */
	public void FireBullet()
	{
		muzzleFlashParticles.Emit(1);
		GameObject lastBullet = Instantiate(bulletPrefab.gameObject, muzzlePoint.transform.position, muzzlePoint.transform.rotation, null);
		Rigidbody lastBulletRB = lastBullet.GetComponent<Rigidbody>();
		lastBulletRB.velocity = muzzlePoint.forward * bulletVelocity;
		transform.DOPunchRotation(new Vector3(recoilRotationAmount, 0f, 0f), .25f);
		transform.DOPunchPosition(transform.forward * recoilPositionAmount, .25f);
	}

	//This section is for making the gun repeat fire, again not part of Weapon.cs
	//Not every gun should repeat fire the same way. Take a gravity gun for instance. It would just start and stop.
	private bool isPrimaryFiring = false;
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
	}
}
