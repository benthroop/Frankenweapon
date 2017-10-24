using UnityEngine;
using System.Collections;
 
public class PhysicalExplosion : MonoBehaviour 
{
	public GameObject fire;
    public Material scorch;
	public float explosiveForce = 20f;
	public float explosiveRadius = 5f;
	public float upwardsForce = 5f;

	private Collider[] hitColliders;

	void Start() {
		hitColliders = Physics.OverlapSphere (transform.position, explosiveRadius);
		foreach (Collider hit in hitColliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody> ();

			if (rb != null) {
				//    rb.AddExplosionForce(explosiveForce, transform.position, explosiveRadius, upwardsForce, ForceMode.Impulse);
				rb.AddForce((hit.transform.position - transform.position).normalized * explosiveForce);
			}

			if (hit.tag == "Wood") {
                Renderer rend = hit.gameObject.GetComponent<Renderer>();
                Transform hitT = hit.gameObject.transform;
				GameObject fireTemp = Instantiate (fire, hit.transform.position, hit.transform.rotation);
				fireTemp.transform.parent = hitT;
				Destroy (fireTemp, 20f);
                rend.material = scorch;

            }

            if (hit.gameObject.GetComponent<BeeSwarmControl_HORNET>() != null)
            {

                hit.gameObject.GetComponent<BeeSwarmControl_HORNET>().Death();
                

            }


        }
    }


}﻿