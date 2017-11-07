using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class VehicleTully: VehicleBase
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    public float maxSteer;
    public float maxTorque;
    public float downforce;

    private Rigidbody myRigidbody;

    void Start()
    {
        //this is to keep the wheels from jittering
        frontRight.ConfigureVehicleSubsteps(5f, 12, 15);
        frontLeft.ConfigureVehicleSubsteps(5f, 12, 15);
        backRight.ConfigureVehicleSubsteps(5f, 12, 15);
        backLeft.ConfigureVehicleSubsteps(5f, 12, 15);
        myRigidbody = GetComponent<Rigidbody>();
        if (myRigidbody == null)
        {
            Debug.LogError("You a Stupid");
        }
    }

    void Drive()
    {
        //turning
        frontLeft.steerAngle = steeringControlValue * maxSteer;
        frontRight.steerAngle = steeringControlValue * maxSteer;

        //torque
        backLeft.motorTorque = throttleControlValue * maxTorque;
        backRight.motorTorque = throttleControlValue * maxTorque;

        ApplyLocalPositionToVisuals(frontLeft);
        ApplyLocalPositionToVisuals(frontRight);
        ApplyLocalPositionToVisuals(backLeft);
        ApplyLocalPositionToVisuals(backRight);


    }

    void FixedUpdate()
    {
        Drive();
        if (IsGrounded())
        {
            AddDownForce();
        }
        if (transform.position.x < -29) transform.position = new Vector3(29, transform.position.y, transform.position.z);
        if (transform.position.x > 29) transform.position = new Vector3(-29, transform.position.y, transform.position.z);
        if (transform.position.z < -29) transform.position = new Vector3(transform.position.x, transform.position.y, 29);
        if (transform.position.z > 29) transform.position = new Vector3(transform.position.x, transform.position.y, -29);
    }

    public void AddDownForce()
    {
        myRigidbody.AddForce(-transform.up * downforce * myRigidbody.velocity.magnitude);
    }

    private bool IsGrounded()
    {
        WheelHit leftHit;
        WheelHit rightHit;
        bool result = false;
        
        if(frontLeft.GetGroundHit(out leftHit))
        {
            result = true;
        }
       if (frontRight.GetGroundHit(out rightHit))
        {
            result = true;
        }

        return result;

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
        //all you
    }

    public override void BoostStop()
    {
        //all you
    }

    public override void ActionStart()
    {
        //e brake on
        backLeft.brakeTorque = 10000f;
        backRight.brakeTorque = 10000f;


    }

    public override void ActionStop()
    {
        backLeft.brakeTorque = 0f;
        backRight.brakeTorque = 0f;
    }
}
