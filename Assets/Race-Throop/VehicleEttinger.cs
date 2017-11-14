using UnityEngine;
using System.Collections;

public class VehicleEttinger : VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;

	public float maxSteer;
	public float maxTorque;
    private int score;
    public GameObject Reset;

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
	}
	
	void FixedUpdate () 
	{
		Drive ();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(0);

    }

    private void OnTriggerEnter(Collider other)
    {
        score = score + 1;
        if (other.gameObject.tag == "Respawn")
        {
            gameObject.transform.position = Reset.gameObject.transform.position;
            gameObject.transform.rotation = Reset.gameObject.transform.rotation;
            gameObject.GetComponent<Rigidbody>().velocity= new Vector3(0,0,0);
        }

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
