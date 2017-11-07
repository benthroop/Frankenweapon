using UnityEngine;
using System.Collections;

public class VehicleEstabrook: VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;

	public float maxSteer;
	public float maxTorque;

    public GameObject gun;

    [SerializeField]
    bool isRacing;
    [SerializeField] Transform racePos, testPos;

    public int playerNumber;

    bool hasChecked;

    int count = 0;
    public int score = 0;
	void Start () 
	{
        if(isRacing)
        {
            transform.position = racePos.position;
        }
        else
        {
            transform.position = testPos.position;
        }
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

    private void Update()
    {
        FindTarget();
        //gun.transform.Rotate(Vector3.up * 180);
    }

    void FixedUpdate () 
	{
        if (!RaceManager.instance.isWon)
        {
            Drive();
            if (!isRacing)
            {
                if (transform.position.x < -29) transform.position = new Vector3(29, transform.position.y, transform.position.z);
                if (transform.position.x > 29) transform.position = new Vector3(-29, transform.position.y, transform.position.z);
                if (transform.position.z < -29) transform.position = new Vector3(transform.position.x, transform.position.y, 29);
                if (transform.position.z > 29) transform.position = new Vector3(transform.position.x, transform.position.y, -29);
            }
        }
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

    public void FindTarget()
    {
        int layermask = 1 << 31;
        layermask = ~layermask;

        GameObject target = null;

        Collider[] sensedObjects = Physics.OverlapSphere(gun.transform.position, 5f, layermask);
        for(int i = 0; i < sensedObjects.Length; i++)
        {
           
            Rigidbody hitRB = sensedObjects[i].GetComponent<Rigidbody>();

            RaycastHit hitItem;

            

            if (Physics.Linecast(gun.transform.position, sensedObjects[i].transform.position, out hitItem, layermask))
            {
                if (hitRB != null && hitRB.tag == "Wood")
                {
                    Debug.Log(hitItem.transform.name);

                    target = hitItem.transform.gameObject;
                        //hitRB.AddForce(hitItem.normal * -500f);
                        //hitRB.AddForce(Vector3.up * 500f);

                }
            }
            
        }
        if(target != null)
        {
            gun.transform.rotation = Quaternion.LookRotation(target.transform.position, Vector3.up);

        }
        /*RaycastHit targetcar;
        if(Physics.SphereCast(gun.transform.position, 1f, gun.transform.right, out targetcar, 100))
        {
            Debug.Log("Test");
            Debug.Log("RaycastHit: " + targetcar.transform.name);
            if (targetcar.transform.gameObject.tag == "Wood")
            {
                Debug.Log("Hit");
                targetcar.rigidbody.AddForce(targetcar.normal * 300);
            }
        }*/
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
        
	}

	public override void ActionStop()
	{
		//all you
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Lap")
        {
            //Debug.Log("has checked");
            if (hasChecked)
            {
                RaceManager.instance.passLap(playerNumber, count);
                count++;
                hasChecked = false;

            }

        }
        else if (col.tag == "Checker")
        {
            hasChecked = true;
        }else if(col.tag == "Point")
        {
            string previous = "";
            if (previous != col.name)
            {
                score++;
                previous = col.name;

            }

        }
    }
}
