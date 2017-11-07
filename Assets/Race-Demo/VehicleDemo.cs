using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleDemo : VehicleBase 
{
	[SerializeField] WheelCollider frontRight;
	[SerializeField] WheelCollider frontLeft;
	[SerializeField] WheelCollider backRight;
	[SerializeField] WheelCollider backLeft;
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
		if(isFlying == true)
      {

         isFlying = false;
         LR.enabled = false;
         canMove = true;

      }
	}

   public void FindSpot()
   {

      
      if (Physics.Raycast(baseHarpoon.transform.position, baseHarpoon.transform.forward, out hit, maxDistance, cullingmask))
      {
         Debug.Log("hooking");
         isFlying = true;
         loc = hit.point;
         canMove = false;
         LR.enabled = true;
         LR.SetPosition(1, loc);
      }

   }

   public void Flying()
   {

      transform.position = Vector3.Lerp(transform.position, loc, speed * Time.deltaTime / Vector3.Distance(transform.position, loc));
      LR.SetPosition(0, car.position);

      if (Vector3.Distance(transform.position, loc) < 0.5f)
      {
         isFlying = false;
         LR.enabled = false;
         canMove = true;
      }
   }

}
