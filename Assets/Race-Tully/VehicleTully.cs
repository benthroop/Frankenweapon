using UnityEngine;
using System.Collections;

public class VehicleTully: VehicleBase
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    public float maxSteer;
    public float maxTorque;

    void Start()
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

        ApplyLocalPositionToVisuals(frontLeft);
        ApplyLocalPositionToVisuals(frontRight);
        ApplyLocalPositionToVisuals(backLeft);
        ApplyLocalPositionToVisuals(backRight);


    }

    void FixedUpdate()
    {
        Drive();

        if (transform.position.x < -29) transform.position = new Vector3(29, transform.position.y, transform.position.z);
        if (transform.position.x > 29) transform.position = new Vector3(-29, transform.position.y, transform.position.z);
        if (transform.position.z < -29) transform.position = new Vector3(transform.position.x, transform.position.y, 29);
        if (transform.position.z > 29) transform.position = new Vector3(transform.position.x, transform.position.y, -29);
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
        //all you
    }

    public override void ActionStop()
    {
        //all you
    }
}
