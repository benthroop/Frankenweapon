using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class VehicleGingello : VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;

	public float maxSteer;
	public float maxTorque;
    public float Boost;
    public float maxBrake;
    public float DownForce;
    public bool Boosting;

   



	void Start () 
	{
		//this is to keep the wheels from jittering
		frontRight.ConfigureVehicleSubsteps(5f, 12, 15);
		frontLeft.ConfigureVehicleSubsteps(5f, 12, 15);
		backRight.ConfigureVehicleSubsteps(5f, 12, 15);
		backLeft.ConfigureVehicleSubsteps(5f, 12, 15);
        
	}
    void Update()
    {
        Drive();    
    }
    void FixedUpdate()
    {
        if (IsGrounded())
        {
            AddSweetDownForce();
            Debug.Log("Down");
        }       
    }

     public bool IsGrounded()
    {

        WheelHit leftHit;
        WheelHit rightHit;
        bool result = false;
        if(frontLeft.GetGroundHit(out leftHit))
        {
            result = true;
        }
        if(frontRight.GetGroundHit(out rightHit))
        {
            result = true;
        }
        
        return result;
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
                if (Boosting == true)
                {
                    backLeft.motorTorque = throttleControlValue * maxTorque * Boost;
                    backRight.motorTorque = throttleControlValue * maxTorque * Boost;
                    Debug.Log(backRight.motorTorque.ToString());
                    Debug.Log(backLeft.motorTorque.ToString());

                }
                else
                {
                    backLeft.brakeTorque = 0f;
                    backRight.brakeTorque = 0f;
                    frontLeft.brakeTorque = 0f;
                    frontRight.brakeTorque = 0f;

                    backLeft.motorTorque = throttleControlValue * maxTorque;
                    backRight.motorTorque = throttleControlValue * maxTorque;
                }
            }
            //moving backward
            else
            {
                backLeft.brakeTorque = maxBrake * Mathf.Abs(throttleControlValue);
                backRight.brakeTorque = maxBrake * Mathf.Abs(throttleControlValue); 
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
        backLeft.brakeTorque = 10000f;
        backRight.brakeTorque = 10000f;
    }

	public override void ActionStop()
	{
        backLeft.brakeTorque = 0f;
        backRight.brakeTorque = 0f;
    }


    public void AddSweetDownForce()
    {

            GetComponent<Rigidbody>().AddForce(-transform.up * DownForce * GetComponent<Rigidbody>().velocity.magnitude);
        }
  


    

}
