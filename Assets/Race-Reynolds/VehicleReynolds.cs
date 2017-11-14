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

    public GameObject positionTrack;
    public PositionTracking positionTracker;
    public GameObject groundChecker;
    public GameObject audioPlayer;
    public GameObject audioPlayer2;

    public float maxSteer;
	public float maxTorque;
    public float downForce;

    public bool turning = false;
    public bool grounded = true;

    public int playerNum;
    public int resetTimer = 0;
    int startTimer = 0;
	void Start () 
	{
        positionTracker = positionTrack.GetComponent<PositionTracking>();
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
       if (grounded)
        {
            GetComponent<Rigidbody>().AddForce(-transform.up * downForce * GetComponent<Rigidbody>().velocity.magnitude);
        }
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
        startTimer++;
        if (startTimer > 160)
        {
            Drive();
            GetComponent<Rigidbody>().isKinematic = false;
        }
        if (GetComponent<Rigidbody>().velocity.magnitude < 1)
        {
            resetTimer++;
        }
        else
        {
            resetTimer = 0;
        }
        if (resetTimer >= 60 && startTimer > 220)
        {
            switch (playerNum)
            {
                case 1:
                    transform.position = positionTracker.checkpoints[positionTracker.p1Index].transform.position;
                    transform.rotation = positionTracker.checkpoints[positionTracker.p1Index].transform.localRotation;
                    break;
                case 2:
                    transform.position = positionTracker.checkpoints[positionTracker.p2Index].transform.position;
                    transform.rotation = positionTracker.checkpoints[positionTracker.p2Index].transform.localRotation;
                    break;
                default:
                    break;
            }
        }
	}
    private void FixedUpdate()
    {
        ApplyLocalPositionToVisuals(frontRight);
        ApplyLocalPositionToVisuals(frontLeft);
        ApplyLocalPositionToVisuals(backRight);
        ApplyLocalPositionToVisuals(backLeft);
        if (turning)
        {
            transform.Rotate(0, 0, 10);
        }
    }
    public override void BoostStart()
    {
        if (startTimer > 160)
        {
            audioPlayer2.GetComponent<AudioSource>().Play();
            backLeft.motorTorque = throttleControlValue * 9999;
            backRight.motorTorque = throttleControlValue * 9999;
        }
    }

	public override void BoostStop()
    {
        backLeft.motorTorque = throttleControlValue * 0;
        backRight.motorTorque = throttleControlValue * 0;
    }

	public override void ActionStart()
	{
        if (!grounded && startTimer > 160)
        {
            turning = true;
            audioPlayer.GetComponent<AudioSource>().volume = 1;
        }
	}

	public override void ActionStop()
	{
        turning = false;
        audioPlayer.GetComponent<AudioSource>().volume = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            positionTracker.UpdatePosition(playerNum, other.GetComponent<CheckpointScript>().index);
        }
    }
}
