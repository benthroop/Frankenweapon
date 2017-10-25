using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBase : MonoBehaviour 
{
	protected float steeringControlValue;
	protected float throttleControlValue;

	public virtual void SetThrottle(float throttleValue)
	{
		throttleControlValue = throttleValue;
	}

	public virtual void SetSteering(float steeringValue)
	{
		steeringControlValue = steeringValue;
	}

	public virtual void BoostStart()
	{

	}

	public virtual void BoostStop()
	{
	
	}

	public virtual void ActionStart()
	{

	}

	public virtual void ActionStop()
	{

	}

	public virtual void NextGear()
	{

	}

	public virtual void PreviousGear()
	{

	}
}
