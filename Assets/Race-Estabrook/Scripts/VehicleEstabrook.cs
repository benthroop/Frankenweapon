using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class VehicleEstabrook: VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;

	public float maxSteer;
	public float maxTorque;

    public GameObject gun;
    
    public GameObject camera;

    public GameObject bulletPrefab;
    public GameObject shootPosition;

    [SerializeField]
    bool isRacing;
    [SerializeField] Transform racePos, testPos;

    public int playerNumber;
    bool hasChecked;
    public int downForce;

    private Rigidbody rb;

    Transform lastHit;

    public int count = 0;
    public int score = 0;
    int boost = 50;
    int modifier = 1;

    public GameObject shield;
    public bool isShielded = false;
	void Start () 
	{
        lastHit = racePos;
        //gun.transform.Rotate(Vector3.forward * 90);
        rb = GetComponent<Rigidbody>();
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
        if (!RaceManager.instance.isCountdown)
        {
            //torque
            backLeft.motorTorque = throttleControlValue * maxTorque;
            backRight.motorTorque = throttleControlValue * maxTorque;
        }

        ApplyLocalPositionToVisuals(frontLeft);
        ApplyLocalPositionToVisuals(frontRight);
        ApplyLocalPositionToVisuals(backLeft);
        ApplyLocalPositionToVisuals(backRight);

       
    }

    private void Update()
    {
        //FindTarget();
        //gun.transform.Rotate(Vector3.up * 180);
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
        }
        if (transform.position.y < -40)
        {
            transform.position = new Vector3(lastHit.position.x, lastHit.position.y, lastHit.position.z);
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;

        }
        Debug.Log(modifier);
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

        //gun.transform.rotation = Quaternion.LookRotation(gun.transform.position - LookThing.transform.position, Vector3.up);
        Vector3 cameraRot = new Vector3(cameraControlValuey*-1, cameraControlValuex, 0);
        camera.transform.Rotate(cameraRot);
        gun.transform.Rotate(new Vector3(0, cameraControlValuex, 0));
        

        AddDownForce();
    }

    private void AddDownForce()
    {
        rb.AddForce(-transform.up * downForce);
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
        shield.SetActive(true);
        isShielded = true;
	}

	public override void BoostStop()
	{
        shield.SetActive(false);
        isShielded = false;
	}

	public override void ActionStart()
	{
        if (!isShielded && !RaceManager.instance.isCountdown)
        {
            Shoot();
        }
	}

	

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, shootPosition.transform.position, shootPosition.transform.rotation);
        bulletInstance.GetComponent<bulletScript>().playerNum = playerNumber;
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
            lastHit = col.gameObject.transform;
            string previous = "";
            if (previous != col.name)
            {
                score++;
                
                previous = col.name;

            }

        }
    }
}
