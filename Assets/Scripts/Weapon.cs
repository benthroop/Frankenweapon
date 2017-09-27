using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	/* This is what's called the Base Class for our Weapons system. This code is meant to be as
	 * generic as possible and let our WeaponManager interface with it, unaware of the implementations
	 * of specific guns that inherit from Weapon, like ExampleGun.cs. Think of this as a bunch of buttons
	 * and WeaponManager just pushes them.*/

    public virtual void PrimaryFireStart()
    {
        Debug.Log("WEAPON CLASS: PRIMARY FIRE START!");
    }

    public virtual void PrimaryFireEnd()
    {
		Debug.Log("WEAPON CLASS: PRIMARY FIRE END!");
    }

    public virtual void SecondaryFireStart()
    {
		Debug.Log("WEAPON CLASS: SECONDARY FIRE START!");
    }

    public virtual void SecondaryFireEnd()
    {
		Debug.Log("WEAPON CLASS: SECONDARY FIRE END!");
    }

    public virtual void Reload()
    {
		Debug.Log("WEAPON CLASS: RELOADING");
    }
}
