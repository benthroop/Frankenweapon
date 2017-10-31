using UnityEngine;
using System.Collections;

public class VehicleDemo : VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;
   public Transform flWheel;
   public Transform frWheel;
   public Transform blWheel;
   public Transform brWheel;
   public float maxSteer;
	public float maxTorque;

	void Start () 
	{
		//this is to keep the wheels from jittering
		frontRight.ConfigureVehicleSubsteps(5f, 12, 15);
		frontLeft.ConfigureVehicleSubsteps(5f, 12, 15);
		backRight.ConfigureVehicleSubsteps(5f, 12, 15);
		backLeft.ConfigureVehicleSubsteps(5f, 12, 15);
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

         flWheel.localEulerAngles = new Vector3(flWheel.localEulerAngles.x, frontLeft.steerAngle - flWheel.localEulerAngles.z, flWheel.localEulerAngles.z);
         frWheel.localEulerAngles = new Vector3(frWheel.localEulerAngles.x, frontRight.steerAngle - frWheel.localEulerAngles.z, frWheel.localEulerAngles.z);
 
         flWheel.Rotate(frontLeft.rpm / 60 * 360 * Time.deltaTime, 0, 0);
         frWheel.Rotate(frontRight.rpm / 60 * 360 * Time.deltaTime, 0, 0);
         blWheel.Rotate(backLeft.rpm / 60 * 360 * Time.deltaTime, 0, 0);
         brWheel.Rotate(backRight.rpm / 60 * 360 * Time.deltaTime, 0, 0);


	}
	
	void Update () 
	{
		Drive ();
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
		//all you
	}

	public override void ActionStop()
	{
		//all you
	}
}
