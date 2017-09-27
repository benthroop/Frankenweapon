using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public virtual void PrimaryFireStart()
    {
        Debug.Log("PRIMARY FIRE START!");
    }

    public virtual void PrimaryFireEnd()
    {
        Debug.Log("PRIMARY FIRE END!");
    }

    public virtual void SecondaryFireStart()
    {
        Debug.Log("SECONDARY FIRE START!");
    }

    public virtual void SecondaryFireEnd()
    {
        Debug.Log("SECONDARY FIRE END!");
    }

    public virtual void Reload()
    {
		Debug.Log("RELOADING");
    }
}
