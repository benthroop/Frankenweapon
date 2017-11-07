using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour {


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


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		


	}

public void FindSpot() {

      if (Physics.Raycast(baseHarpoon.transform.position, baseHarpoon.transform.forward, out hit, maxDistance, cullingmask))
      {
         isFlying = true;
         loc = hit.point;
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
      }
   } 

}
