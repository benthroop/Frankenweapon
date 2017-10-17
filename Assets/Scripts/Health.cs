using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float currentHP;
    public float maxHP;

    public UnityEvent OnTakeDamage;
    public UnityEvent OnDie;

    public bool TryTakeDamage(float hp)
    {
        currentHP -= hp;
        if (currentHP < 0f)
        {
            currentHP = 0f;
            Die();
        }

        if (OnTakeDamage != null)
        {
            OnTakeDamage.Invoke();
        }

        return true;
    }

    public void Die()
    {
        if (OnDie != null)
        {
            OnDie.Invoke();
        }
    }
	
}
