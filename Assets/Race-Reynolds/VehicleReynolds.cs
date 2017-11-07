using UnityEngine;
using System.Collections;

public class VehicleReynolds : VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;

    public GameObject frontRightVis;
    public GameObject frontLeftVis;
    public GameObject backRightVis;
    public GameObject backLeftVis;

    public GameObject positionTracker;

    public float maxSteer;
	public float maxTorque;
    public float downForce;

    public int playerNum;
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

        if (Vector3.Dot(transform.up, Vector3.up) > 0)
        {
            AddDownForce();
        }

       
        //notice that the wheel visuals do NOT turn. You might want to make that work if it's visible to the player.
        //there's actually a bit about that in the Unity Wheelcollider tutorial: https://docs.unity3d.com/Manual/WheelColliderTutorial.html
    }

    private void AddDownForce()
    {
        GetComponent<Rigidbody>().AddForce(-transform.up * downForce * GetComponent<Rigidbody>().velocity.magnitude);
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        Transform visualWheel;
        switch (collider.transform.name)
        {
            case "Front_right_Wheel":
                visualWheel = frontRightVis.transform;
                break;
            case "Front_left_Wheel":
                visualWheel = frontLeftVis.transform;
                break;
            case "Back_right_wheel":
                visualWheel = backRightVis.transform;
                break;
            case "Back_left_wheel":
                visualWheel = backLeftVis.transform;
                break;
            default:
                visualWheel = frontRightVis.transform;
                break;
        }

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
    void Update () 
	{
		Drive ();
	}
    private void FixedUpdate()
    {
        ApplyLocalPositionToVisuals(frontRight);
        ApplyLocalPositionToVisuals(frontLeft);
        ApplyLocalPositionToVisuals(backRight);
        ApplyLocalPositionToVisuals(backLeft);
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            positionTracker.GetComponent<PositionTracking>().UpdatePosition(playerNum, other.GetComponent<CheckpointScript>().index);
        }
    }
}
