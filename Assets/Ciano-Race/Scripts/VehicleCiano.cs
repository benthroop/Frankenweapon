using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VehicleCiano : VehicleBase {
	public GameObject WorldCore;

	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;

	public float maxSteer;
	public float BaseTorque;
	public float TorqueMultiplier;
	public float CurrentTorque;

	public float maxBrake;
	public float DownForce;

	public GameObject SmokeMaker;
    public GameObject SolidCarNoise;
    public GameObject GhostCarNoise;

	void Start () {
			//this is to keep the wheels from jittering
		frontRight.ConfigureVehicleSubsteps(5f, 12, 15);
		frontLeft.ConfigureVehicleSubsteps(5f, 12, 15);
		backRight.ConfigureVehicleSubsteps(5f, 12, 15);
		backLeft.ConfigureVehicleSubsteps(5f, 12, 15);
		CurrentTorque = BaseTorque;
	}

	// finds the corresponding visual wheel
	// correctly applies the transform
	public void ApplyLocalPositionToVisuals(WheelCollider collider) {
		if (collider.transform.childCount == 0){
			return;
		}

        Transform visualWheel = collider.transform.GetChild(0);
        Transform TurningVisual = visualWheel.transform.GetChild(0);

        if (this.gameObject.layer != LayerMask.NameToLayer("CarGroupGhost")) {
           
            visualWheel.GetComponent<MeshRenderer>().enabled = true;
            TurningVisual.GetComponent<MeshRenderer>().enabled = true;

        } else {
            visualWheel.GetComponent<MeshRenderer>().enabled = false;
            TurningVisual.GetComponent<MeshRenderer>().enabled = false;
        }

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

	void Drive() {
		RacingRuleCiano RaceRuleDetails = WorldCore.GetComponent<RacingRuleCiano> ();

		if (RaceRuleDetails.RaceOnGoing == true) {
			frontLeft.steerAngle = steeringControlValue * maxSteer;
			frontRight.steerAngle = steeringControlValue * maxSteer;

			//throttle backward
			if (throttleControlValue < 0f) {
				//moving backward
				if (transform.InverseTransformVector (GetComponent<Rigidbody> ().velocity).z < Mathf.Epsilon) {
					backLeft.brakeTorque = 0f;
					backRight.brakeTorque = 0f;
					frontLeft.brakeTorque = 0f;
					frontRight.brakeTorque = 0f;

					backLeft.motorTorque = throttleControlValue * CurrentTorque;
					backRight.motorTorque = throttleControlValue * CurrentTorque;
				} else {
					backLeft.brakeTorque = maxBrake * Mathf.Abs (throttleControlValue);
					backRight.brakeTorque = maxBrake * Mathf.Abs (throttleControlValue);
					frontLeft.brakeTorque = maxBrake * Mathf.Abs (throttleControlValue);
					frontRight.brakeTorque = maxBrake * Mathf.Abs (throttleControlValue);
				}
			} else {
				//moving forward
				if (transform.InverseTransformVector (GetComponent<Rigidbody> ().velocity).z > Mathf.Epsilon) {
					backLeft.brakeTorque = 0f;
					backRight.brakeTorque = 0f;
					frontLeft.brakeTorque = 0f;
					frontRight.brakeTorque = 0f;

					backLeft.motorTorque = throttleControlValue * CurrentTorque;
					backRight.motorTorque = throttleControlValue * CurrentTorque;
				} else {
					backLeft.brakeTorque = maxBrake * Mathf.Abs (throttleControlValue);
					backRight.brakeTorque = maxBrake * Mathf.Abs (throttleControlValue);
					;
					frontLeft.brakeTorque = maxBrake * Mathf.Abs (throttleControlValue);
					frontRight.brakeTorque = maxBrake * Mathf.Abs (throttleControlValue);
				}
			}

                ApplyLocalPositionToVisuals(frontRight);
                ApplyLocalPositionToVisuals(frontLeft);
                ApplyLocalPositionToVisuals(backLeft);
                ApplyLocalPositionToVisuals(backRight);
            
            if (frontLeft.isGrounded == true && frontRight.isGrounded == true) {
                GetComponent<Rigidbody>().AddForce(-transform.up * DownForce * GetComponent<Rigidbody>().velocity.magnitude);
            }
	
			GhostCheck ();
            NoiseCheck();
        }
	}

	void Update () {
		Drive ();
	}

	void GhostCheck() {
		if (this.gameObject.layer == LayerMask.NameToLayer ("CarGroupGhost")) {
			SmokeMaker.GetComponent<ParticleSystem> ().emissionRate = 30f;
		} else {
			SmokeMaker.GetComponent<ParticleSystem> ().emissionRate = 0f;
		}
	}

    void NoiseCheck() {
        if (this.gameObject.layer == LayerMask.NameToLayer("CarGroupGhost"))
        {
            GhostCarNoise.SetActive(true);
            SolidCarNoise.SetActive(false);
        }
        else
        {
            SolidCarNoise.SetActive(true);
            GhostCarNoise.SetActive(false);
        }
       
    }

    public override void BoostStart(){
		CurrentTorque = BaseTorque / TorqueMultiplier;

		Debug.Log ("Ghost");
		this.gameObject.layer = LayerMask.NameToLayer ("CarGroupGhost");

		foreach (Transform child in transform) {
			child.gameObject.layer = LayerMask.NameToLayer ("CarGroupGhost");
		}
			
	}

	public override void BoostStop(){
		CurrentTorque = BaseTorque;

		Debug.Log ("Solid");
		this.gameObject.layer = LayerMask.NameToLayer ("CarGroupSolid");

		foreach (Transform child in transform) {
			child.gameObject.layer = LayerMask.NameToLayer ("CarGroupSolid");
		}
	}

	public override void ActionStart(){
		
	}

	public override void ActionStop(){
        InformationDisplay PlaceDetails = GetComponent<InformationDisplay>();
        transform.position = PlaceDetails.PreviousCheckPoint.transform.position;
        transform.rotation = PlaceDetails.PreviousCheckPoint.transform.rotation;
    }
}