using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SimpleCar : VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;

	public float maxSteer;
	public float maxTorque;
	public float maxBrake;
    public float downforce;

    private Rigidbody myRigidbody;

	void Start () 
	{
		//this is to keep the wheels from jittering
		frontRight.ConfigureVehicleSubsteps(5f, 12, 15);
		frontLeft.ConfigureVehicleSubsteps(5f, 12, 15);
		backRight.ConfigureVehicleSubsteps(5f, 12, 15);
		backLeft.ConfigureVehicleSubsteps(5f, 12, 15);

        myRigidbody = GetComponent<Rigidbody>();
	}

	// finds the corresponding visual wheel
	// correctly applies the transform
	public void ApplyLocalPositionToVisuals(WheelCollider collider)
	{
		if (collider.transform.childCount == 0)
		{
			return;
		}

		Transform visualWheel = collider.transform.GetChild(0);

		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);

		visualWheel.transform.position = position;
		visualWheel.transform.rotation = rotation;
	}

	void Drive()
	{
		//turning
		frontLeft.steerAngle = steeringControlValue * maxSteer;
		frontRight.steerAngle = steeringControlValue * maxSteer;

		//throttle backward
		if (throttleControlValue < 0f) 
		{
            //moving backward
			if (transform.InverseTransformVector(GetComponent<Rigidbody>().velocity).z < Mathf.Epsilon)
			{
				backLeft.brakeTorque = 0f;
				backRight.brakeTorque = 0f;
				frontLeft.brakeTorque = 0f;
				frontRight.brakeTorque = 0f;

				backLeft.motorTorque = throttleControlValue * maxTorque;
				backRight.motorTorque = throttleControlValue * maxTorque;
			}
            //moving forward
			else
			{
				backLeft.brakeTorque = maxBrake * Mathf.Abs(throttleControlValue);
				backRight.brakeTorque = maxBrake * Mathf.Abs(throttleControlValue);
				frontLeft.brakeTorque = maxBrake * Mathf.Abs(throttleControlValue);
				frontRight.brakeTorque = maxBrake * Mathf.Abs(throttleControlValue);
			}
		}
        //throttle forward
		else
		{
            //moving forward
			if (transform.InverseTransformVector(GetComponent<Rigidbody>().velocity).z > Mathf.Epsilon)
			{
				backLeft.brakeTorque = 0f;
				backRight.brakeTorque = 0f;
				frontLeft.brakeTorque = 0f;
				frontRight.brakeTorque = 0f;

				backLeft.motorTorque = throttleControlValue * maxTorque;
				backRight.motorTorque = throttleControlValue * maxTorque;
			}
            //moving backward
			else
			{
				backLeft.brakeTorque = maxBrake * Mathf.Abs(throttleControlValue);
				backRight.brakeTorque = maxBrake * Mathf.Abs(throttleControlValue); ;
				frontLeft.brakeTorque = maxBrake * Mathf.Abs(throttleControlValue);
				frontRight.brakeTorque = maxBrake * Mathf.Abs(throttleControlValue);
			}
		}

		//notice that the wheel visuals do NOT turn. You might want to make that work if it's visible to the player.
		//there's actually a bit about that in the Unity Wheelcollider tutorial: https://docs.unity3d.com/Manual/WheelColliderTutorial.html

		ApplyLocalPositionToVisuals(frontRight);
		ApplyLocalPositionToVisuals(frontLeft);
		ApplyLocalPositionToVisuals(backLeft);
		ApplyLocalPositionToVisuals(backRight);
	}
	
	void FixedUpdate () 
	{
		Drive ();

        if (IsGrounded())
        {
            Debug.Log("DOWNFORCE: ON");
            AddDownForce();
        }
        else
        {
            Debug.Log("DOWNFORCE: OFF");
        }
    }

    private bool IsGrounded()
    {
        WheelHit leftHit;
        WheelHit rightHit;
        bool result = false;

        if (frontLeft.GetGroundHit(out leftHit))
        {
            result = true;
        }

        if (frontRight.GetGroundHit(out rightHit))
        {
            result = true;
        }

        return result;
    }

    private void AddDownForce()
    {
       myRigidbody.AddForce(-transform.up * downforce * myRigidbody.velocity.magnitude);
    }

    public override void BoostStart()
	{
		//all you
	}

	public override void BoostStop()
	{
		//all you
	}

	public override void ActionStart()
	{
        //emergency brake on
        backLeft.brakeTorque = 10000f;
        backRight.brakeTorque = 10000f;
    }

	public override void ActionStop()
	{
        //emergency brake off
        backLeft.brakeTorque = 0f;
        backRight.brakeTorque = 0f;

    }
}
