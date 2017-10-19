using UnityEngine;
using UnityEngine.Events;

public class TosserGun : Weapon {

    public float range = 15.0f;
    public Transform holdPosition;
    public float force = 75.0f;
    public ForceMode throwForceMode = ForceMode.VelocityChange;
    public LayerMask layerMask = -1;

   

    private GameObject heldObject = null;

    public ParticleSystem particleEffect;
    public UnityEvent Pew;
    public UnityEvent Throw;
    public UnityEvent Err;

    public override void PrimaryFireStart()
    {
        if(heldObject == null)
        {
            Pull();
        }
        else
        {
            if (Throw != null)
            {
                Throw.Invoke();
            }
            Push();
        }

    }

    private void Update()
    {
        if(heldObject != null)
        {
            heldObject.transform.position = holdPosition.position;
            heldObject.transform.rotation = holdPosition.rotation;
        }
    }

    /*void Update ()
    {
        if(heldObject == null)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                
                Pull();
            }
        }
        else
        {
            heldObject.transform.position = holdPosition.position;
            heldObject.transform.rotation = holdPosition.rotation;

            if(Input.GetButtonDown("Fire1"))
            {
                if(Throw != null)
                {
                    Throw.Invoke();
                }
                Push();
            }
        }
    }*/

    public void Pull()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range, layerMask))
        {
            
            if (hit.rigidbody != null)
            {
                if (Pew != null)
                {
                    Pew.Invoke();
                }
                Debug.Log(hit.transform.name);
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.GetComponent<Collider>().enabled = false;
            }
            else
            {
                if (Err != null)
                {
                    Err.Invoke();
                }
            }
        }
        else
        {
            if (Err != null)
            {
                Err.Invoke();
            }
        }
    }

    public void Push()
    {
        Rigidbody body = heldObject.GetComponent<Rigidbody>();
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.GetComponent<Collider>().enabled = true;
        body.AddForce(transform.up * -force, throwForceMode);
        heldObject = null;
    }

}
