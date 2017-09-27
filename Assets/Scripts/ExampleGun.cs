﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExampleGun : Weapon
{
    public ParticleSystem muzzleFlashParticles;
    public Transform muzzlePoint;
    public Rigidbody bullet;
    public float bulletVelocity;
    public float recoilAmount;

    public override void PrimaryFireStart()
    {
        Debug.Log("FIRING EXAMPLE GUN");
        muzzleFlashParticles.Emit(1);
        GameObject lastBullet = Instantiate(bullet.gameObject, muzzlePoint.transform.position, muzzlePoint.transform.rotation, null);
        Rigidbody lastBulletRB = lastBullet.GetComponent<Rigidbody>();

        lastBulletRB.velocity = muzzlePoint.forward * bulletVelocity;
        transform.DOPunchRotation(new Vector3(recoilAmount, 0f, 0f), .25f);
    }


}
