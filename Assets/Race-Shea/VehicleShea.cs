﻿using UnityEngine;
using System.Collections;

public class VehicleShea : VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;

	public float maxSteer;
	public float maxTorque;
    public int maxTorqueIncrement;
    private float maxTorqueStart;

    private float _originalRotX;
    private float _originalRotZ;

	void Start () 
	{
        maxTorqueStart = maxTorque;
		//this is to keep the wheels from jittering
		frontRight.ConfigureVehicleSubsteps(5f, 12, 15);
		frontLeft.ConfigureVehicleSubsteps(5f, 12, 15);
		backRight.ConfigureVehicleSubsteps(5f, 12, 15);
		backLeft.ConfigureVehicleSubsteps(5f, 12, 15);

        _originalRotX = transform.rotation.x;
        _originalRotZ = transform.rotation.z;
    }

	void Drive()
	{
		//turning
		frontLeft.steerAngle = steeringControlValue * maxSteer;
		frontRight.steerAngle = steeringControlValue * maxSteer;

		//torque
		backLeft.motorTorque = throttleControlValue * maxTorque;
		backRight.motorTorque = throttleControlValue * maxTorque;

		//notice that the wheel visuals do NOT turn. You might want to make that work if it's visible to the player.
		//there's actually a bit about that in the Unity Wheelcollider tutorial: https://docs.unity3d.com/Manual/WheelColliderTutorial.html
	}

    public void TurnWheels (WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }
        Transform wheelVisual = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        wheelVisual.transform.position = position;
        wheelVisual.transform.rotation = rotation;
    }
	
	void Update () 
	{
		Drive ();
        TurnWheels(frontLeft);
        TurnWheels(frontRight);

        if (Input.GetKeyDown(KeyCode.R))
        {
            flipCar();
        }
    }

    void flipCar()
    {
        transform.Translate(0, 3, 0);
        transform.rotation = (Quaternion.Euler(new Vector3(_originalRotX, transform.rotation.y, _originalRotZ)));
    }

	public override void BoostStart()
	{
        //car goes faster
        //set maxtorque higher??
        maxTorque = (maxTorque * maxTorqueIncrement);
	}

	public override void BoostStop()
	{
        //maxtorque back to normal
        maxTorque = maxTorqueStart;
	}

	public override void ActionStart()
	{
		//all you
	}

	public override void ActionStop()
	{
		//all you
	}

}
