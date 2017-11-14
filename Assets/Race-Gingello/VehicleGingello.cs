using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
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

    public int CheckNum, CheckFin, CheckLap;
    public bool Boosting;
    [SerializeField] Vector3 PreviousCheckpoint, startPoint;
    public Vector3 CurrentPosition, PPosition;
    [SerializeField] GameObject NextCP;
    public GameObject[] CheckPoints;
    public GameObject FinLine, BoostDam;
    [SerializeField] UnityEvent BoostParticles;
    [SerializeField] UnityEvent BoostParticlesOff;
    [SerializeField] UnityEvent CheckPointPassed;
    public float distToNextCP;

    // Got the idea for my checkpoint system from thomas in class so credit due to him for the idea




    void Start () 
	{
		//this is to keep the wheels from jittering
		frontRight.ConfigureVehicleSubsteps(5f, 12, 15);
		frontLeft.ConfigureVehicleSubsteps(5f, 12, 15);
		backRight.ConfigureVehicleSubsteps(5f, 12, 15);
		backLeft.ConfigureVehicleSubsteps(5f, 12, 15);
        NextCP = CheckPoints[CheckNum];
        startPoint = this.gameObject.transform.position;
    }
    void Update()
    {
        PPosition = new Vector3(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y + 2, this.gameObject.transform.localPosition.z);
        distToNextCP = CurrentPosition.magnitude;
       

        CheckPosition();
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
            BoostParticles.Invoke();
       
    }

	public override void BoostStop()
	{
	    Boosting = false;
        BoostParticlesOff.Invoke();
	}

	public override void ActionStart()
	{Debug.Log ("BoostDamager");
		BoostDamage ();
    }

	public override void ActionStop()
	{
		
    }


    public void AddSweetDownForce()
    {

            GetComponent<Rigidbody>().AddForce(-transform.up * DownForce * GetComponent<Rigidbody>().velocity.magnitude);
        }


    private void OnTriggerEnter(Collider cp)
    {
        
            
        if (cp.transform.gameObject.name == "9")
        {

            CheckNum += 1;
            CheckFin += 1;
            NextCP = CheckPoints[CheckNum];
            FinLine.SetActive(true);
            PreviousCheckpoint = cp.transform.localPosition;

        }
        if (cp.transform.gameObject.name =="11")
        {

            CheckNum = 0;
            CheckFin += 1;
            NextCP = CheckPoints[CheckNum];
            PreviousCheckpoint = cp.transform.localPosition;

        }
		if (this.gameObject.transform.name == "Vehicle-Gingello") { 
			if (cp.gameObject.transform.name ==  ("StartLine")) {
				CheckFin += 1;
				CheckLap += 1;
				FinLine.SetActive (false);
				PreviousCheckpoint = cp.transform.localPosition;

			}
		}
		if (this.gameObject.transform.name == "Vehicle-GingelloBlue") { 
			if (cp.gameObject.transform.name ==  ("StartLine2")) {
				CheckFin += 1;
				CheckLap += 1;
				FinLine.SetActive (false);
				PreviousCheckpoint = cp.transform.localPosition;

			}
		}
        if (cp.gameObject.name == "LifeAlert")
        {
            if (CheckNum <= 0)
            {

                this.gameObject.transform.position = startPoint;
                this.gameObject.transform.rotation = Quaternion.Euler(0, this.gameObject.transform.rotation.eulerAngles.y, 0);
            }
            else
            {
                this.gameObject.transform.position = PreviousCheckpoint;
                this.gameObject.transform.rotation = Quaternion.Euler(0, this.gameObject.transform.rotation.eulerAngles.y, 0);
            }
        }
		if(cp.CompareTag("BoostD"))
			{

				Boosting = false;

			}
        else if (cp.CompareTag("Checkpoint"))
        {
            Debug.Log("StanCP");

            CheckNum += 1;
            CheckFin += 1;
            NextCP = CheckPoints[CheckNum];
            PreviousCheckpoint = cp.transform.localPosition;
        }


        }


        void CheckPosition()
    {


        CurrentPosition = NextCP.transform.position - this.gameObject.transform.position;
       

    }

	void BoostDamage()
	{
		
			BoostDam.SetActive (true);
		
	





	}


  

    


}
