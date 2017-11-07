using UnityEngine;
using System.Collections;

public class VehicleAlter : VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;

	public float maxSteer;
	public float maxTorque;
    public bool canBoost = true;
    public bool canMove = true;

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
	
	void Update () 
	{
		Drive ();
        VisualSpin(frontLeft);
        VisualSpin(frontRight);
        VisualSpin(backRight);
        VisualSpin(backLeft);
        EBrake();
	}

    void VisualSpin(WheelCollider collider)
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




    public override void BoostStop()
    {
        canBoost = false;
        StartCoroutine(BoostTimer());
    }

    IEnumerator BoostTimer()
    {

        yield return new WaitForSeconds(3);
        canBoost = true;
    }
    public override void BoostStart()
    {

        if (canMove == true)
        {
            if (canBoost == true)
            {
                Debug.Log("BOOST");
                gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1000, ForceMode.Acceleration);
            }
        }

    }


    void EBrake()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Ebrake");
            backLeft.brakeTorque = 100000000f;
            backRight.brakeTorque = 100000000000f;

        }
        else
        {
            backLeft.brakeTorque = 0f;
            backRight.brakeTorque = 0f;
        }
    }
}
