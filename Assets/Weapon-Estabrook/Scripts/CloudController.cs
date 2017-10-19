using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {


    public GameObject explosionParticles;
    public GameObject lightningLight;

    public static CloudController instance;
    private void Awake()
    {
        instance = this;

        
    }

    void OnTriggerEnter (Collider col)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;

    }

    public void explode()
    {
        Debug.Log("EXPLODE");
        
        Collider[] explodeColliders = Physics.OverlapSphere(transform.position, 4f);
        for(int i = 0; i < explodeColliders.Length; i++)
        {
            Rigidbody hitRB = explodeColliders[i].GetComponent<Rigidbody>();

            RaycastHit hitItem;

            if (Physics.Linecast(transform.position, explodeColliders[i].transform.position, out hitItem))
            {
                if(hitRB != null)
                {
                    hitRB.AddForce(hitItem.normal * -500f);
                    hitRB.AddForce(Vector3.up * 500f);

                }
            }
        }
        explosionParticles.GetComponent<ParticleSystem>().Play();
        
        Destroy(this.gameObject, .1f);

    }
}
