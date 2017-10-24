using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ShakespeareGun : Weapon 
{
    /*  Overrides of Weapon.cs  */


    public bool CanFire = true;
    public bool CanFireSecondary =false;


    public override void PrimaryFireStart()
    {
		Debug.Log("EXAMPLE GUN CLASS: PRIMARY FIRE START!");
		isPrimaryFiring = true;
        
            FireBullet();
       
    }

    public override void SecondaryFireStart()
    {
        Debug.Log("EXAMPLE GUN CLASS: Secondary FIRE START!");
        isSecondaryFiring = true;

        FireSecondaryBullet();

        Debug.Log("Sec Fire");
    }

    public override void PrimaryFireEnd()
	{
		Debug.Log("EXAMPLE GUN CLASS: PRIMARY FIRE END!");
		isPrimaryFiring = false;
		transform.DOLocalRotate(Vector3.zero, .5f, RotateMode.Fast);
		transform.DOLocalMove(Vector3.zero, .5f);
	}
    public override void SecondaryFireEnd()
    {
        Debug.Log("EXAMPLE GUN CLASS: Secondary FIRE START!");
        isSecondaryFiring = false;

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
    public Rigidbody bullet2Prefab;
	public float bulletVelocity;
    public float bullet2Velocity;

    public float recoilRotationAmount;
	public float recoilPositionAmount;
	public float primaryFireRepeatDelay;
    private GameObject BulletCheck;
    private int SecondaryFireIncrement;

    /* Start of custom functions for Example Gun */

    /* This is a new method that doesn't override anything in Weapon.cs
	 * Notice that PrimaryFireStart is able to call it though. */
    public void FireBullet()
	{
        if (CanFire == true)
        {
            // Debug.Log("called");
            CanFire = false;
            muzzleFlashParticles.Emit(1);
            GameObject lastBullet = Instantiate(bulletPrefab.gameObject, muzzlePoint.transform.position, muzzlePoint.transform.rotation, null);
            Rigidbody lastBulletRB = lastBullet.GetComponent<Rigidbody>();
            lastBulletRB.velocity = muzzlePoint.forward * bulletVelocity;
            transform.DOPunchRotation(new Vector3(recoilRotationAmount, 0f, 0f), 1.25f);
            transform.DOPunchPosition(transform.forward * recoilPositionAmount, .25f);
            SecondaryFireIncrement += 1;
            if(SecondaryFireIncrement >=4)
            {
                CanFire = false;
                CanFireSecondary = true;
            }

        }
        

        
	}

    public void FireSecondaryBullet()
    {
        if (CanFireSecondary == true)
        {


            muzzleFlashParticles.Emit(1);
            GameObject lastBullet = Instantiate(bullet2Prefab.gameObject, muzzlePoint.transform.position, muzzlePoint.transform.rotation, null);
            Rigidbody lastBulletRB = lastBullet.GetComponent<Rigidbody>();
            lastBulletRB.velocity = muzzlePoint.forward * bullet2Velocity;
            transform.DOPunchRotation(new Vector3(recoilRotationAmount, 0f, 0f), 2.25f);
            transform.DOPunchPosition(transform.forward * recoilPositionAmount, 0.25f);
            CanFire = true;
            CanFireSecondary = false;
            SecondaryFireIncrement = 0;
        }
    }

    //This section is for making the gun repeat fire, again not part of Weapon.cs
    //Not every gun should repeat fire the same way. Take a gravity gun for instance. It would just start and stop.
    private bool isPrimaryFiring = false;
    private bool isSecondaryFiring = false;
	
}

