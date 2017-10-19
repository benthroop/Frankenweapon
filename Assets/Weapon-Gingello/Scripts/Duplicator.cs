using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


public class Duplicator : Weapon {

	public float range = 100f;
	public float impForce = 200f;
	public float fireRate = 25f;
	private float nextTimeToFire = 0f;
	public float sizemultiplier, sizeMax;


	public GameObject impactEffect, NewBullet;
	public ParticleSystem muzzleFlashParticles;
	public UnityEvent Bang, GetBullet;

	public Transform muzzlePoint;
    public Vector3 InitbulletScale;

	public float bulletVelocity;
	public float recoilRotationAmount;
	public float recoilPositionAmount;
	public float primaryFireRepeatDelay;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            DeleteBullets();

        }
    }

    public override void PrimaryFireStart()
		{if(Time.time >= nextTimeToFire)
			{
			nextTimeToFire = Time.time + 1f / fireRate;
			FireBullet ();
			Debug.Log ("SHoot");
			muzzleFlashParticles.Emit (1);
			}
		}

	public override void SecondaryFireStart()
	{if (Time.time >= nextTimeToFire) {
		

			nextTimeToFire = Time.time + 1f/fireRate;
			DetectBullet();

		}
	}

	public override void Reload()
	{
		if (sizemultiplier >= sizeMax) {
		
			sizemultiplier = 1f;
		
		} else {
			sizemultiplier += 1f;
			Debug.Log ("SizeChange");
		}

	}

	void DeleteBullets()
	{
		RaycastHit DelBul;
		if (Physics.Raycast (playerCamera.transform.position, playerCamera.transform.forward, out DelBul, range)) {
			if (DelBul.rigidbody != null && DelBul.transform.gameObject.tag != "Player") {
				Destroy (DelBul.transform.gameObject);
				Debug.Log ("Deleted");
			}
		}
	}

	void DetectBullet()
	{

		RaycastHit Hit;
		if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out Hit, range))
		{
			if(Hit.rigidbody != null && Hit.transform.gameObject.tag != "Player")
			{
				GetBullet.Invoke();
				NewBullet = Hit.transform.gameObject;
                InitbulletScale = new Vector3(NewBullet.transform.localScale.x / sizemultiplier, NewBullet.transform.localScale.y / sizemultiplier, NewBullet.transform.localScale.z / sizemultiplier);                              
			}
            GameObject impactDel = Instantiate(impactEffect, Hit.point, Quaternion.LookRotation(Hit.normal));           
			Destroy(impactDel, 2f);
		}
	}

	public void FireBullet()
	{if (NewBullet != null)
		{

			Bang.Invoke();
			muzzleFlashParticles.Emit(1);
            
            GameObject lastBullet = Instantiate (NewBullet.gameObject, muzzlePoint.transform.position, muzzlePoint.transform.rotation, null);

           
            Vector3 SpawnSize = new Vector3 (InitbulletScale.x * sizemultiplier, InitbulletScale.y * sizemultiplier,InitbulletScale.z *  sizemultiplier);
			lastBullet.transform.localScale = SpawnSize;
			Rigidbody lastBulletRB = lastBullet.GetComponent<Rigidbody>();
			lastBulletRB.velocity = muzzlePoint.forward * bulletVelocity;
			transform.DOPunchRotation(new Vector3(recoilRotationAmount, 0f, 0f), .25f);
			transform.DOPunchPosition(transform.forward * recoilPositionAmount, .25f);
		}
	}




}

