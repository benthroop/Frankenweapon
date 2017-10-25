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

	public virtual void Boost()
	{

	}

	public virtual void Action()
	{

	}

	public virtual void NextGear()
	{

	}

	public virtual void PreviousGear()
	{

	}
}
