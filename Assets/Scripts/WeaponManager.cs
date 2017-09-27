using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weaponList;
    public Weapon currentWeapon;
	public Transform registrationPoint;

	public void Start()
	{
		if (currentWeapon == null)
		{
			SetWeaponToIndex(0);
		}
	}

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

		if (Input.GetKeyDown(KeyCode.R))
		{
			currentWeapon.Reload();
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			PreviousWeapon();
		}

		if (Input.GetKeyDown(KeyCode.V))
		{
			NextWeapon();
		}
    }

	private int currentWeaponIndex;
	public void SetWeaponToIndex(int idx)
	{
		if (currentWeapon != null)
		{
			Destroy(currentWeapon.gameObject);
		}

		GameObject newWeaponGo = Instantiate(weaponList[idx].gameObject, registrationPoint);
		newWeaponGo.transform.localPosition = Vector3.zero;
		newWeaponGo.transform.localRotation = Quaternion.identity;
		newWeaponGo.transform.localScale = Vector3.one;
		currentWeapon = newWeaponGo.GetComponent<Weapon>();
	}

	public void NextWeapon()
	{
		currentWeaponIndex++;

		if (currentWeaponIndex >= weaponList.Count)
		{
			currentWeaponIndex = 0;
		}

		SetWeaponToIndex(currentWeaponIndex);
	}

	public void PreviousWeapon()
	{
		currentWeaponIndex--;

		if (currentWeaponIndex < 0)
		{
			currentWeaponIndex = weaponList.Count-1;
		}

		SetWeaponToIndex(currentWeaponIndex);
	}
}
