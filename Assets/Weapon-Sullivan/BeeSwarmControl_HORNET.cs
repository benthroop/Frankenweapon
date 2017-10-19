using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSwarmControl_HORNET : MonoBehaviour {

    public float speed;
    float RotationSpeed = 3f; 
    GameObject Target;
    private Vector3 Difference;
    private float atan2;
    Rigidbody rb;
    bool LockedOn = false;
    float ColliderTick = 1f;
    BoxCollider DetectionCollider;
    float Attacks = 4f;
    float LifeTick = 30f;
    public GameObject OOOH;
    bool IsDead = false;
 

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        DetectionCollider = GetComponent<BoxCollider>();  
	}

    // Update is called once per frame
    void Update()
    {
        ColliderTick -= Time.deltaTime;
        LifeTick -= Time.deltaTime; 
        if(ColliderTick <= 0)
        {
            DetectionCollider.enabled = true; 
        }
        if(LifeTick <= 0)
        {
            Destroy(gameObject); 
        }

        if (LockedOn == true)
        {
            Vector3 TargetDirection = Target.transform.position - transform.position;
            float step = RotationSpeed * Time.deltaTime;
            Vector3 NewDirection = Vector3.RotateTowards(transform.forward, TargetDirection, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(NewDirection);
        }
    }

    void FixedUpdate ()
    {
        if (IsDead == false)
        {
            rb.AddRelativeForce(Vector3.forward * speed);
        }
       
    }

    void OnCollisionEnter (Collision other)
    {
        rb.AddForce(-Vector3.forward * 2, ForceMode.Impulse);
        if (other.gameObject.tag == "Player")
        {
            Attacks--;
            CheckAttacks();
            Instantiate(OOOH, gameObject.transform.position, gameObject.transform.rotation); 
        }

    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            LockedOn = true;
            Target = other.gameObject; 
        }
    }

    void CheckAttacks ()
    {
        if (Attacks == 0)
        {
            Destroy(gameObject);
        }
    }

    public void Death()
    {
        rb.useGravity = true;
        IsDead = true; 
    }
}
