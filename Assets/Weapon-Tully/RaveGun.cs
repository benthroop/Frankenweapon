using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaveGun : Weapon 
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public ParticleSystem muzzleFlash1;
    public ParticleSystem muzzleFlash2;
    public ParticleSystem muzzleFlash3;


    public List<GameObject> impactEffect;
    public AudioSource[] AudioClips = null;
    public List<Material> Mats;
    private float nextTimeToFire = 0f;
    

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            var num = Random.Range(0, impactEffect.Count);
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();





            if (num == 0)
            {
                AudioClips[0].Play();
            }

            if (num == 1)
            {
                AudioClips[1].Play();
            }
            if (num == 2)
            {
                AudioClips[2].Play();
            }
            if (num == 3)
            {
                AudioClips[3].Play();
            }

            //AudioSource audio = GetComponent<AudioSource>();
            //audio.Play();
            //audio.PlayDelayed(44100);
        }
    }

    void Shoot ()
    {
        var num = Random.Range(0, impactEffect.Count);
        if (num == 0)
        {
            muzzleFlash.Play();//Emit(25);
        }
        if (num == 1)
        {
            muzzleFlash1.Play();//Emit(25);
        }
        if (num == 2)
        {
            muzzleFlash2.Play();//Emit(25);
        }
        if (num == 3)
        {
            muzzleFlash3.Play();//Emit(25);
        }

        RaycastHit hit;
       if  (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);

            }

            GameObject impactGO = Instantiate(impactEffect[num], hit.point, Quaternion.LookRotation(hit.normal));
            impactGO.GetComponent<ParticleSystem>().Play();
            Destroy(impactGO, 2f);
        }

        

    }



}
