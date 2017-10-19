
using UnityEngine;

public class Gun : Weapon {

	public float damage = 10f;
	public float range = 100f;
	public float impactForce = 50f;

	public ParticleSystem MuzzleFlash;
	public GameObject impactEffect;
	public AudioClip Fire;

    /*
	void Update () {

		if (Input.GetButtonDown ("Fire1")) 
		{
			Shoot ();
	
		}
		
	}*/

    public override void PrimaryFireStart()
    {
        Shoot();
    }


    void Shoot ()
	{
        GetComponent<AudioSource>().Play();
		MuzzleFlash.Play ();

		RaycastHit hit;
		if (Physics.Raycast (playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
		{
			Debug.Log (hit.transform.name);

			if (hit.rigidbody != null)
			{
				hit.rigidbody.AddForce (-hit.normal * impactForce);
			}

			GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy (impactGO, 2f);
		}
	}

}