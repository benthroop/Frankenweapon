using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExampleGun : Weapon
{
    public ParticleSystem muzzleFlashParticles;
    public Transform muzzlePoint;
    public Rigidbody bullet;
    public float bulletVelocity;
    public float recoilRotationAmount;
	public float recoilPositionAmount;
	public float primaryFireRepeatDelay;

    public override void PrimaryFireStart()
    {
        Debug.Log("FIRING EXAMPLE GUN");
		isPrimaryFiring = true;
		currentFiringTimer = nextPrimaryFireTime;
    }

	public override void PrimaryFireEnd()
	{
		isPrimaryFiring = false;
		transform.DOLocalRotate(Vector3.zero, .5f, RotateMode.Fast);
		transform.DOLocalMove(Vector3.zero, .5f);
	}

	private void FireBullet()
	{
		muzzleFlashParticles.Emit(1);
		GameObject lastBullet = Instantiate(bullet.gameObject, muzzlePoint.transform.position, muzzlePoint.transform.rotation, null);
		Rigidbody lastBulletRB = lastBullet.GetComponent<Rigidbody>();
		lastBulletRB.velocity = muzzlePoint.forward * bulletVelocity;
		transform.DOPunchRotation(new Vector3(recoilRotationAmount, 0f, 0f), .25f);
		transform.DOPunchPosition(transform.forward * recoilPositionAmount, .25f);
	}

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
