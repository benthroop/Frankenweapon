using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weaponList;
    public Weapon currentWeapon;

    public void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            currentWeapon.PrimaryFireStart();
        }

        if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            currentWeapon.PrimaryFireEnd();
        }
    }
}
