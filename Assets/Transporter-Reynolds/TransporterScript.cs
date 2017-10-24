
using System.Collections.Generic;
using UnityEngine;

public class TransporterScript : Weapon {
    public float range = 100f;
    public List<GameObject> queue = new List<GameObject>();
    AudioSource audio;
    public AudioClip suck, buzz, swap;
    public GameObject glow, poof;
    public void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public override void PrimaryFireStart()
    {
        //Debug.Log("Transporter: PRIMARY FIRE START!");
        Shoot();
    }

    public override void PrimaryFireEnd()
    {
        //Debug.Log("Transporter: PRIMARY FIRE END!");
    }

    public override void SecondaryFireStart()
    {
       // Debug.Log("Transporter: SECONDARY FIRE START!");
        Swap();
    }

    public override void SecondaryFireEnd()
    {
        //Debug.Log("Transporter: SECONDARY FIRE END!");
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            bool repeat = false;
            Debug.Log(hit.transform.name);

            GameObject target = hit.transform.gameObject;
            if (target.GetComponent<Rigidbody>() != null)
            {
                for (int i = 0; i < queue.Count; i++)
                {
                    if (queue[i] == target)
                    {
                        repeat = true;
                    }
                }
                if (repeat)
                {
                    audio.PlayOneShot(buzz);
                }
                else
                {
                    audio.PlayOneShot(suck);
                    AddToQueue(target);
                    Instantiate(glow, target.transform.position, Quaternion.identity, target.transform);
                }

            }
            else
            {
                audio.PlayOneShot(buzz);
            }
        }

    }

    void AddToQueue(GameObject target)
    {
        if (queue.Count >= 2)
        {
            Destroy(queue[0].transform.Find("glow(Clone)").gameObject);
            queue.RemoveAt(0);
        }
        queue.Add(target);
    }

    void Swap()
    {
        Vector3 tempPos;
        if (queue.Count == 2 && queue[0].transform.position != queue[1].transform.position)
        {
            tempPos = queue[0].transform.position;
            queue[0].transform.position = queue[1].transform.position;
            queue[1].transform.position = tempPos;
            Instantiate(poof, queue[0].transform.position, Quaternion.identity);
            Instantiate(poof, queue[1].transform.position, Quaternion.identity);
            audio.PlayOneShot(swap);
        }

        else
        {
            audio.PlayOneShot(buzz);
        }
    }
}
