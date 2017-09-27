using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public virtual void PrimaryFireStart()
    {
        Debug.Log("PRIMARY FIRE DOWn!");
    }

    public virtual void PrimaryFireEnd()
    {
        Debug.Log("PRIMARY FIRE UP!");
    }


    public virtual void SecondaryFireStart()
    {
        Debug.Log("SECONDARY FIRE!");
    }

    public virtual void SecondaryFireEnd()
    {
        Debug.Log("SECONDARY FIRE!");
    }

    public virtual void Reload()
    {

    }
}
