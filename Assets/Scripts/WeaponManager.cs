using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weaponList;
    public Weapon currentWeapon;
	public Transform registrationPoint;
	private Camera myCamera;

	public void Start()
	{
		if (currentWeapon == null)
		{
			SetWeaponToIndex(0);
		}
		else
		{
			currentWeapon.transform.parent = registrationPoint;
			currentWeapon.transform.localPosition = Vector3.zero;
			currentWeapon.transform.localRotation = Quaternion.identity;
			currentWeapon.transform.localScale = Vector3.one;
		}

		myCamera = GetComponentInChildren<Camera>();
		if (myCamera == null)
		{
			Debug.LogError("WeaponManager couldn't find its camera!");
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

		if (myCamera != null)
		{
			currentWeapon.playerCamera = myCamera;
		}
	}

	public void CurrentWeaponPrimaryFireStart()
	{
		currentWeapon.PrimaryFireStart();
	}

	public void CurrentWeaponPrimaryFireStop()
	{
		currentWeapon.PrimaryFireEnd();
	}

	public void CurrentWeaponSecondaryFireStart()
	{
		currentWeapon.SecondaryFireStart();
	}

	public void CurrentWeaponSecondaryFireStop()
	{
		currentWeapon.SecondaryFireEnd();
	}

	public void CurrentWeaponReload()
	{
		currentWeapon.Reload();
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
