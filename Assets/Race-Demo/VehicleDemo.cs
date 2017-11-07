using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleDemo : VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;
    [SerializeField] private WheelCollider[] m_WheelColliders = new WheelCollider[4];
    public float m_Downforce = 100f;
    public Transform flWheel;
   public Transform frWheel;
   public Transform blWheel;
   public Transform brWheel;
   public float maxSteer;
	public float maxTorque;
   public ParticleSystem boostPart;
   public ParticleSystem impactPart;
   public bool canBoost = true;
   public bool canMove = false;
   public Text countDown;
   public int carHealth = 100;
    public GameObject explo;
    private Rigidbody myRigidbody;

    public SpringJoint springJoint;



   public RaycastHit hit;
   public LayerMask cullingmask;

   public int maxDistance;
   public bool isFlying = false;

   public Vector3 loc;
   public float speed = 10;
   public Transform car;

   public GameObject myCar;
   public LineRenderer LR;

   public GameObject baseHarpoon;

   void Start () 
	{


		//this is to keep the wheels from jittering
		frontRight.ConfigureVehicleSubsteps(5f, 12, 15);
		frontLeft.ConfigureVehicleSubsteps(5f, 12, 15);
		backRight.ConfigureVehicleSubsteps(5f, 12, 15);
		backLeft.ConfigureVehicleSubsteps(5f, 12, 15);

        myRigidbody = GetComponent<Rigidbody>();

      StartCoroutine(Timer());
   }

   IEnumerator Timer()
   {

      yield return new WaitForSeconds(1);
      countDown.text = "3";
      yield return new WaitForSeconds(1);
      countDown.text = "2";
      yield return new WaitForSeconds(1);
      countDown.text = "1";
      yield return new WaitForSeconds(1);
      countDown.text = "GO!";
      yield return new WaitForSeconds(1);
      countDown.text = "";
      canMove = true;
   }


   void Drive()
	{

      if (canMove == true)
      {
         //turning
         frontLeft.steerAngle = steeringControlValue * maxSteer;
         frontRight.steerAngle = steeringControlValue * maxSteer;

         //torque
         backLeft.motorTorque = throttleControlValue * maxTorque;
         backRight.motorTorque = throttleControlValue * maxTorque;

         //notice that the wheel visuals do NOT turn. You might want to make that work if it's visible to the player.
         //there's actually a bit about that in the Unity Wheelcollider tutorial: https://docs.unity3d.com/Manual/WheelColliderTutorial.html

         flWheel.localEulerAngles = new Vector3(flWheel.localEulerAngles.x, frontLeft.steerAngle - flWheel.localEulerAngles.z, flWheel.localEulerAngles.z);
         frWheel.localEulerAngles = new Vector3(frWheel.localEulerAngles.x, frontRight.steerAngle - frWheel.localEulerAngles.z, frWheel.localEulerAngles.z);

         flWheel.Rotate(frontLeft.rpm / 60 * 360 * Time.deltaTime, 0, 0);
         frWheel.Rotate(frontRight.rpm / 60 * 360 * Time.deltaTime, 0, 0);
         blWheel.Rotate(backLeft.rpm / 60 * 360 * Time.deltaTime, 0, 0);
         brWheel.Rotate(backRight.rpm / 60 * 360 * Time.deltaTime, 0, 0);

      }
	}
	
	void Update () 
	{
		Drive ();
        if (IsGrounded())
        {
            DownForce();
        }
        CheckHealth();
        DrawSpringLine();
	}


    void CheckHealth()
    {
        if(carHealth <= 0)
        {
            GameObject exploTemp = Instantiate(explo, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(exploTemp, 10f);
        }
    }

    void DownForce()
    {
            m_WheelColliders[0].attachedRigidbody.AddForce(-transform.up * m_Downforce * m_WheelColliders[0].attachedRigidbody.velocity.magnitude);
        
    }

    private bool IsGrounded()
    {
        WheelHit leftHit;
        WheelHit rightHit;
        bool result = false;


        if (frontLeft.GetGroundHit(out leftHit))
        {
            result = true;
        }
        if(frontRight.GetGroundHit(out rightHit))
        {
            result = true;
        }

        return result;
    }


	public override void BoostStart()
	{

      if (canMove == true)
      {
         if (canBoost == true)
         {
            boostPart.Play();
            Debug.Log("BOOST");
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1000, ForceMode.Acceleration);
         }
      }

	}

void OnTriggerEnter(Collider col) {

      if (col.tag == "vehicle")
      {
         ParticleSystem impactTemp = Instantiate(impactPart, col.transform.position, col.transform.rotation);
         Destroy(impactTemp, 3f);
      }

        if (col.tag == "Bumper")
        {
            ParticleSystem impactTemp = Instantiate(impactPart, col.transform.position, col.transform.rotation);
            Destroy(impactTemp, 3f);
            carHealth = carHealth - 20;
            Debug.Log("Hit");
        }

    }

	public override void BoostStop()
	{
      boostPart.Stop();
      canBoost = false;
      StartCoroutine(BoostTimer());
   }

   IEnumerator BoostTimer()
   {
      
      yield return new WaitForSeconds(3);
      canBoost = true;
   }

   public override void ActionStart()
	{
      FindSpot();
      Debug.Log("hooking");

      if (isFlying == true)
         {
            Flying();
         }


	}

	public override void ActionStop()
	{
        return;
		if(isFlying == true)
      {

         isFlying = false;
         LR.enabled = false;
         canMove = true;
            Destroy(springJoint);
                

      }
	}

    void DrawSpringLine()
    {
        if (springJoint != null)
        {
            LR.enabled = true;
            LR.SetPosition(0, myRigidbody.position);
            LR.SetPosition(1, springJoint.connectedBody.transform.position);
        }
        else
        {
            LR.enabled = false;
        }
    }

   public void FindSpot()
   {

      
      if (Physics.Raycast(baseHarpoon.transform.position, baseHarpoon.transform.forward, out hit, maxDistance, cullingmask))
      {
            if (hit.rigidbody != null)
            {
                Debug.Log("hooking");
                 isFlying = true;
                 loc = hit.point;
                 canMove = false;
                 LR.enabled = true;
                 LR.SetPosition(1, loc);


                springJoint = gameObject.AddComponent<SpringJoint>();
                
                springJoint.connectedBody = hit.rigidbody;
                springJoint.spring = 1000f;
                springJoint.maxDistance = 10f;
                springJoint.minDistance = 2f;
                springJoint.enableCollision = true;
            }


        }

   }

   public void Flying()
   {
        canMove = true;


        //      transform.position = Vector3.Lerp(transform.position, loc, speed * Time.deltaTime / Vector3.Distance(transform.position, loc));
        //      LR.SetPosition(0, car.position);

        if (Vector3.Distance(transform.position, loc) < 0.5f)
      {
         isFlying = false;
        // LR.enabled = false;
         canMove = true;
      }
   }

}
