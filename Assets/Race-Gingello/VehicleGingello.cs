using UnityEngine;
using System.Collections;

public class VehicleGingello : VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;

	public float maxSteer;
	public float maxTorque;
    public float Boost;
    public bool Boosting;



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
        if (Boosting == false)
        {
            //torque
            backLeft.motorTorque = throttleControlValue * maxTorque;
            backRight.motorTorque = throttleControlValue * maxTorque;
            Debug.Log(backRight.motorTorque.ToString());
            Debug.Log(backLeft.motorTorque.ToString());
        }
        if(Boosting == true)
        {
            backLeft.motorTorque = throttleControlValue * maxTorque * Boost;
            backRight.motorTorque = throttleControlValue * maxTorque * Boost;
            Debug.Log(backRight.motorTorque.ToString());
            Debug.Log(backLeft.motorTorque.ToString());

        }
        ApplyLocalPositionToVisuals(frontRight);
        ApplyLocalPositionToVisuals(frontLeft);
        ApplyLocalPositionToVisuals(backRight);
        ApplyLocalPositionToVisuals(backLeft);
        //notice that the wheel visuals do NOT turn. You might want to make that work if it's visible to the player.
        //there's actually a bit about that in the Unity Wheelcollider tutorial: https://docs.unity3d.com/Manual/WheelColliderTutorial.html
    }
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


    void Update () 
	{
   
        Drive();
    }
	public override void BoostStart()
    {
        Boosting = true;        
    }

	public override void BoostStop()
	{
	    Boosting = false;
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
