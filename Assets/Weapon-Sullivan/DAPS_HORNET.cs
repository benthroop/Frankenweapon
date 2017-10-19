using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAPS_HORNET : Weapon
{

    public override void PrimaryFireStart()
    {
        isPrimaryFiring = true;
        currentFiringTimer = nextPrimaryFireTime;
    }

    public override void PrimaryFireEnd()
    {
        isPrimaryFiring = false; 
    }

    // Use this for initialization
    void Start () {
		
	}

    void FireBEES ()
    {
        MuzzleFlash.Emit(5); 
        Instantiate(BeeSwarm, Barrel1.transform.position, Barrel1.transform.rotation);
        Instantiate(BeeSwarm, Barrel2.transform.position, Barrel2.transform.rotation);
        Instantiate(BeeSwarm, Barrel3.transform.position, Barrel3.transform.rotation);
        Instantiate(BeeSwarm, Barrel4.transform.position, Barrel4.transform.rotation);
        Instantiate(BeeSwarm, Barrel5.transform.position, Barrel5.transform.rotation);
        Instantiate(BeeSwarm, Barrel6.transform.position, Barrel6.transform.rotation);
    }
	
	

    private bool isPrimaryFiring = false;
    private float nextPrimaryFireTime = 0f;
    private float currentFiringTimer = 0f;
    public float primaryFireRepeatDelay;
    public ParticleSystem MuzzleFlash; 
    public GameObject BeeSwarm;
    public GameObject Barrel1;
    public GameObject Barrel2;
    public GameObject Barrel3;
    public GameObject Barrel4;
    public GameObject Barrel5;
    public GameObject Barrel6;

    public void Update()
    {
        if (isPrimaryFiring)
        {
            if (currentFiringTimer >= nextPrimaryFireTime)
            {
                FireBEES();
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
