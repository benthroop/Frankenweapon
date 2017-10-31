using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCiano : VehicleBase {
	
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;

	public bool DrivingForward;
	public List<GameObject> LeftWheels;
	public List<GameObject> RightWheels;

	public float VelocityOfCar;
	public float maxSteer;
	public float maxTorque;

	void Start () {
		//this is to keep the wheels from jittering
		frontRight.ConfigureVehicleSubsteps(5f, 12, 15);
		frontLeft.ConfigureVehicleSubsteps(5f, 12, 15);
		backRight.ConfigureVehicleSubsteps(5f, 12, 15);
		backLeft.ConfigureVehicleSubsteps(5f, 12, 15);
	}

	void Drive() {
		//turning
		frontLeft.steerAngle = steeringControlValue * maxSteer;
		frontRight.steerAngle = steeringControlValue * maxSteer;

		backLeft.motorTorque = throttleControlValue * maxTorque;
		backRight.motorTorque = throttleControlValue * maxTorque;


		VelocityOfCar = Vector3.Project(GetComponent<Rigidbody>().velocity, transform.forward).magnitude;

		if (GetComponent<Rigidbody> ().velocity.x >= 0) {
			DrivingForward = false;
		} else {
			DrivingForward = true;
		}

		foreach (GameObject LeftWheel in LeftWheels) {
			if (DrivingForward == true) {
				LeftWheel.transform.Rotate (new Vector3 (VelocityOfCar * 50 * Time.deltaTime, 0, 0));
			} else {
				LeftWheel.transform.Rotate (new Vector3 (VelocityOfCar * -50 * Time.deltaTime, 0, 0));
			}
		}

		foreach (GameObject RightWheel in RightWheels) {
			if (DrivingForward == true) {
				
				RightWheel.transform.Rotate (new Vector3 (VelocityOfCar * 50 * Time.deltaTime, 0, 0));
			} else {
				RightWheel.transform.Rotate (new Vector3 (VelocityOfCar * -50 * Time.deltaTime, 0, 0));
			}


		}
		//notice that the wheel visuals do NOT turn. You might want to make that work if it's visible to the player.
		//there's actually a bit about that in the Unity Wheelcollider tutorial: https://docs.unity3d.com/Manual/WheelColliderTutorial.html
	}

	void Update () {
		Drive ();
	}

	public override void BoostStart() {
		//all you
	}

	public override void BoostStop() {
		//all you
	}

	public override void ActionStart() {
		//all you
	}

	public override void ActionStop() {
		//all you
	}
}
