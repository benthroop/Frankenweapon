using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGun : Weapon
{

    public bool hasLaunched = false;

    public GameObject cloud;
    public GameObject hitPoint;
    public Transform muzzlePoint;

    public GameObject SignalLight;

    public float cloudVelocity;

    GameObject cloudInstance;

    public bool checkHit = false;


    public GameObject pop;
    public GameObject Electricity;
    public GameObject click;

    public GameObject muzzleFlash;
    public GameObject lightning;

    

    void Update()
    {
        if(hasLaunched)
        {
            SignalLight.GetComponent<Light>().enabled = true;

        }
        else
        {

            SignalLight.GetComponent<Light>().enabled = false;
        }
        if (checkHit)
        {
            int layerMask1 = 1 << 10;
            int layerMask2 = 1 << 11;
            layerMask1 = ~layerMask1;
            layerMask2 = ~layerMask2;
            foreach (GameObject cloudIns in GameObject.FindGameObjectsWithTag("Cloud"))
            {
                cloudIns.GetComponent<LineRenderer>().enabled = true;
                lightning.GetComponent<Light>().enabled = true;

                cloudIns.transform.GetChild(3).GetComponent<Light>().enabled = true;

                Vector3[] LineRendPos = new Vector3[2];

                LineRendPos[0] = hitPoint.transform.position;
                LineRendPos[1] = cloudIns.transform.position;

                cloudIns.GetComponent<LineRenderer>().SetPositions(LineRendPos);

                RaycastHit hitItem;

                if (Physics.Linecast(hitPoint.transform.position, cloudIns.transform.position, layerMask1&layerMask2) == false)
                {
                    //checkHit = false;
                    cloudIns.GetComponent<LineRenderer>().enabled = false;
                    lightning.GetComponent<Light>().enabled = false;
                    destroyObject(cloudIns);
                    
                }

                if (Physics.Linecast(hitPoint.transform.position, cloudIns.transform.position, out hitItem))
                {
                    Rigidbody hitRB = hitItem.rigidbody;
                    if (hitRB != null)
                    {
                        hitRB.AddForce(hitItem.normal * -500f);
                        hitRB.AddForce(Vector3.up * 500f);

                    }
                }

                

                
            }
            if (GameObject.FindGameObjectsWithTag("Cloud").Length == 0)
            {
                checkHit = false;
                hasLaunched = false;
            }



        }
        
        

    }

    public override void PrimaryFireStart()
    {
        if(!hasLaunched)
        {
            LaunchCloud();
            muzzleFlash.GetComponent<ParticleSystem>().Play();

        }
        else
        {
            pop.GetComponent<AudioSource>().Play();
            explodeCloud();
        }

    }

    public override void SecondaryFireStart()
    {
        
        LightningCloud();
    }



    void LaunchCloud()
    {
        GetComponent<AudioSource>().Play();
        hasLaunched = true;
        cloudInstance = Instantiate(cloud.gameObject, muzzlePoint.transform.position, muzzlePoint.transform.rotation, null);
        cloudInstance.GetComponent<Rigidbody>().velocity = muzzlePoint.forward * cloudVelocity;
    }

    void explodeCloud()
    {
        foreach (GameObject cloudIns in GameObject.FindGameObjectsWithTag("Cloud"))
        {
            cloudIns.GetComponent<CloudController>().explode();
        }
        
        hasLaunched = false;
    }

    void LightningCloud()
    {
        if (hasLaunched)
        {
            Electricity.GetComponent<AudioSource>().Play();
            hasLaunched = false;
            checkHit = true;


        }
        else
        {
            click.GetComponent<AudioSource>().Play();

        }

    }

    void destroyObject(GameObject obj)
    {
        
        Destroy(obj.gameObject);

    }
}

