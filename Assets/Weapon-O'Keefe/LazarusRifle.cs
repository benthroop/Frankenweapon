using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazarusRifle : Weapon
{
    RaycastHit lazerHit;
    bool firingLazer;

    LineRenderer lazerVisual;
    public Transform muzzlePoint;
    public GameObject muzzleLight; //prefab
    GameObject muzzleGlow; //instance

    public GameObject mirrorPrefab;
    GameObject lastMirrorPlaced;

    public float range;

    AudioSource gunSpeaker;

    void Start()
    {
        firingLazer = false;
        lazerVisual = gameObject.GetComponent<LineRenderer>();
        gunSpeaker = GetComponent<AudioSource>();

    }

    public override void PrimaryFireStart()
    {
        firingLazer = true;
        muzzleGlow = Instantiate(muzzleLight, muzzlePoint);
        gunSpeaker.Play();
    }

    public override void PrimaryFireEnd()
    {
        firingLazer = false;
        Destroy(muzzleGlow);
        gunSpeaker.Stop();
    }

    public override void SecondaryFireStart()
    {
        RaycastHit mirrorHit;
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out mirrorHit, range))
        {
            lastMirrorPlaced = Instantiate(mirrorPrefab, mirrorHit.point, Quaternion.LookRotation(mirrorHit.normal));

            lastMirrorPlaced.transform.parent = mirrorHit.transform;
        }
    }
	
	void Update ()
    {
        lazerVisual.SetPosition(0, muzzlePoint.position);

        if (firingLazer)
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out lazerHit))
            {
                lazerVisual.SetPosition(1, lazerHit.point);
            }
            else
            {
                lazerVisual.SetPosition(1, muzzlePoint.position);
            }
        }
        else
        {
            lazerVisual.SetPosition(1, muzzlePoint.position);
        }
	}
}
